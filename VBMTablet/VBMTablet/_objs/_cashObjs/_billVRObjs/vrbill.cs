using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;
using Xamarin.Forms;

namespace VBMTablet._objs._cashObjs._billVRObjs
{
    public class BillVRObj
    {
        public int BillVRID { get; set; }
        public int ShopID { get; set; }
        public DateTime BillDate { get; set; }
        public double TgTien { get; set; }
        public string MaGiamGia { get; set; }
        public double ThanhTien { get; set; }
        public string TableNotes { get; set; }
        public string UserID { get; set; }
        public int BillType { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Status { get; set; }
        public string deliverAddress { get; set; }
        public double giamGia { get; set; }
        public DateTime henGioLayFrom { get; set; }
        public DateTime henGioLayTo { get; set; }
        public string MaBill { get; set; }
        public string SubMaBill { get; set; }
        public int PosID { get; set; }
        public int IsPaid { get; set; }
        public double ShippingFee { get; set; }
        public double ExtraShipFee { get; set; }
        public int IsPaidShipping { get; set; }
        public string HoTenKhach { get; set; }
        public string PhoneKhach { get; set; }
        public DateTime StartDeliTime { get; set; }
        public DateTime DaGiaoChoKhach { get; set; }
        public string DeliHoTen { get; set; }
        public int IsZeroContact { get; set; }
        public int UserStatus { get; set; }
        public int ThanhToanType { get; set; }
        public int BillGroupID { get; set; }
        public List<VRBillDetailObj> ListBillVRDetails { get; set; }

        public static async Task<List<BillVRObj>> getVRBills()
        {
            string url = $"http://vuabanhmi.com:6519/api/BillVR/DonCanGiao?ShopID={localdb.shopID}";
            try
            {
                using (var cl = new HttpClient())
                {
                    var resp1 = await cl.GetAsync(url);
                    var resp2 = await resp1.Content.ReadAsStringAsync();
                    var resp3 = JObject.Parse(resp2);
                    var succ = bool.Parse(tools.GetJArrayValue(resp3, "Success"));
                    if (succ)
                    {
                        var datas = tools.GetJArrayValue(resp3, "Datas");
                        var rt = JsonConvert.DeserializeObject<List<BillVRObj>>(datas);
                        return rt;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("", $"Không lấy được thông tin VR Bills: {resp3}", "OK");
                    }
                }
            }
            catch (Exception exc)
            {
                await Application.Current.MainPage.DisplayAlert("", $"Không lấy được thông tin VR Bills: {exc.Message}", "OK");
            }
            return null;
        }

    }

    public class VRBillDetailObj
    {
        public int BillVRID { get; set; }
        public int SpID { get; set; }
        public string SpName { get; set; }
        public double DonGia { get; set; }
        public int SoLg { get; set; }
        public double TgTien { get; set; }
        public double NguyenGia { get; set; }
        public string Discount { get; set; }
        public double ThanhTien { get; set; }
        public string Notes { get; set; }
    }

    public class vrBillSignalRResponseObj
    {
        public long BillVRID { get; set; }
        public List<vrBillSignalRDetailObj> lstDe { get; set; }
    }

    public class vrBillSignalRDetailObj
    {
        public long BillVRDetailID { get; set; }
        public string NVLInfos { get; set; }
        public string TenSP { get; set; }
        public List<vrBillSignalRDetailNVLInfoObj> _NVLInfos
        {
            get
            {
                var dat = NVLInfos.Replace("\\", "");
                var rt = JsonConvert.DeserializeObject<List<vrBillSignalRDetailNVLInfoObj>>(NVLInfos);
                return rt;
            }
        }
    }

    public class vrBillSignalRDetailNVLInfoObj
    {
        public string UniqueNVL { get; set; }
        public double TrongLuongPack { get; set; }
        public DateTime NSX { get; set; }
        public DateTime NHH { get; set; }
        public string BaoQuan { get; set; }
        public long BillVRID { get; set; }
        public string TenSP { get; set; }
    }

    public class VRBillStatusObj
    {
        public string Status { get; set; }

        public int StatusID { get; set; }

        public long BillID { get; set; }

        public DateTime ThoiGian { get; set; }

        public long ShopID { get; set; }
    }
}
