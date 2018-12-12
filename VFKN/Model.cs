using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using VFKN.Entities;

namespace VFKN
{
    public class Model
    {
        public Model()
        {
            logger = LogProvider.GetLogger<Model>();
        }

        private readonly ILogger logger;

        internal Dictionary<Type, Dictionary<int, Entity>> instances = new Dictionary<Type, Dictionary<int, Entity>>();

        

        public IEnumerable<T> Get<T>(Func<T, bool> condition = null) where T : Entity
        {
            var qType = typeof(T);
            var types = instances.Keys.Where(t => qType.IsAssignableFrom(t));
            var entities = types
                .Select(t => instances[t]).SelectMany(d => d.Values)
                .Cast<T>();
            if (condition == null)
                return entities;
            return entities.Where(condition);
        }

        public T Get<T>(int ID) where T : Entity
        {
            var qType = typeof(T);
            if (!instances.TryGetValue(qType, out Dictionary<int, Entity> entities))
                return null;
            if (!entities.TryGetValue(ID, out Entity entity))
                return null;
            return entity as T;
        }
    }
}
