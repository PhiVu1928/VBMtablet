using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
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
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            
        }

        async void GrPromoDetail_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            var cv = (PromoStatus)ctr.BindingContext;
            try
            {
                var promodetai = new promo_detail();
                await Navigation.PushPopupAsync(promodetai);
                promodetai.Render(cv.promotion);
            }
            catch { }
        }
    }
}