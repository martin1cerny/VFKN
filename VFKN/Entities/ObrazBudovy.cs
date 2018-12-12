using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFKN.Geometry;

namespace VFKN.Entities
{
    [Entity("OB")]
    public class ObrazBudovy : EntityWithID
    {
        // ID N30;STAV_DAT N2;DATUM_VZNIKU D;DATUM_ZANIKU D;PRIZNAK_KONTEXTU N1;RIZENI_ID_VZNIKU N30;RIZENI_ID_ZANIKU N30;TYPPPD_KOD N10;SOURADNICE_Y N10.2;SOURADNICE_X N10.2;VELIKOST N10.2;UHEL N10.4;BUD_ID N30;OBRBUD_TYPE T10
        public ObrazBudovy(Model model) : base(model)
        {
        }

        public long TYPPPD_KOD;
        public float? SOURADNICE_Y;
        public float? SOURADNICE_X;
        public float? VELIKOST;
        public float? UHEL;
        public string BUD_ID;
        public string OBRBUD_TYPE;

        public Point DefinitionPoint
        {
            get
            {
                if (SOURADNICE_X.HasValue && SOURADNICE_Y.HasValue)
                    return new Point(SOURADNICE_X.Value, SOURADNICE_Y.Value);
                return null;
            }
        }

        public bool IsPerimeterPoint => OBRBUD_TYPE == "OB";
        
    }
}
