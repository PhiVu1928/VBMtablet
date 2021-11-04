using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using VBMTablet._pages._cashPages._thanhtoan._ewallet;
using VBMTablet._vms._cart;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._thanhtoan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class hinh_thuc_thanh_toan_page : Rg.Plugins.Popup.Pages.PopupPage
    {
        vmEwallet vm { get; set; }
        public hinh_thuc_thanh_toan_page()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            vm = new vmEwallet();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vm;
            });
        }
        async void ff_close_tapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        async void grdEwallet_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            try
            {
                var cv = (EwalletItem)ctr.BindingContext;
                switch (cv.id)
                {
                    case 4:
                        {
                            foreach(var item in vm.ewalletItems)
                            {
                                if(cv.id == item.id)
                                {
                                    item.Selected = true;
                                }
                                else
                                {
                                    item.Selected = false;
                                }
                            }
                            var ScanZaloPage = new scanQRZalo_page();
                            await Navigation.PushPopupAsync(ScanZaloPage);
                            break;
                        }
                }
                await ctr.ScaleTo(1, 100);
                this.IsEnabled = true;
            }
            catch { }
        }
    }
}