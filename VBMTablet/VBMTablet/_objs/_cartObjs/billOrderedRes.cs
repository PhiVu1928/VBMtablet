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
    public class BillOrderReturn
    {
        public long BillID { get; set; }

        public long ShopID { get; set; }

        public DateTime BillDate { get; set; }

        public double TgTien { get; set; }

        public string DiscountJSON { get; set; }

        public double ThanhTien { get; set; }

        public string TableNotes { get; set; }

        public long UserID { get; set; }

        public int BillType { get; set; }

        public long BillTicks { get; set; }

        public string Datitude { get; set; }

        public string Dlongtitude { get; set; }

        public int Status { get; set; }

        public string deliverAddress { get; set; }

        public double giamGia { get; set; }

        public DateTime henGioLay { get; set; }

        public int isHenGioLay { get; set; }

        public string MaBill { get; set; }

        public int PosID { get; set; }

        public int IsPaid { get; set; }

        public string HoTenKhach { get; set; }

        public string PhoneKhach { get; set; }

        public DateTime StartDeliTime { get; set; }

        public int IsZeroContact { get; set; }

        public int UserStatus { get; set; }

        public bool IsRated { get; set; }

        public int productS { get; set; }
        public int serviceS { get; set; }
        public int peopleS { get; set; }
        public int timeS { get; set; }
        public string KHcomment { get; set; }

        public int predictEDTSec { get; set; }

        public DateTime predictEDTTime { get; set; }

        public int numOfTurn { get; set; }

        public static async Task<BillOrderReturn> getBillThroughUniqueID(string unique)
        {
            if (tools.isConn())
            {
                string url = $"http://vuabanhmi.com:1986/api/Bill/GetBillByUniqueID?uniqueID={unique}";
                try
                {
                    using (var cl = new HttpClient())
                    {
                        var res1 = await cl.GetAsync(url);
                        var res2 = await res1.Content.ReadAsStringAsync();
                        var res3 = JObject.Parse(res2);
                        bool succ = bool.Parse(tools.GetJArrayValue(res3, "Success"));
                        if (succ)
                        {
                            var data = tools.GetJArrayValue(res3, "Data");
                            return JsonConvert.DeserializeObject<BillOrderReturn>(data);
                        }
                        else
                        {
                            //log error
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
