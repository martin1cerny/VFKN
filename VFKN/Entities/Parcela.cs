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
    }
}
