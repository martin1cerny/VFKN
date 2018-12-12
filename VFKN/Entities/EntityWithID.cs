using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Entities
{
    public abstract class EntityWithID : EntityWithState
    {
        public EntityWithID(Model model) : base(model)
        {
        }

        public string ID;
    }
}
