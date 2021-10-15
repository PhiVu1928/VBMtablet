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

    public class eMenu
    {
        public int class_id { get; set; }
        public int sub_id { get; set; }
        public int index { get; set; }
        public int type_eme { get; set; }
        public string img { get; set; }
        public bool is_has_combo { get; set; }
        public List<sizeMenu> lst_size { get; set; }
        public List<comboMenu> lst_combo { get; set; }
        public List<int> lst_unsell_store { get; set; }
        public Dictionary<string, string> names { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(names);
            }
        }
        public List<string> banners { get; set; }
        public int HotTypeID { get; set; } //HotTypeID: 0 là ko có gì, 1 là bán chạy, 2 là new

        public eMenu clone()
        {
            var rt = new eMenu
            {
                index = index,
                class_id = class_id,
                type_eme = type_eme,
                img = img,
                is_has_combo = is_has_combo,
                names = new Dictionary<string, string>(names),
                lst_size = new List<sizeMenu>(),
                lst_combo = new List<comboMenu>(),
                lst_unsell_store = new List<int>(),
                banners = new List<string>(),
                sub_id = sub_id,
                HotTypeID = HotTypeID
            };
            lst_size.ForEach(p => rt.lst_size.Add(p.clone()));
            lst_combo.ForEach(p => rt.lst_combo.Add(p.clone()));
            lst_unsell_store.ForEach(p => rt.lst_unsell_store.Add(p));
            banners.ForEach(p => rt.banners.Add(p));
            return rt;
        }

    }

}
