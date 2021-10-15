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

    public class comboMenu
    {
        public long id { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(names);
            }
        }
        public Dictionary<string, string> names { get; set; }
        public double price { get; set; }
        public string combine_id { get; set; }
        public comboMenu clone()
        {
            return new comboMenu
            {
                price = price,
                combine_id = combine_id,
                id = id,
                names = new Dictionary<string, string>(names)
            };
        }

    }

}
