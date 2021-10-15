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

    public class extra
    {
        public long id { get; set; }
        public double price { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(names);
            }
        }
        public Dictionary<string, string> names { get; set; }
        public string alias { get; set; }
        public List<long> mons { get; set; }

        public extra clone()
        {
            var rt = new extra
            {
                alias = alias,
                id = id,
                mons = new List<long>(),
                names = new Dictionary<string, string>(names),
                price = price
            };
            mons.ForEach(p => rt.mons.Add(p));
            return rt;
        }
    }

}
