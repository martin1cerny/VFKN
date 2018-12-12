using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using VFKN.Entities;

namespace VFKN.Parser
{
    internal class Reader
    {
        private readonly Model model;
        private static readonly Dictionary<string, Type> _typesByCodes = new Dictionary<string, Type>();
        private readonly ILogger logger;

        private Entity currentEntity;
        private int currentColumn;
        private List<string> currentColumns;
        private List<FieldInfo> currentFields;
        private Type currentType;

        private bool inHeader = true;

        public Reader(Model model)
        {
            logger = LogProvider.GetLogger<Reader>();

            this.model = model ?? throw new ArgumentNullException(nameof(model));
            if (_typesByCodes.Any())
                return;
            var types = typeof(Model).Assembly.GetTypes()
                .Where(t => t.IsClass && typeof(Entity).IsAssignableFrom(t));
            if (!types.Any())
            {
                logger.LogWarning("There are no types usable to create instances of VFKN");
                return;
            }
            foreach (var type in types)
            {
                if (!(type.GetCustomAttributes(typeof(EntityAttribute), false).FirstOrDefault() is EntityAttribute attr))
                {
                    logger.LogWarning($"Parser code is not defined for {type.Name}");
                    continue;
                }
                var code = attr.Code;
                if (string.IsNullOrWhiteSpace(code))
                {
                    logger.LogWarning($"Parser code is not defined for {type.Name}");
                    continue;
                }

                // build reflection cache
                _typesByCodes.Add(code, type);
            }
        }

        public void Load(Stream file)
        {
            var scanner = Scanner.Create(file);
            var tok = (Tokens)scanner.yylex();
            var complete = false;
            while (tok != Tokens.EOF)
            {
                switch (tok)
                {
                    case Tokens.error:
                        logger.LogWarning($"Unexpected character at line {scanner.yylloc.StartLine}, column {scanner.yylloc.StartColumn}");
                        break;
                    case Tokens.HEADER:
                        break;
                    case Tokens.BLOCK:
                        inHeader = false;
                        StartBlock(scanner.yytext);
                        break;
                    case Tokens.DATA:
                        CreateInstance(scanner.yytext);
                        break;
                    case Tokens.EOL:
                        break;
                    case Tokens.STRING:
                        SetValue(scanner.Text);
                        break;
                    case Tokens.INTEGER:
                        SetValue(scanner.yytext);
                        break;
                    case Tokens.FLOAT:
                        SetValue(scanner.yytext);
                        break;
                    case Tokens.ENDCOL:
                        currentColumn++;
                        break;
                    case Tokens.COL_HEADER:
                        AddColumnHeader(scanner.yytext);
                        break;
                    case Tokens.END_VFKN:
                        complete = true;
                        break;
                    default:
                        break;
                }
                tok = (Tokens)scanner.yylex();
            }
            if (!complete)
                logger.LogWarning("File was not terminated by '&K;' This might signal an incomplete file. ");
        }

        private void SetValue(string value)
        {
            if (currentEntity == null)
                return;

            var field = currentFields[currentColumn];
            if (field == null)
                return;

            var type = Nullable.GetUnderlyingType(field.FieldType) ?? field.FieldType;
            if (type == typeof(string))
            {
                field.SetValue(currentEntity, value);
                return;
            }
            if (string.IsNullOrWhiteSpace(value))
                return;
            if (type == typeof(float))
            {
                if (float.TryParse(value, out float f))
                    field.SetValue(currentEntity, f);
                return;
            }
            if (type == typeof(double))
            {
                if (double.TryParse(value, out double d))
                    field.SetValue(currentEntity, d);
                return;
            }
            if (type == typeof(int))
            {
                if (int.TryParse(value, out int i))
                    field.SetValue(currentEntity, i);
                return;
            }
            if (type == typeof(long))
            {
                if (long.TryParse(value, out long l))
                    field.SetValue(currentEntity, l);
                return;
            }

            if (type.IsEnum)
            {
                try
                {
                    var e = Enum.Parse(type, value);
                    field.SetValue(currentEntity, e);
                }
                catch (Exception)
                {
                    logger.LogWarning($"Unexpected enum {type.Name} value: {value}");
                }
            }

            logger.LogWarning($"Unexpected value type: {type.Name}");
        }


        private void CreateInstance(string flag)
        {
            currentColumn = -1;
            if (currentType == null)
                return;

            var type = GetTypeFromFlag(flag);
            if (type != currentType)
            {
                logger.LogError($"Inconsistent data. Data with flag '{flag}' are not compatible with {currentType.Name}");
                return;
            }
            currentEntity = Create();
            if (currentFields == null)
            {
                currentFields = new List<FieldInfo>();
                var fields = currentType.GetFields();
                foreach (var name in currentColumns)
                {
                    var field = fields.FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
                    if (field == null)
                        logger.LogWarning($"Field '{name}' not found on type '{currentType.Name}'. Resulting model will be incomplete.");
                    currentFields.Add(field);
                }
            }
        }

        private void AddColumnHeader(string data)
        {
            if (currentType == null)
                return;
            var name = data.Split(' ')[0];
            currentColumns.Add(name);
        }

        private void StartBlock(string flag)
        {
            // clear current state
            currentColumn = -1;
            currentColumns = new List<string>();
            currentEntity = null;
            currentType = null;
            currentFields = null;

            currentType = GetTypeFromFlag(flag);
            if (currentType == null)
            {
                logger.LogWarning($"Type not implemented for entity '{flag}'");
                return;
            }
        }

        private Type GetTypeFromFlag(string flag)
        {
            var code = flag.Substring(2);
            if (!_typesByCodes.TryGetValue(code, out Type type))
                return null;
            return type;
        }

        internal Entity Create()
        {
            if (currentType == null)
                return null;
            var entity = Activator.CreateInstance(currentType, model) as Entity;

            if (inHeader)
                return entity;

            if (!model.instances.TryGetValue(entity.GetType(), out List<Entity> entities))
            {
                entities = new List<Entity>();
                model.instances.Add(entity.GetType(), entities);
            }
            entities.Add(entity);
            return entity;
        }

        
    }
}
