using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Entities
{
    /// <summary>
    /// Datový blok obsahuje odkazy na adresy budov, které jsou obsaženy v bloku nemovi-tostí. 
    /// Tento datový blok obsahuje vždy aktuální data bez ohledu na datum, ke kterému je export 
    /// NVF vytvořen (nepracuje s historií).
    /// </summary>
    [Entity("ADROBJ")]
    public class Adresa : Entity
    {
        public Adresa(Model model) : base(model)
        {
        }

        public int KOD;
        public int OBJEKT_KOD;
        public int? ULICE_KOD;
        public string CIS_ORIENT;
        public int PSC;
        public string PLATNOST_OD;
        public string PLATNOST_DO;
        public string ULICE_NAZEV;

    }
}
