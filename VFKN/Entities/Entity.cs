using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Entities
{
    public abstract class Entity
    {
        public Model Model { get; }

        public Entity(Model model)
        {
            Model = model;
        }
    }
}
