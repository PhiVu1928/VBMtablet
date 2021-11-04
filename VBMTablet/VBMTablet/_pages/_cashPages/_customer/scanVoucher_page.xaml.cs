using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._cashPages._customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scanVoucher_page : Rg.Plugins.Popup.Pages.PopupPage
    {
        public scanVoucher_page()
        {
            InitializeComponent();
        }

        private void ff_close_tapped(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        async void zxScanBarCode_OnScanResult(ZXing.Result result)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("Scanned Result", result.Text, "OK");
                });
                zxScanBarCode.IsScanning = false;
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.ToString(), "OK");
            }
        }
    }
}