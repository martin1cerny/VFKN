﻿using System;
using System.Collections.Generic;
using System.Text;
using VFKN.Extensions;

namespace VFKN.Entities
{
    public abstract class EntityWithState: Entity
    {
        public EntityWithState(Model model) : base(model)
        {
        }

        public int? STAV_DAT;
        public string DATUM_VZNIKU;
        public string DATUM_ZANIKU;
        public int? PRIZNAK_KONTEXTU;
        public string RIZENI_ID_VZNIKU;
        public string RIZENI_ID_ZANIKU;

        public DateTime? DatumVzniku => DATUM_VZNIKU.VfkToDateTime();
        public DateTime? DatumZaniku => DATUM_ZANIKU.VfkToDateTime();
        
    }
}
