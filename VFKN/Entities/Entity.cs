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

        public int ID { get; set; }

        public int STAV_DAT { get; set; }
        public string DATUM_VZNIKU { get; set; }
        public string DATUM_ZANIKU { get; set; }
        public int PRIZNAK_KONTEXTU { get; set; }
        public int RIZENI_ID_VZNIKU { get; set; }
        public int RIZENI_ID_ZANIKU { get; set; }
    }
}
