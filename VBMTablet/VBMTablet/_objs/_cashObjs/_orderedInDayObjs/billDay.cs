using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._utils;

namespace VBMTablet._objs._cashObjs._orderedInDayObjs
{
    public class billDay
    {
        public int BillID { get; set; }
        public int ShopID { get; set; }
        public DateTime BillDate { get; set; }
        public double TgTien { get; set; }
        public string DiscountJSON { get; set; }
        public double ThanhTien { get; set; }
        public string TableNotes { get; set; }
        public int UserID { get; set; }
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
        public int ThanhToanType { get; set; }
        public List<ListBillDetail> ListBillDetails { get; set; }
        public static async Task<List<billDay>> getBillsInDay(long shopid)
        {
            string url = $"http://vuabanhmi.com:1986/api/Bill/ListOrderedInDay?ShopID={shopid}";
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
                            var res = JsonConvert.DeserializeObject<List<billDay>>(str);
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
