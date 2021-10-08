using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._vms._home;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class home_page : ContentPage
    {
        vmhome vmhome { get; set; }
        public home_page()
        {
            InitializeComponent();
        }
        public async Task render()
        {
            vmhome = new vmhome();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmhome;
            });
            localdb.home_Page = this;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private void ff_backicon_tapped(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }

        async void ff_homeicon_tapped(object sender, EventArgs e)
        {
            await khachicon.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                stlHomeMenu.IsVisible = true;
                stlEmMenu.IsVisible = false;
                stlPromoMenu.IsVisible = false;
                stlCartMenu.IsVisible = false;
                if(stlHomeMenu.Children.Count == 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var homepage = new _pages._customer.home_view();
                        stlHomeMenu.Children.Add(homepage);
                        homepage.Render();
                    });                    
                }
                else
                {
                    //
                }                
                khachicon.Source = "khachiconpress";
                menuicon.Source = "menuicon";
                Promoicon.Source = "promotionicon";
                ffcarticon.Source = "promotionicon1";

                await khachicon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                //error show here
                await khachicon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }

        async void ff_menuicon_tapped(object sender, EventArgs e)
        {
            await menuicon.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                stlHomeMenu.IsVisible = false;
                stlEmMenu.IsVisible = true;
                stlPromoMenu.IsVisible = false;
                stlCartMenu.IsVisible = false;
                if (stlEmMenu.Children.Count == 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var menupage = new _pages._menu.menu_page();
                        stlEmMenu.Children.Add(menupage);
                        menupage.Render();
                    });                    
                }
                else
                {
                    //
                }
                khachicon.Source = "khachicon";
                menuicon.Source = "menuiconpress";
                Promoicon.Source = "promotionicon";
                ffcarticon.Source = "promotionicon1";


                await menuicon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                //error show here
                await menuicon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
        async void ff_promoicon_tapped(object sender, EventArgs e)
        {
            await Promoicon.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                stlHomeMenu.IsVisible = false;
                stlEmMenu.IsVisible = false;
                stlPromoMenu.IsVisible = true;
                stlCartMenu.IsVisible = false;
                if (stlPromoMenu.Children.Count == 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var promopage = new _pages._promo.khuyen_mai_page();
                        stlPromoMenu.Children.Add(promopage);
                        promopage.Render();
                    });
                    
                }
                else
                {
                    //
                }
                khachicon.Source = "khachicon";
                menuicon.Source = "menuicon";
                Promoicon.Source = "promotioniconpress";
                ffcarticon.Source = "promotionicon1";

                await Promoicon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);

            }
            catch
            {
                this.IsEnabled = false;
                //error show here
                await Promoicon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }

        async void ff_cart_tapped(object sender, EventArgs e)
        {
            await carticon.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                stlHomeMenu.IsVisible = false;
                stlEmMenu.IsVisible = false;
                stlPromoMenu.IsVisible = false;
                stlCartMenu.IsVisible = true;
                stlCartMenu.Children.Clear();
                var cartpage = new VBMTablet._pages._thanhtoan.thanh_toan_page();
                stlCartMenu.Children.Add(cartpage);
                cartpage.Render();
                khachicon.Source = "khachicon";
                menuicon.Source = "menuicon";
                Promoicon.Source = "promotionicon";
                ffcarticon.Source = "promotioniconpress1";
                await menuicon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);

            }
            catch(Exception ex)
            {
                //error show here
                await menuicon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
        public async void updateSlCart()
        {
            vmhome.Cartcount = localdb.CartProd.Sum(p => p.slg);
        }

        
    }
}