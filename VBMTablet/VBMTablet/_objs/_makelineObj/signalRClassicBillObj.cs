using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace VBMTablet._objs._makelineObj
{
    public class signalRClassicBillObj
    {
        public long billID { get; set; }
        public long shopID { get; set; }
        public DateTime billDate { get; set; }
        public double tgtien { get; set; }
        public double thanhtien { get; set; }
        public string discountJson { get; set; }
        public string tableNote { get; set; }
        public string orderUserID { get; set; }
        public string userID { get; set; }
        public int billType { get; set; }
        public long billTicks { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public string maBill { get; set; }
        public int isHenGio { get; set; }
        public DateTime gioHen { get; set; }
        public int tgToRoi { get; set; }
        public int isZeroContact { get; set; }
        public int thanhToanType { get; set; }
        public int posID { get; set; }
        public string fullNameKhach { get; set; }
        public string sdtKhach { get; set; }
        public double datraV { get; set; }
        public int isPaid { get; set; }
        public int Status { get; set; }
        public string deliverAddress { get; set; }
        public List<classicBillDetailObj> details { get; set; }

        public static signalRClassicBillObj parseNewBill(string data)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            signalRClassicBillObj obj = null;
            try
            {
                obj = new signalRClassicBillObj();
                string[] arr = data.Split('#');
                obj.billID = long.Parse(arr[0]);
                obj.maBill = arr[1];
                obj.Status = int.Parse(arr[2]);
                obj.orderUserID = arr[3];
                obj.billType = int.Parse(arr[4]);
                obj.billDate = DateTime.ParseExact(arr[5], "dd/MM/yyyy HH:mm", provider);
                obj.tgtien = double.Parse(arr[6]);
                obj.thanhtien = double.Parse(arr[7]);
                obj.tableNote = arr[8];
                obj.billTicks = long.Parse(arr[9]);
                obj.shopID = long.Parse(arr[10]);
                obj.fullNameKhach = arr[12];
                obj.sdtKhach = arr[13];
                try
                {
                    obj.lat = double.Parse(arr[14]);
                    obj.lng = double.Parse(arr[15]);
                }
                catch { }
                obj.deliverAddress = arr[16];
                obj.isHenGio = int.Parse(arr[17]);
                obj.gioHen = DateTime.ParseExact(arr[18], "dd/MM/yyyy HH:mm", provider);
                obj.tgToRoi = int.Parse(arr[29]);
                obj.posID = int.Parse(arr[22]);
                obj.thanhToanType = int.Parse(arr[23]);
                obj.isPaid = int.Parse(arr[24]);
                obj.discountJson = arr[25];
                obj.datraV = double.Parse(arr[26]);
                obj.isZeroContact = int.Parse(arr[27]);

                obj.details = new List<classicBillDetailObj>();
                string _de = arr[21];
                classicBillDetailObj objDe;
                string[] arrDe = _de.Split(new string[] { "[de]" }, StringSplitOptions.None);
                foreach (string sDe in arrDe)
                {
                    if (sDe.Length > 0)
                    {
                        string[] arrDet = sDe.Split('@');

                        if (arrDet[8].Equals("0"))
                        {
                            objDe = new classicBillDetailObj();
                            objDe.id = long.Parse(arrDet[0]);
                            objDe.spName = arrDet[1];
                            objDe.soLg = int.Parse(arrDet[3]);
                            objDe.donGia = double.Parse(arrDet[4]);
                            objDe.notes = arrDet[5];
                            objDe.nguyenGia = double.Parse(arrDet[7]);
                            objDe.isExtra = int.Parse(arrDet[8]);
                            obj.details.Add(objDe);
                        }
                        else
                        {
                            // Neu La Extra
                            objDe = new classicBillDetailObj();
                            objDe.id = long.Parse(arrDet[0]);
                            objDe.spName = arrDet[1];
                            objDe.soLg = int.Parse(arrDet[3]);
                            objDe.donGia = double.Parse(arrDet[4]);
                            objDe.notes = arrDet[5];
                            objDe.nguyenGia = double.Parse(arrDet[7]);
                            objDe.isExtra = int.Parse(arrDet[8]);
                            obj.details.Add(objDe);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", $"Parse Bill signalR lỗi: {data}", "OK");
            }
            return obj;
        }



    }

    public class classicBillDetailObj
    {
        public long id { get; set; }

        public int isExtra { get; set; }

        public string spName { get; set; }

        public double donGia { get; set; }

        public int soLg { get; set; }

        public string discount { get; set; }

        public double nguyenGia { get; set; }

        public string notes { get; set; }
    }

}
