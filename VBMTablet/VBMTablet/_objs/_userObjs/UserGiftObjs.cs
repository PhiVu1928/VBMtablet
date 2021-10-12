using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._userObjs
{
	public class UserGiftObjs
	{
		public List<giftObjs> lst_gifts { get; set; }
		public List<bmls_obj> lst_bmls { get; set; }
        public static async Task<UserGiftObjs> getUserGiftData(string sdt,long userid)
        {
            string url = $"{localdb.endpoin}get_gifts_bmls?channel=2&sdt={sdt}&userid={userid}";
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
                            var res = JsonConvert.DeserializeObject<UserGiftObjs>(str);
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
    }
}
