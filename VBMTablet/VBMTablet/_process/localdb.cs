using System;
using System.Collections.Generic;
using System.Text;
using VBMTablet._objs._menuObjs;
using VBMTablet._objs._cartObjs;
using VBMTablet._objs._promoObjs;
using VBMTablet._objs._storeObjs;
using VBMTablet._objs._userObjs;
using VBMTablet._objs._staffObjs;
using VBMTablet._objs._cashObjs._barcodeObjs;

using VBMTablet._objs.OtherServices;
using VBMTablet._pages._login;
using VBMTablet._pages._info;
using VBMTablet._pages._menu;
using VBMTablet._pages._home;
using VBMTablet._pages._thanhtoan;
using VBMTablet._pages._home._menuFloatingPages;

using VBMTablet._process;

namespace VBMTablet._process
{
    public class localdb
    {
        public static string emeImg { get; set; } = "http://manage.vuabanhmi.com/SpImg/";
        public static string endpoin { get; set; } = "http://vuabanhmi.com:6519/api/UserData/";
        public static string mapKey { get; set; } = "AIzaSyDpRFj41NIorY3s3JCluX0fgp8IobZQ0Zg";
        public static SQLiteBase SQLiteBase { get; set; }
        public static signalR signalR { get; set; }

        //data
        public static List<groupMenu> groupMenus { get; set; }
        public static List<promotionObjs> promotionObjs { get; set; }
        public static List<storeObj> storeObjs { get; set; }
        public static extra_spices extra_Spices { get; set; }


        // page & view chung
        public static cover_page cover_Page { get; set; } // Trang start App
        public static login_page login_Page { get; set; }
        public static outline_page outlinePage { get; set; } // trang chu co detail va floating Pages
        public static usingNLPage usingNLPage { get; set; } // trang su dung nguyen lieu


        //page & view of cash mode
        public static home_page home_Page { get; set; } //trang sau khi chon start cash
        public static menu_page menu_Page { get; set; }
        public static thanh_toan_page thanh_Toan_Page { get; set; }


        //Bien process
        public static bool isdeburg { get; set; } = true;
        public static long shopID { get; set; }
        public static nlBarcode nlBarcode { get; set; } //day la bien de luu thong tin nguyen lieu khi doc barcode
        public static staff NhanVieninfo { get; set; }
        public static List<staff> FullNhanVienInfo { get; set; }
        public static bool isMLMode { get; set; }
        public static userinfo userinfo { get; set; }//Khach hang
        public static List<CartProd> CartProd { get; set; } = new List<CartProd>();

    }
}
