using VFKN.Geometry;

namespace VFKN.Entities
{
    [Entity("OP")]
    public class ObrazParcely : EntityWithID
    {
        public ObrazParcely(Model model) : base(model)
        {
        }

        public long TYPPPD_KOD;
        public float? SOURADNICE_Y;
        public float? SOURADNICE_X;
        public string TEXT;
        public float? VELIKOST;
        public float? UHEL;
        public string PAR_ID;
        public OPAR_TYPE? OPAR_TYPE;
        public int VZTAZNY_BOD;

        public Point Point
        {
            get
            {
                if (SOURADNICE_X.HasValue && SOURADNICE_Y.HasValue)
                    return new Point(SOURADNICE_X.Value, SOURADNICE_Y.Value);
                return null;
            }
        }

        // OPAR_TYPE:
        // PC  - Parcelní číslo.
        // PPC - Popisné parcelní číslo.
        // SPC - Šipka k popisnému číslu.
        // ZDP - Značka druhu pozemku.


    }

    public enum OPAR_TYPE
    {
        PC,
        PPC,
        SPC,
        ZDP
    }
}
