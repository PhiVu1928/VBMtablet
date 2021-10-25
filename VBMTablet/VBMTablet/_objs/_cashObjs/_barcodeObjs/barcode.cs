using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace VBMTablet._objs._cashObjs._barcodeObjs
{
    public class nlBarcode
    {
        long _autoid;
        string _code;
        long _nlid;
        string _tennguyenlieu;
        int _cumid;
        string _tencum;
        long _shopid;
        string _shopname;
        DateTime _ngaysanxuat;
        DateTime _ngayhethan;
        string _menum;
        double _solg;
        double _solgused;
        double _solgavail;
        int _status;
        int _posid;
        int _typeid;

        public long AutoID
        {
            get { return _autoid; }
            set { _autoid = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public long NLID
        {
            get { return _nlid; }
            set { _nlid = value; }
        }

        public string TenNguyenLieu
        {
            get { return _tennguyenlieu; }
            set { _tennguyenlieu = value; }
        }

        public int CumID
        {
            get { return _cumid; }
            set { _cumid = value; }
        }

        public string TenCum
        {
            get { return _tencum; }
            set { _tencum = value; }
        }

        public long ShopID
        {
            get { return _shopid; }
            set { _shopid = value; }
        }

        public string ShopName
        {
            get { return _shopname; }
            set { _shopname = value; }
        }

        public DateTime NgaySanXuat
        {
            get { return _ngaysanxuat; }
            set { _ngaysanxuat = value; }
        }

        public DateTime NgayHetHan
        {
            get { return _ngayhethan; }
            set { _ngayhethan = value; }
        }

        public string MeNum
        {
            get { return _menum; }
            set { _menum = value; }
        }

        public double SoLg
        {
            get { return _solg; }
            set { _solg = value; }
        }

        public double SoLgUsed
        {
            get { return _solgused; }
            set { _solgused = value; }
        }

        public double SoLgAvail
        {
            get { return _solgavail; }
            set { _solgavail = value; }
        }

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int PosID
        {
            get { return _posid; }
            set { _posid = value; }
        }

        public int TypeID
        {
            get { return _typeid; }
            set { _typeid = value; }
        }
        public nlBarcode()
        {
        }
        public static nlBarcode parseBarCode(string data)
        {
            nlBarcode obj = new nlBarcode();
            try
            {
                string[] arr = data.Split('$');
                obj.Code = arr[0];
                obj.CumID = int.Parse(arr[1]);
                obj.MeNum = arr[2];
                string nhh = arr[3];
                string nsx = arr[4];
                DateTime f;
                DateTime t;
                try
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    f = DateTime.ParseExact(nhh, "MM/dd/yyyy", provider);
                    t = DateTime.ParseExact(nsx, "MM/dd/yyyy", provider);
                }
                catch
                {
                    f = DateTime.Now.AddDays(-1);
                    t = DateTime.Now;
                }
                obj.NgayHetHan = f;
                obj.NgaySanXuat = t;
                obj.NLID = long.Parse(arr[5]);
                obj.ShopID = long.Parse(arr[6]);
                obj.ShopName = arr[7];
                obj.SoLg = double.Parse(arr[8]);
                obj.TenCum = arr[9];
                obj.TenNguyenLieu = arr[10];
                obj.AutoID = long.Parse(arr[11]);
                obj.SoLgAvail = double.Parse(arr[12]);
                obj.SoLgUsed = double.Parse(arr[13]);
            }
            catch { }
            return obj;
        }
    }
}
