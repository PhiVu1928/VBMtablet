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
            lblStaffName.Text = localdb.NhanVieninfo.HoTen;
        }

        async void Tinhtrangdon_Tapped(object sender, EventArgs e)
        {
            await tinhtrang.ScaleTo(0.9, 1);
            await tinhtrang.FadeTo(0.9, 1);
            try
            {
                var billInDayPage = new VBMTablet._pages._home._menuFloatingPages.billInDayPage();
                await Navigation.PushAsync(billInDayPage);
                billInDayPage.Render();

                await tinhtrang.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch(Exception)
            {
                await tinhtrang.ScaleTo(1, 100);
                await tinhtrang.FadeTo(1, 100);
            }
        }

        async void Chuanbi_Tapped(object sender, EventArgs e)
        {
            await ready.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var prepareNlPage = new VBMTablet._pages._home._menuFloatingPages.prepareNLPage();
                await Navigation.PushAsync(prepareNlPage);
                prepareNlPage.Render();

                await ready.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                //log error
                await ready.ScaleTo(1, 100);
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

        async void grdUsingNL_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var UsingNlPage = new VBMTablet._pages._home._menuFloatingPages.usingNLPage();
                await Navigation.PushAsync(UsingNlPage);
                UsingNlPage.Render();
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                //log error
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
    }
}
