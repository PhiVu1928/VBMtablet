using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VBMTablet._process;
using VBMTablet._objs._menuObjs;

namespace VBMTablet._pages._login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cover_page : ContentPage
    {
        public cover_page()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                if (localdb.groupMenus != null && localdb.extra_Spices != null)
                {
                    start_app();
                }
                else
                {
                    var menu = await groupMenu.getMenuData();
                    var extraSpices = await extra_spices.getExsSpisData();
                    if (menu != null && extraSpices != null)
                    {
                        start_app();
                    }
                }
            }
            catch(Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
                       
        }
        public async void start_app()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var login_page = new _login.login_page();
                Navigation.PushAsync(login_page);
                login_page.Render();
            });            
        }
    }
}