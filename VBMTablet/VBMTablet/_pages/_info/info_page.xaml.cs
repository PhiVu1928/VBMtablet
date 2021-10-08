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

namespace VBMTablet._pages._info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class info_page : ContentPage
    {
        public info_page()
        {
            InitializeComponent();
        }

        private void ffimg_left_tapped(object sender, EventArgs e)
        {
            localdb.outline_Page.open_flyout();
        }
        async void ffimg_right_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await offbtn.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                using (var process = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
                {
                   
                    this.IsEnabled = true;
                    await offbtn.ScaleTo(1, 100);
                    await this.FadeTo(1, 100);
                }
            }
            catch
            {
                this.IsEnabled = false;
                await offbtn.ScaleTo(0.9, 1);
                await this.FadeTo(0.9, 1);
            }
        }

        async void ff_order_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await ordericon.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var popup = new _pages._info.popup_xacnhan();
                await Navigation.PushPopupAsync(popup);
                this.IsEnabled = true;
                await ordericon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch(Exception)
            {
                this.IsEnabled = false;
                //error show here
                await ordericon.ScaleTo(0.9, 1);
                await this.FadeTo(0.9, 1);
            }
        }
    }
}