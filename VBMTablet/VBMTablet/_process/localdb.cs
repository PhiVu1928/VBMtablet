using System;
using System.Collections.Generic;
using System.Text;
using VBMTablet._objs._menuObjs;
using VBMTablet._objs._cartObjs;
using VBMTablet._pages._login;
using VBMTablet._pages._info;
using VBMTablet._pages._menu;
using VBMTablet._pages._home;
using VBMTablet._pages._thanhtoan;
using VBMTablet._process;

namespace VBMTablet._process
{
    public class localdb
    {
        public static string emeImg { get; set; } = "http://manage.vuabanhmi.com/SpImg/";
        public static string endpoin { get; set; } = "http://vuabanhmi.com:6519/api/UserData/";
        public static string mapKey { get; set; } = "AIzaSyDpRFj41NIorY3s3JCluX0fgp8IobZQ0Zg";
        public static SQLiteBase SQLiteBase { get; set; }

        public static List<groupMenu> groupMenus { get; set; }
        public static extra_spices extra_Spices { get; set; }
        public static List<CartProd> CartProd { get; set; } = new List<CartProd>();

        //pages
        public static login_page login_Page { get; set; }
        public static cover_page cover_Page { get; set; }
        public static outline_page outline_Page { get; set; }
        public static menu_page menu_Page { get; set; }
        public static home_page home_Page { get; set; }
        public static thanh_toan_page thanh_Toan_Page { get; set; }
        //Bien process
        public static bool isdeburg { get; set; } = true;
    }
}
