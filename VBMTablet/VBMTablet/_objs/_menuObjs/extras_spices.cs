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

    public class extra_spices
    {
        public int version { get; set; }
        public List<extra> extras { get; set; }
        public List<spices> spices { get; set; }

        public static async Task<extra_spices> getExsSpisData()
        {
            string url = $"{localdb.endpoin}get_all_extras_spices";
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
                            var str = tools.GetJArrayValue(jOb, "Data");
                            var rt = JsonConvert.DeserializeObject<extra_spices>(str);
                            localdb.extra_Spices = rt;
                            return rt;
                        }
                    }
                }
                catch { }
            }
            return null;
        }

        public extra findoutExtra(long id)
        {
            var ext = extras.Where(p => p.id == id).FirstOrDefault();
            return ext;
        }

        public spiceItem findoutSpice(long id)
        {
            foreach (var t1 in spices)
            {
                foreach (var t2 in t1.lst_items)
                {
                    if (t2.id == id)
                    {
                        return t2;
                    }
                }
            }
            return null;
        }

    }

}
