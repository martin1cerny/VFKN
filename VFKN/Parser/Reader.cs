using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VFKN.Entities;

namespace VFKN.Parser
{
    public class Reader
    {
        private readonly Model model;
        private static Dictionary<string, Type> _typesByCodes = new Dictionary<string, Type>();
        private readonly ILogger logger;

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

        public void Load(string file)
        {
            using (var s = File.OpenRead(file))
            {
                Load(s);
            }
        }

        public void Load(Stream file)
        {
            var scanner = Scanner.Create(file);
            var tok = (Tokens)scanner.yylex();


        }

        internal Entity Create(string code)
        {
            if (!_typesByCodes.TryGetValue(code, out Type type))
                return null;
            return Activator.CreateInstance(type, this) as Entity;
        }

        internal void Add(Entity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");
            if (entity.ID == default(int))
                throw new ArgumentOutOfRangeException("Entity must have ID");
            if (!model.instances.TryGetValue(entity.GetType(), out Dictionary<int, Entity> entities))
            {
                entities = new Dictionary<int, Entity>();
                model.instances.Add(entity.GetType(), entities);
            }
            if (entities.TryGetValue(entity.ID, out Entity exist))
                throw new ArgumentException($"Entity of type {entity.GetType().Name} with ID={entity.ID} exists already");
            entities.Add(entity.ID, entity);
        }
    }
}
