using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VFKN.Entities;
using VFKN.Parser;

namespace VFKN
{
    public class Model
    {
        public Model()
        {
            logger = LogProvider.GetLogger<Model>();
        }

        private readonly ILogger logger;

        internal Dictionary<Type, List<Entity>> instances = new Dictionary<Type, List<Entity>>();


        public IEnumerable<T> Get<T>(Func<T, bool> condition = null) where T : Entity
        {
            var qType = typeof(T);
            var types = instances.Keys.Where(t => qType.IsAssignableFrom(t));
            var entities = types
                .SelectMany(t => instances[t])
                .Cast<T>();
            if (condition == null)
                return entities;
            return entities.Where(condition);
        }

        public static Model Open(string file)
        {
            using (var s = File.OpenRead(file))
            {
                return Open(s);
            }
        }

        public static Model Open(Stream file)
        {
            var model = new Model();
            var reader = new Reader(model);
            reader.Load(file);
            return model;
        }
    }
}
