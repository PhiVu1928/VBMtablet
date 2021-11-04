using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using VBMTablet._vms._cart;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._thanhtoan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class discount_page : Rg.Plugins.Popup.Pages.PopupPage
    {
        vmCartPromo vm { get; set; }
        public discount_page()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            vm = new vmCartPromo();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vm;
            });
        }
        async void ff_close_tapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        async void stlCartPromo_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as StackLayout;
            await ctr.ScaleTo(0.9, 1);
            try
            {
                var cv = (cartPromoItem)ctr.BindingContext;
                foreach(var item in vm.cartPromoItems)
                {
                    if(cv.promotionObjs.id == item.promotionObjs.id && item.Selected == true)
                    {
                        item.Selected = false;
                    }
                    else if(cv.promotionObjs.id == item.promotionObjs.id)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }
            catch { }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }
    }
}