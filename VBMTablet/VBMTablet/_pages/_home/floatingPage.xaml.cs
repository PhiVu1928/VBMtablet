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
            this.IsEnabled = false;
            try
            {
                var billInDayPage = new VBMTablet._pages._home._menuFloatingPages.billInDayPage();
                await Navigation.PushAsync(billInDayPage);
                billInDayPage.Render();
            }
            catch(Exception)
            {

            }
            await tinhtrang.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        async void Chuanbi_Tapped(object sender, EventArgs e)
        {
            await ready.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {
                var prepareNlPage = new VBMTablet._pages._home._menuFloatingPages.prepareNLPage();
                await Navigation.PushAsync(prepareNlPage);
                prepareNlPage.Render();
            }
            catch
            {
                //log error
            }
            await ready.ScaleTo(1, 100);
            this.IsEnabled = true;
        }
        async void grdKaruna_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {
                var lstBillKarunaPage = new VBMTablet._pages._home._menuFloatingPages.lstBillKarunaPage();
                await Navigation.PushAsync(lstBillKarunaPage);
                lstBillKarunaPage.Render();
            }
            catch
            {
                //log error
            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }


        async void grdLogOut_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;

            //thuc hien xoa cac thong tin
            localdb.NhanVieninfo = null;
            localdb.shopID = 0;
            localdb.groupMenus = null;
            localdb.extra_Spices = null;
            localdb.FullNhanVienInfo = null;
            localdb.home_Page = null;
            localdb.CartProd = null;
            localdb.promotionObjs = null;
            localdb.menu_Page = null;
            localdb.thanh_Toan_Page = null;
            localdb.fullUserInfo = null;

            var loginPage = new VBMTablet._pages._login.login_page();
            await Navigation.PushAsync(loginPage);
            loginPage.Render();
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        async void grdUsingNL_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {
                var UsingNlPage = new VBMTablet._pages._home._menuFloatingPages.usingNLPage();
                await Navigation.PushAsync(UsingNlPage);
                UsingNlPage.Render();
            }
            catch
            {
                //log error
            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }
    }
}
