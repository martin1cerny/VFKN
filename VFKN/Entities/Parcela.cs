using System.Linq;
using VFKN.Geometry;

namespace VFKN.Entities
{
    [Entity("PAR")]
    public class Parcela : EntityWithID
    {
        public Parcela(Model model) : base(model)
        {
        }

        public string PKN_ID;
        public string PAR_TYPE;
        public int KATUZE_KOD;
        public int KATUZE_KOD_PUV;
        public int DRUH_CISLOVANI_PAR;
        public int KMENOVE_CISLO_PAR;
        public int ZDPAZE_KOD ;
        public int PODDELENI_CISLA_PAR;
        public int DIL_PARCELY;
        public string MAPLIS_KOD;
        public int ZPURVY_KOD;
        public int DRUPOZ_KOD;
        public int ZPVYPA_KOD;
        public int TYP_PARCELY;
        public int VYMERA_PARCELY;
        public float CENA_NEMOVITOSTI;
        public string DEFINICNI_BOD_PAR;
        public string TEL_ID;
        public string PAR_ID;
        public string BUD_ID;
        public string IDENT_BUD;

        public Point DefinitionPoint
        {
            get
            {
                var obr = Model.Get<ObrazParcely>(o => o.PAR_ID == ID && o.OPAR_TYPE == OPAR_TYPE.PC).FirstOrDefault();
                if (obr == null)
                    return null;
                return obr.Point;
            }
        }

        public Polygon Polygon
        {
            get
            {
                var spoj = Model.Get<ObrazParcely>(o => o.PAR_ID == ID)
                    .SelectMany(o => Model.Get<SpojeniBoduPolohopisu>(s => s.OB_ID == o.ID))
                    .Where(s => s != null)
                    .OrderBy(s => s.PORADOVE_CISLO_BODU)
                    .Select(s => Model.Get<SouradniceObrazu>(o => o.ID == s.BP_ID).FirstOrDefault())
                    .Where(o => o != null)
                    .Select(o => o.Point)
                    .ToList();

                if (spoj.Count == 0)
                    return null;

                return new Polygon { Points = spoj };
            }
        }
    }
}
