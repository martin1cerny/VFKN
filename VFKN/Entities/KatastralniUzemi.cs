using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Entities
{
    [Entity("KATUZE")]
    public class KatastralniUzemi : Entity
    {
        public KatastralniUzemi(Model model) : base(model)
        {
        }

        public int KOD;
        public int OBCE_KOD;
        public string NAZEV;
        public string PLATNOST_OD;
        public string PLATNOST_DO;
        public int? CISLO;
        public int CISELNA_RADA;
    }
}
