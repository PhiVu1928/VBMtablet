using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.Border;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VBMTablet._vms._promo;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._promo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class khuyen_mai_page : ContentView
    {
        vmpromo vmpromo { get; set; }
        public khuyen_mai_page()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            vmpromo = new vmpromo();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmpromo;
            });
        }
       
        

        async void bdpromo_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.8, 150);

            var cv = (PromoStatus)ctr.BindingContext;
            try
            {
                var promodetai = new promo_detail();
                await PopupNavigation.Instance.PushAsync(promodetai);
                promodetai.Render(cv.promotion);
            }
            catch { }

            this.IsEnabled = true;
            await ctr.ScaleTo(1, 150);

        }

    }
}