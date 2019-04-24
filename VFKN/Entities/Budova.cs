using System.Collections.Generic;
using System.Linq;
using VFKN.Geometry;

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
    public class Budova : EntityWithID
    {
        public Budova(Model model) : base(model) { }

        public int TYPBUD_KOD;
        public int? CAOBCE_KOD;
        public int? CISLO_DOMOVNI;
        public float? CENA_NEMOVITOSTI;
        public int? ZPVYBU_KOD;
        public string TEL_ID;
        /// <summary>
        /// Identifikátor dočasné stavby
        /// </summary>
        public string DOCASNA_STAVBA;
        /// <summary>
        /// Indikátor stavby, je-li součástí
        /// pozemku
        /// </summary>
        public string JE_SOUCASTI;
        /// <summary>
        /// Odkaz na unikátní generované číslo
        /// práva stavby
        /// </summary>
        public string PS_ID;

        public Point DefinitionPoint
        {
            get
            {
                var obr = Model.Get<ObrazBudovy>(o => o.BUD_ID == ID && !o.IsPerimeterPoint).FirstOrDefault();
                if (obr == null)
                    return null;
                return obr.Point;
            }
        }

        public Polygon Polygon
        {
            get
            {
                var spoj = Model.Get<ObrazBudovy>(o => o.BUD_ID == ID && o.IsPerimeterPoint)
                    .SelectMany(o => Model.Get<SpojeniBoduPolohopisu>(s => s.OB_ID == o.ID))
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

        public Parcela Parcela
        {
            get
            {
                return Model.Get<Parcela>(p => p.BUD_ID == ID).FirstOrDefault();
            }
        }

        public IEnumerable<Adresa> Address
        {
            get
            {
                var ids = new HashSet<int>( Model
                    .Get<Bud_Obj>(bo => bo.ID_KN == ID)
                    .Where(bo => bo.ID_UA.HasValue)
                    .Select(bo => bo.ID_UA.Value));
                return Model.Get<Adresa>(a => ids.Contains(a.OBJEKT_KOD));
            }
        }
    }
}
