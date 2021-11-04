using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Border;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._cashPages._customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class voucher_page : ContentPage
    {
        public voucher_page()
        {
            InitializeComponent();
        }

        async void bdGotoMenu_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            localdb.home_Page.GotoMenu();
            Navigation.RemovePage(this);
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        async void bdScanVoucher_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            var ScanPage = new scanVoucher_page();
            await Navigation.PushPopupAsync(ScanPage);
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }
    }
}