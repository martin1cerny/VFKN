using System;
using System.Collections.Generic;
using System.Text;
using VFKN.Geometry;

namespace VFKN.Entities
{
    [Entity("SOBR")]
    public class SouradniceObrazu : Entity
    {
        public SouradniceObrazu(Model model) : base(model)
        {
        }

        public string ID;
        public int? STAV_DAT;
        public int? KATUZE_KOD;
        public int? CISLO_ZPMZ;
        public int? CISLO_TL;
        public long CISLO_BODU;
        public long? UPLNE_CISLO;
        public float SOURADNICE_Y;
        public float SOURADNICE_X;
        public int? KODCHB_KOD;

        public Point Point
        {
            get
            {
                return new Point(SOURADNICE_X, SOURADNICE_Y);
            }
        }
    }
}
