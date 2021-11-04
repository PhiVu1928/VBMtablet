using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._cashPages._thanhtoan._ewallet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scanQRZalo_page : Rg.Plugins.Popup.Pages.PopupPage
    {
        public scanQRZalo_page()
        {
            InitializeComponent();
        }
        async void ff_close_tapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
        async void ZXingScannerView_OnScanResult(ZXing.Result result)
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