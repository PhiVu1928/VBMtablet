using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
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
            localdb.outlinePage = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public async Task start_app()
        {
            try
            {
                var hpage = new info_page();
                Device.BeginInvokeOnMainThread(() =>
                {
                    flypage.Detail = hpage;
                });
                
                var fpage = new menu_info_page();
                Device.BeginInvokeOnMainThread(() =>
                {
                    flypage.Flyout = fpage;
                });

                hpage.render();
                fpage.render();

                busyindicator.IsRunning = false;
                busyindicator.IsVisible = false;
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("error", ex.ToString(), "OK");
            }
        }

        public void open_flyout()
        {
            flypage.IsPresented = true;
        }

    }
}