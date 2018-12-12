using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Entities
{
    [Entity("SBP")]
    public class SpojeniBoduPolohopisu : EntityWithID
    {
        // SBP;ID N30;STAV_DAT N2;DATUM_VZNIKU D;DATUM_ZANIKU D;PRIZNAK_KONTEXTU N1;RIZENI_ID_VZNIKU N30;RIZENI_ID_ZANIKU N30;BP_ID N30;PORADOVE_CISLO_BODU N38;OB_ID N30;HP_ID N30;DPM_ID N30;PARAMETRY_SPOJENI T100;ZVB_ID N30
        public SpojeniBoduPolohopisu(Model model) : base(model)
        {
        }

        public string BP_ID;
        public string PORADOVE_CISLO_BODU;
        public string OB_ID;
        public string HP_ID;
        public string DPM_ID;
        public string PARAMETRY_SPOJENI;
        public string ZVB_ID;
    }
}
