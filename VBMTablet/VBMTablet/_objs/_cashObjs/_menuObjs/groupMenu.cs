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
    public class groupMenu
    {
        public int menuVersion { get; set; }
        public long id { get; set; }
        public int index { get; set; }
        public string nameVN 
        {
            get
            {
                return tools.getVNName(names);
            }
        }
        public Dictionary<string, string> names { get; set; }
        public List<subMenu> lst_sub_menu { get; set; }

        public static async Task<List<groupMenu>> getMenuData()
        {
            string url = $"{localdb.endpoin}get_menu_data?channel=2";
            if (tools.isConn())
            {
                try
                {
                    using (var cl = tools.createHttpClient())
                    {
                        var resp = await cl.GetAsync(url);
                        var data = await resp.Content.ReadAsStringAsync();
                        var jOb = JObject.Parse(data);
                        var isSuccess = bool.Parse(tools.GetJArrayValue(jOb, "Success"));
                        if (isSuccess)
                        {
                            var str = tools.GetJArrayValue(jOb, "Datas");
                            var res = JsonConvert.DeserializeObject<List<groupMenu>>(str);
                            localdb.groupMenus = res;
                            return res;
                        }
                    }
                }
                catch (Exception e)
                {
                    // o day can log lai su kien
                }
            }
            else
            {
                // o day can log lai su kien
            }
            return null;
        }

        public static eMenu searchMenu(long id)
        {
            foreach (var t1 in localdb.groupMenus)
            {
                foreach (var t2 in t1.lst_sub_menu)
                {
                    foreach (var t3 in t2.lst_emes)
                    {
                        foreach (var t4 in t3.lst_size)
                        {
                            if (t4.id == id)
                            {
                                return t3;
                            }
                        }
                        foreach (var t4 in t3.lst_combo)
                        {
                            if (t4.id == id)
                            {
                                return t3;
                            }
                        }
                    }
                }
            }
            return null;
        }

    }
}
