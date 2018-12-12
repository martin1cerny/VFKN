using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Entities
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class EntityAttribute: Attribute
    {
        public EntityAttribute(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
