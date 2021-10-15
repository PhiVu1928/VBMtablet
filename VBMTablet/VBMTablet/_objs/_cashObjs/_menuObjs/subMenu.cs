using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._menuObjs
{

    public class subMenu
    {
        public int id { get; set; }
        public int index { get; set; }
        public Dictionary<string, string> names { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(names);
            }
        }
        public List<eMenu> lst_emes { get; set; }
    }

}
