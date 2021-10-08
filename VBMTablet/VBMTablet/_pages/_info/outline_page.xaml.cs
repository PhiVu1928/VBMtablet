using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class outline_page : FlyoutPage
    {
        public outline_page()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();            
            
        }
        public async Task start_app()
        {
            try
            {
                var hpage = new VBMTablet._pages._info.info_page();
                flypage.Detail = hpage;
                var fpage = new VBMTablet._pages._info.menu_info_page();
                flypage.Flyout = fpage;
                flypage.Title = "vbm";
                busyindicator.IsRunning = false;
                busyindicator.IsVisible = false;
            }
            catch(Exception ex)
            {
                App.Current.MainPage.DisplayAlert("error", ex.ToString(), "OK");
            }
            
        }

        public void open_flyout()
        {
            flypage.IsPresented = true;
        }

    }
}