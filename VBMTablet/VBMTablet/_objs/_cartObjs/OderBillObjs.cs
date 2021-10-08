using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;
using Xamarin.Forms;

namespace VBMTablet._objs._cartObjs
{
    public class BillCreateDoAddBill
    {
        public int ShopID { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime time_payment { get; set; }
        public double TgTien { get; set; }
        public string DiscountJSON { get; set; }
        public double ThanhTien { get; set; }
        public int BillType { get; set; }
        public string TableNotes { get; set; }
        public int UserID { get; set; }
        public string Datitude { get; set; }
        public string Dlongtitude { get; set; }
        public string deliverAddress { get; set; }
        public double giamGia { get; set; }
        public DateTime henGioLay { get; set; }
        public int isHenGioLay { get; set; }
        public string MaBill { get; set; }
        public int PosID { get; set; }
        public int IsPaid { get; set; }
        public int orderedUserID { get; set; }
        public int IsZeroContact { get; set; }
        public string UniqueID { get; set; }
        public string Token { get; set; }
        public List<BillDetailsCreateDoAddBill> ListBillDetails { get; set; }
        public List<Components> lstAddData { get; set; }

        public static BillCreateDoAddBill createDoAddBill(string tableNote, int noContact)
        {
            string discountJson = "";
            foreach (var item in localdb.CartProd.Where(p => p.orderType > 0 || p.orderType == -55555).ToList())
            {
                var dcs = discountJson.Split(' ');
                if (!dcs.Contains(item.orderCode))
                    discountJson += item.orderCode + " ";
            }
            var gifts = localdb.CartProd.Where(p => p.orderType < 0 && p.orderType != -55555).ToList();
            if (gifts.Count > 0)
            {
                discountJson += "[reward =";
                foreach (var item in gifts)
                {
                    discountJson += item.orderCode + "#";
                }
                discountJson += "]";
            }
            var price = localdb.thanh_Toan_Page.vmcart.CallMoney();
            var res = new BillCreateDoAddBill
            {
                BillDate = DateTime.Now,
                BillType = 0,
                Datitude = "",
                deliverAddress = "",
                DiscountJSON = discountJson,
                Dlongtitude = "",
                giamGia = price.Item1 - price.Item2,
                henGioLay = DateTime.Now,
                isHenGioLay = 0,
                IsPaid = -1,
                IsZeroContact = noContact,
                ListBillDetails = new List<BillDetailsCreateDoAddBill>(),
                lstAddData = new List<Components>(),
                MaBill = "",
                orderedUserID = 3451,
                UserID = 3451,
                PosID = 0,
                ShopID = 2,
                TableNotes = tableNote,
                TgTien = price.Item1,
                ThanhTien = price.Item2,
                time_payment = DateTime.Now,
                UniqueID = Guid.NewGuid().ToString("N"),
                Token = ""
            };
            foreach (var item in localdb.CartProd)
            {
                res.ListBillDetails.Add(new BillDetailsCreateDoAddBill(item));
                foreach (var t2 in item.LstExts)
                {
                    res.ListBillDetails.Add(new BillDetailsCreateDoAddBill(t2, item.slg * t2.solg));
                }
                foreach (var t2 in item.LstSpls)
                {
                    res.ListBillDetails.Add(new BillDetailsCreateDoAddBill(t2, item.slg));
                }
            }

            string _token = res.BillDate.Ticks + "|" + res.UserID + "|" + res.orderedUserID + "|" + res.ShopID;
            _token = tools.EncryptAES(_token);
            _token = tools.Base64Encode(_token);
            res.Token = _token;

            return res;
        }

        public static async Task<BillOrderReturn> orderAction(BillCreateDoAddBill bill)
        {
            string url = "http://vuabanhmi.com:1986/api/Bill/AddNew4";
            string dt = JsonConvert.SerializeObject(bill);
            if (localdb.isdeburg)
            {
                var ques = await Application.Current.MainPage.DisplayAlert("Ban dat hang chu?", dt, "OK", "No");
                if (!ques)
                {
                    return null;
                }
            }
            if (!tools.isConn())
            {
                await Application.Current.MainPage.DisplayAlert("Không có kết nối internet", "Vui lòng kiểm tra kêt nối internet và đặt hàng lại", "OK");
                return null;
            }
            try
            {
                using (var client = tools.createHttpClient())
                {
                    var res1 = await client.PostAsync(url,
                         new StringContent(dt, Encoding.UTF8, "application/json"));
                    string res2 = await res1.Content.ReadAsStringAsync();
                    var job = JObject.Parse(res2);
                    var success = bool.Parse(tools.GetJArrayValue(job, "Success"));
                    if (success)
                    {
                        var data = tools.GetJArrayValue(job, "Data");
                        return JsonConvert.DeserializeObject<BillOrderReturn>(data);
                    }
                    else
                    {
                        
                    }
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Loi", e.ToString(), "Ok");
            }
            return null;
        }

    }

    public class BillDetailsCreateDoAddBill
    {
        public int SpID { get; set; }
        public string SpName { get; set; }
        public double DonGia { get; set; }
        public int SoLg { get; set; }
        public string Discount { get; set; }
        public string Notes { get; set; }
        public double NguyenGia { get; set; }
        public int IsExtra { get; set; }
        public string AliasTenSP { get; set; }

        public BillDetailsCreateDoAddBill(CartProd mc)
        {
            string note = "";
            if (mc.isGetLater)
            {
                note = "dd";
            }
            AliasTenSP = "";
            Discount = mc.orderCode;
            DonGia = mc.dongia;
            IsExtra = 0;
            NguyenGia = mc.nguyengia;
            Notes = note;
            SoLg = mc.slg;
            SpID = (int)mc.id;
            SpName = "";
            var eme = CartProd.getDetailInfo(mc.id);
            if (eme != null)
            {
                foreach (var item in eme.lst_size)
                {
                    if (item.id == mc.id)
                    {
                        SpName = item.nameVN;
                        break;
                    }
                }
                if (SpName == "")
                {
                    foreach (var item in eme.lst_combo)
                    {
                        if (item.id == mc.id)
                        {
                            SpName = item.nameVN;
                            break;
                        }
                    }
                }
            }
        }

        public BillDetailsCreateDoAddBill(cartExtra ex, int slg)
        {
            AliasTenSP = "";
            Discount = "";
            DonGia = ex.dongia;
            IsExtra = 1;
            NguyenGia = ex.nguyengia;
            Notes = "";
            SoLg = slg;
            SpID = (int)ex.id;
            SpName = "";
            try
            {
                SpName = ex.name["vi"];
            }
            catch { }
        }

        public BillDetailsCreateDoAddBill(cartSpice spi, int slg)
        {
            AliasTenSP = "";
            Discount = "";
            DonGia = 0;
            IsExtra = 1;
            NguyenGia = 0;
            Notes = "";
            SoLg = slg;
            SpID = (int)spi.id;
            SpName = "";
            try
            {
                SpName = spi.name["vi"];
            }
            catch { }
        }

    }
}
