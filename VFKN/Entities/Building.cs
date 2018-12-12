using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Entities
{
    /// <summary>
    /// Tabulka obsahuje stavby evidované v ISKN.
    /// Problém více čísel popisných u jedné budovy je řešen entitou Část budovy.Jed-noznačná identifikace budovy bude zajištěna interním UID.
    /// Pokud je budova rozčleněna na jednotky, může být součástí entity popis a zobra-zení rozmístění jednotek.
    /// Právní vztahy ke společným částem budovy jsou vztaženy k budově.
    /// Poznámka: Na parcelu lze zapsat maximálně jednu budovu (jedno BUD_ID). Evidují se pouze budovy, které jsou nadzemní stavbou. V případě, že budova spočívá zčás-ti na jiné budově, eviduje se i příslušné věcné břemeno. Budova na budově nepři-
    /// padá v úvahu, náš právní řád nepřipouští horizontální dělení staveb. Naopak vá-že-li se budova na více parcel, je ID_BUDOVY naplněno ve všech těchto parcelách.
    /// Poznámka: Entita CASTI_BUDOV je naplněna pouze tehdy, pokud existují alespoň 2 části budov k příslušné budově.V entitě BUDOVA je pak uloženo libovolné z těchto čísel domovních.
    /// Poznámka: Budova na cizím pozemku má jiné TEL_ID než parcela, na které leží.
    /// </summary>
    [Entity("BUD")]
    public class Building: Entity
    {
        public Building(Model model): base (model) { }

        public int TYPBUD_KOD { get; set; }
        public int CAOBCE_KOD { get; set; }
        public int CISLO_DOMOVNI { get; set; }
        public int CENA_NEMOVITOSTI { get; set; }
        public int ZPVYBU_KOD { get; set; }
        public int TEL_ID { get; set; }
        /// <summary>
        /// Identifikátor dočasné stavby
        /// </summary>
        public string DOCASNA_STAVBA { get; set; }
        /// <summary>
        /// Indikátor stavby, je-li součástí
        /// pozemku
        /// </summary>
        public string JE_SOUCASTI { get; set; }
        /// <summary>
        /// Odkaz na unikátní generované číslo
        /// práva stavby
        /// </summary>
        public int PS_ID { get; set; }
    }
}
