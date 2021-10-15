using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VBMTablet._process;
using VBMTablet._utils;
using VBMTablet._objs._menuObjs;
using VBMTablet._objs._storeObjs;
using VBMTablet._objs._promoObjs;
using VBMTablet._objs.OtherServices;

namespace VBMTablet._pages._login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cover_page : ContentPage
    {
        public cover_page()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            localdb.cover_Page = this;
            startAppAction();
        }

        public async Task startAppAction()
        {
            localdb.signalR = new signalR();
            localdb.signalR.ConnectAsync();
            var menu = await groupMenu.getMenuData();
            var extraSpices = await extra_spices.getExsSpisData();
            var store = await storeObj.getLstStores();
            var promo = await promotionObjs.getPromotions();
            if (menu != null && extraSpices != null && store != null && promo != null)
            {
                localdb.groupMenus = menu;
                localdb.extra_Spices = extraSpices;
                localdb.storeObjs = store;
                localdb.promotionObjs = promo;
                localdb.cover_Page.start_app();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Kết nối hệ thống thất bại", "OK");
            }
        }

        public async void start_app()
        {
            var login_page = new login_page();
            Device.BeginInvokeOnMainThread(() =>
            {
                Navigation.PushAsync(login_page);
            });
            login_page.Render();
        }
        
    }
}