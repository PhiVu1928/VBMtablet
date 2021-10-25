using System;
using System.Collections.Generic;
using System.Text;

namespace VBMTablet._objs._cashObjs._orderedInDayObjs
{
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
