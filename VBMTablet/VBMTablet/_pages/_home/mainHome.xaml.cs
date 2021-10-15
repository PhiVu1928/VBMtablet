using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using VBMTablet._process;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VBMTablet._vms._homeVMs;
using Syncfusion.XForms.Border;

namespace VBMTablet._pages._info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class info_page : ContentPage
    {
        homePageVM vm;

        public info_page()
        {
            InitializeComponent();
        }

        public async Task render()
        {
            vm = new homePageVM();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vm;
            });
        }

        private void ffimg_left_tapped(object sender, EventArgs e)
        {
            localdb.outlinePage.open_flyout();
        }

        async void bdChangeMode_Tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.8, 150);

            vm.isMl = !vm.isMl;
            localdb.isMLMode = vm.isMl;

            await ctr.ScaleTo(1, 150);
            this.IsEnabled = true;
        }

        async void ff_order_tapped(object sender, EventArgs e)
        {
            await ordericon.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var popup = new _pages._info.popup_xacnhan();
                await Navigation.PushPopupAsync(popup);

                await ordericon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch(Exception)
            {
                //error show here
                await ordericon.ScaleTo(0.9, 1);
                await this.FadeTo(0.9, 1);
            }
        }

    }
}