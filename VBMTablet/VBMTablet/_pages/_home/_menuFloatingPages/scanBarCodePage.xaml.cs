using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home._menuFloatingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class scanBarCodePage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public scanBarCodePage()
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
                    localdb.signalR.HubProxy.Invoke("SystemAction", "getBarCodeInfo{}" + result.Text + "{}");
                });
                zxScanBarCode.IsScanning = false;
                await Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", ex.ToString(), "OK");
            }
            
        }
    }
}