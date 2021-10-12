using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VBMTablet._objs._userObjs;
using VBMTablet._vms._home;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class gift_page : ContentView
    {
        vmCustomerGift vmCustomerGift { get; set; }
        public gift_page()
        {
            InitializeComponent();
        }
        public async Task Render(UserGiftObjs userGiftObjs)
        {
            vmCustomerGift = new vmCustomerGift(userGiftObjs);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmCustomerGift;
            });
        }
    }
}