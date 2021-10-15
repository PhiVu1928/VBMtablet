using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._vms._homeVMs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class menu_info_page : ContentPage
    {

        floatingPageVM vm;

        public menu_info_page()
        {
            InitializeComponent();
        }

        public async Task render()
        {
            vm = new floatingPageVM();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vm;
            });
        }

        async void Tinhtrangdon_Tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await tinhtrang.ScaleTo(0.9, 1);
            await tinhtrang.FadeTo(0.9, 1);
            try
            {
                using(var process = UserDialogs.Instance.Loading("Loading...",null,null,true,MaskType.Black))
                {
                   
                    await tinhtrang.ScaleTo(1, 100);
                    await this.FadeTo(1, 100);
                }
            }
            catch(Exception)
            {
                this.IsEnabled = true;
                await tinhtrang.ScaleTo(1, 100);
                await tinhtrang.FadeTo(1, 100);
            }
        }

        async void Chuanbi_Tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await ready.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                using (var progress = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
                {
                    
                    await ready.ScaleTo(1, 100);
                    await this.FadeTo(1, 100);
                }
            }
            catch
            {
                //alert
                //log error
                this.IsEnabled = true;
                await ready.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }

        async void tokenpage_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await tokenicon.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                using (var progress = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
                {
                   
                    await tokenicon.ScaleTo(1, 100);
                    await this.FadeTo(1, 100);
                }
            }
            catch
            {
                //alert
                //log error
                this.IsEnabled = true;
                await tokenicon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }

        async void grdLogOut_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            localdb.NhanVieninfo = null;
            var loginPage = new VBMTablet._pages._login.login_page();
            await Navigation.PushAsync(loginPage);
            loginPage.Render();
            await ctr.ScaleTo(1, 100);
            await this.FadeTo(1, 100);
        }
    }
}
