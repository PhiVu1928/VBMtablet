using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._menuObjs
{

    public class spiceItem
    {
        public long ref_id { get; set; }
        public long id { get; set; }
        public int size { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(names);
            }
        }
        public Dictionary<string, string> names { get; set; }
        public string alias { get; set; }
        public double price { get; set; }

        public spiceItem clone()
        {
            return new spiceItem
            {
                alias = alias,
                id = id,
                names = new Dictionary<string, string>(names),
                size = size,
                price = price,
                ref_id = ref_id
            };
        }
    }

}
