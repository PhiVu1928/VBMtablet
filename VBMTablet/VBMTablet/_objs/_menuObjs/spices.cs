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

    public class spices
    {
        public long ref_id { get; set; }
        public long id_default { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(names);
            }
        }
        public Dictionary<string, string> names { get; set; }
        public List<long> mons { get; set; }
        public List<spiceItem> lst_items { get; set; }

        public spices clone()
        {
            var rt = new spices
            {
                id_default = id_default,
                lst_items = new List<spiceItem>(),
                mons = new List<long>(),
                names = new Dictionary<string, string>(names),
                ref_id = ref_id
            };
            mons.ForEach(p => rt.mons.Add(p));
            lst_items.ForEach(p => rt.lst_items.Add(p.clone()));
            return rt;
        }

    }

}
