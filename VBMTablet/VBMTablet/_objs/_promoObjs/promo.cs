using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._promoObjs
{
    public class promotionObjs
    {
        public int id { get; set; }
        public string code { get; set; }
        public Dictionary<string, string> imgs { get; set; }
        public string imgVN
        {
            get
            {
                return tools.getVNName(imgs);
            }
        }
        public Dictionary<string, string> names { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(names);
            }
        }
        public Dictionary<string, string> urls { get; set; }
        public string urlVN
        {
            get
            {
                return tools.getVNName(urls);
            }
        }
        public Dictionary<string, string> descs { get; set; }
        public string descVN
        {
            get
            {
                return tools.getVNName(descs);
            }
        }

        public static async Task<List<promotionObjs>> getPromotions()
        {
            if (tools.isConn())
            {
                try
                {
                    using (var cl = tools.createHttpClient())
                    {
                        string url = $"{localdb.endpoin}all_promotions?channel=2";
                        var res1 = await cl.GetAsync(url);
                        var res2 = await res1.Content.ReadAsStringAsync();
                        var job = JObject.Parse(res2);
                        var success = bool.Parse(tools.GetJArrayValue(job, "Success"));
                        if (success)
                        {
                            var datas = tools.GetJArrayValue(job, "Datas");
                            var res = JsonConvert.DeserializeObject<List<promotionObjs>>(datas);
                            localdb.promotionObjs = res;
                            return res;
                        }
                        else
                        {
                            //can log lai
                        }
                    }
                }
                catch (Exception exc)
                {
                   // can log lai
                }
            }
            else
            {
                //can log lai
            }
            return null;
        }

        public static async Task<promoValidStatus> checkValidPromo(long shopID, long promoId, int isHengio, string hengio) //dd/MM/yy HH:mm
        {
            if (tools.isConn())
            {
                try
                {
                    using (var cl = tools.createHttpClient())
                    {
                        string url = $"{localdb.endpoin}CheckPromotionInUse?promoid={promoId}&shopid={shopID}&isHengio={isHengio}&hengio={hengio}";
                        var res1 = await cl.GetAsync(url);
                        var res2 = await res1.Content.ReadAsStringAsync();
                        var job = JObject.Parse(res2);
                        var success = bool.Parse(tools.GetJArrayValue(job, "Success"));
                        var data = tools.GetJArrayValue(job, "Data");
                        var rt = JsonConvert.DeserializeObject<promoValidStatus>(data);
                        if (success)
                        {
                            rt.isValid = true;
                        }
                        else
                        {
                            rt.isValid = false;
                        }
                        return rt;
                    }
                }
                catch (Exception exc)
                {
                    // can log lai
                }
            }
            else
            {
                // can log lai
            }
            return null;
        }

    }

}
