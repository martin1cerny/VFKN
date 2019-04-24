using System;
using System.Collections.Generic;
using System.Text;

namespace VFKN.Entities
{
    [Entity("BUDOBJ")]
    public class Bud_Obj : Entity
    {
        public Bud_Obj(Model model) : base(model)
        {
        }

        public int CISDOM_HOD;
        public string ID_KN;
        public string CB_KN;
        public int? ID_UA;

    }
}
