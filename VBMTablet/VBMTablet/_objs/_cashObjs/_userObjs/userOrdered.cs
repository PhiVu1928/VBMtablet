using System;
using System.Collections.Generic;
using System.Text;

namespace VBMTablet._objs._userObjs
{
    public class userOrdered
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
        public object HoTenKhach { get; set; }
        public object PhoneKhach { get; set; }
        public DateTime StartDeliTime { get; set; }
        public int IsZeroContact { get; set; }
        public int UserStatus { get; set; }
        public bool IsRated { get; set; }
        public int productS { get; set; }
        public int serviceS { get; set; }
        public int peopleS { get; set; }
        public int timeS { get; set; }
        public string KHcomment { get; set; }
        public DateTime predictEDT { get; set; }
        public List<ListBillDetail> ListBillDetails { get; set; }
    }
    public class ListBillDetail
    {
        public int BillID { get; set; }
        public int SpID { get; set; }
        public string SpName { get; set; }
        public double DonGia { get; set; }
        public int SoLg { get; set; }
        public double TgTien { get; set; }
        public string Discount { get; set; }
        public double ThanhTien { get; set; }
        public string Notes { get; set; }
        public double NguyenGia { get; set; }
        public int IsExtra { get; set; }
        public string AliasTenSP { get; set; }
        public int ItemID { get; set; }
    }
}
