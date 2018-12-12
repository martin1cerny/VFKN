namespace VFKN.Entities
{
    [Entity("OPSUB")]
    public class OpravnenySubjekt : EntityWithState
    {
        // &HOPSUB;ID T255;STAV_DAT N2;DATUM_VZNIKU D;DATUM_ZANIKU D;PRIZNAK_KONTEXTU N1;RIZENI_ID_VZNIKU N30;RIZENI_ID_ZANIKU N30;
        // ID_JE_1_PARTNER_BSM T255;ID_JE_2_PARTNER_BSM T255;ID_ZDROJ T255;OPSUB_TYPE T10;CHAROS_KOD N2;ICO N8;DOPLNEK_ICO N3;NAZEV T255;
        // NAZEV_U T255;RODNE_CISLO T10;TITUL_PRED_JMENEM T35;JMENO T100;JMENO_U T100;PRIJMENI T100;PRIJMENI_U T100;TITUL_ZA_JMENEM T10;CISLO_DOMOVNI N4;
        // CISLO_ORIENTACNI T4;NAZEV_ULICE T48;CAST_OBCE T48;OBEC T48;OKRES T32;STAT T100;PSC N5;MESTSKA_CAST T48;CP_CE N1;DATUM_VZNIKU2 D;RIZENI_ID_VZNIKU2 N30;KOD_ADRM N9;ID_NADRIZENE_PO T255
        public OpravnenySubjekt(Model model) : base(model)
        {
        }

        public string ID;
        public string ID_JE_1_PARTNER_BSM;
        public string ID_JE_2_PARTNER_BSM;
        public string ID_ZDROJ;
        public string OPSUB_TYPE;
        public int CHAROS_KOD;
        public int ICO;
        public int DOPLNEK_ICO;
        public string NAZEV;
        public string NAZEV_U;
        public string RODNE_CISLO;
        public string TITUL_PRED_JMENEM;
        public string JMENO;
        public string JMENO_U;
        public string PRIJMENI;
        public string PRIJMENI_U;
        public string TITUL_ZA_JMENEM;
        public int CISLO_DOMOVNI;
        public string CISLO_ORIENTACNI;
        public string NAZEV_ULICE;
        public string CAST_OBCE;
        public string OBEC;
        public string OKRES;
        public string STAT;
        public int PSC;
        public string MESTSKA_CAST;
        public int CP_CE;
        public string DATUM_VZNIKU2;
        public string RIZENI_ID_VZNIKU2;
        public int KOD_ADRM;
        public string ID_NADRIZENE_PO;
    }
}
