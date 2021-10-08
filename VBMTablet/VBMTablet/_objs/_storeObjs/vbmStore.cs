using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._storeObjs
{
    public class storeObj
    {
        public int ShopID;
        public string ShopName;
        public string DiaChi;
        public string AvatarStore;
        public string QRCodeStore;
        public int IsAllowShipRequestOrder;
        public string LatitudeStore;
        public string LongtitudeStore;
        public string OpenHourStore;
        public string CloseHourStore;
        public double DistanceAllow;

        public static async Task<List<storeObj>> getLstStores()
        {
            string url = $"{localdb.endpoin}vbm_stores";
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
                            var res = JsonConvert.DeserializeObject<List<storeObj>>(str);
                            return res;
                        }
                        else
                        {
                            //log lai loi
                        }
                    }
                }
                catch (Exception exc)
                {
                    //log lai e.message
                }
            }
            else
            {
                //log lai mat ket noi
            }
            return null;
        }

    }
}
