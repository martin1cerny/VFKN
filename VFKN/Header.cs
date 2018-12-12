using System.Collections.Generic;
using VFKN.Entities;

namespace VFKN
{
    public class Header
    {
        public string VERZE;
        public string VYTVORENO;
        public string PUVOD;
        public string CODEPAGE;
        public List<string> SKUPINA = new List<string>();
        public string JMENO;
        public List<string> PLATNOST = new List<string>();
        public int ZMENY;
        public string PM;
        public List<KatastralniUzemi> KATUZE = new List<KatastralniUzemi>();
        public List<OpravnenySubjekt> OPSUB = new List<OpravnenySubjekt>();
        public int POLYG;
        public List<float[]> POLYGDATA;
        public int OSOBDATA;
    }
}
