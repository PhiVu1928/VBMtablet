using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._menuObjs
{
    public class drink_combo
    {
        public long spId { get; set; }
        public Dictionary<string, string> name { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(name);
            }
        }
        public long classID { get; set; }
        public Dictionary<string, string> className { get; set; }
        public string classNameVn
        {
            get
            {
                return tools.getVNName(className);
            }
        }
        public string img { get; set; }
        public double nguyengia { get; set; }
        public double dongia { get; set; }

        public drink_combo clone()
        {
            return new drink_combo { classID = classID, className = className, dongia = dongia, nguyengia = nguyengia, img = img, name = new Dictionary<string, string>(name), spId = spId };
        }

        public static async Task<List<drink_combo>> getLstDrCbs(long id)
        {
            string url = $"{localdb.endpoin}drinkForCombo?channel=1&mainID={id}";

            if (tools.isConn())
            {
                try
                {
                    using (var cl = new HttpClient())
                    {
                        var res1 = await cl.GetAsync(url);
                        var res2 = await res1.Content.ReadAsStringAsync();
                        var jOb = JObject.Parse(res2);
                        var success = bool.Parse(tools.GetJArrayValue(jOb, "Success"));
                        if (success)
                        {
                            var datas = tools.GetJArrayValue(jOb, "Datas");
                            var res = JsonConvert.DeserializeObject<List<drink_combo>>(datas);
                            return res;
                        }
                    }
                }
                catch (Exception e)
                {
                    
                }

            }
            else
            {

            }
            return null;
        }

    }
}

