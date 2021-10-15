using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._cartObjs
{
    public class orderConditionStatus
    {
        public bool isOK { get; set; }
        public Dictionary<string, string> msg { get; set; }
        public int isHengio { get; set; }
        public DateTime hengio { get; set; }
        public long shopId { get; set; }
        public long userID { get; set; }
        public long tgTien { get; set; }
        public long thanhTien { get; set; }
        public int billtype { get; set; }
        public double distance { get; set; }
        public List<CartProd> prods { get; set; }

        public static async Task<orderConditionStatus> checkOrderCondition(orderConditionStatus info)
        {
            orderConditionStatus result = null;

            string url = $"{localdb.endpoin}checkOrderCondition";
            if (!tools.isConn())
            {
                
            }
            else
            {
                try
                {
                    using (var cl = new HttpClient())
                    {
                        string body = JsonConvert.SerializeObject(info);
                        var res1 = await cl.PutAsync(url,
                        new StringContent(body, Encoding.UTF8, "application/json"));
                        string res2 = await res1.Content.ReadAsStringAsync();
                        var job = JObject.Parse(res2);
                        var success = bool.Parse(tools.GetJArrayValue(job, "Success"));
                        if (success)
                        {
                            var data = tools.GetJArrayValue(job, "Data");
                            result = JsonConvert.DeserializeObject<orderConditionStatus>(data);
                            if (result.isOK)
                            {
                                return result;
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
            return result;
        }

    }
}
