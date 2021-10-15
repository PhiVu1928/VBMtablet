using Acr.UserDialogs;
using Syncfusion.XForms.Border;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;
using VBMTablet._vms._home;
using VBMTablet._objs._userObjs;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Syncfusion.XForms.TabView;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;

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
            stlEmMenu.IsVisible = false;
            stlPromoMenu.IsVisible = false;
            stlCartMenu.IsVisible = false;
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
                    //                    
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
                    var promopage = new _pages._promo.khuyen_mai_page();
                    stlPromoMenu.Children.Add(promopage);
                    promopage.Render();
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

        async void bdSearchUser_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            var sdt = vmhome.sdt;
            try
            {
                if (string.IsNullOrEmpty(sdt))
                {
                    await Application.Current.MainPage.DisplayAlert("", "Bạn chưa nhập số điện thoại", "Ok");
                }
                if (sdt.Length < 10 && sdt != "555" && sdt != "777" && sdt != "1")
                {
                    await Application.Current.MainPage.DisplayAlert("", "Vui long kiểm tra lại số diện thoại khách hàng!", "Ok");
                }
                if (sdt.Length == 10)
                {
                     using(var progress = UserDialogs.Instance.Loading("Loading...",null,null,true,MaskType.Black))
                    {
                        var user = await userinfo.getUserData(sdt);
                        if (user != null)
                        {
                            tvCustomer.Items[1].Content = null;
                            tvCustomer.Items[2].Content = null;
                            tvCustomer.Items[3].Content = null;
                            vmhome.visNonUserInfo = false;
                            vmhome.visUserInfo = true;
                            lblTenKhachHang.Text = localdb.userinfo.Fullname;
                            var Customer = new VBMTablet._pages._home.customer_page();
                            tvCustomer.Items[0].Content = Customer;
                            Customer.Render(localdb.userinfo);
                        }
                        else
                        {
                            var ques = await Application.Current.MainPage.DisplayAlert("", "Không tìm thấy thông tin khách hàng, bạn có muốn thêm khách hàng mới không", "Thêm khách mới", "NO");
                            if(ques)
                            {
                                var NewCustomerPage = new addNewCustomer_page();
                                await Navigation.PushPopupAsync(NewCustomerPage);
                                NewCustomerPage.Render();
                            }
                        }                        

                    }
                }
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
        async void tvCustomer_SelectionChanging(object sender, SelectionChangingEventArgs e)
        {
            switch (e.Index)
            {
                case 0: break;
                case 1:
                    if(tvCustomer.Items[1].Content == null)
                    {
                        tvCustomer.Items[1].Content = new VBMTablet._pages._home.script_page();
                    }
                    break;
                case 2:
                    if (tvCustomer.Items[2].Content == null && vmhome.sdt.Length != 1)
                    {
                        using (var progress = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
                        {
                            string sdt = vmhome.sdt;
                            UserGiftObjs userGiftObjs = await UserGiftObjs.getUserGiftData(vmhome.sdt, localdb.userinfo.UserID);
                            if (userGiftObjs != null)
                            {
                                var giftPage = new VBMTablet._pages._home.gift_page();
                                tvCustomer.Items[2].Content = giftPage;
                                giftPage.Render(userGiftObjs);
                            }
                        }
                    }
                    break;
                case 3:
                    if(tvCustomer.Items[3].Content == null)
                    {
                        using (var progress = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
                        {
                            string url = $"{localdb.endpoin}get_ordered_bills?sdt={vmhome.sdt}&userid={localdb.userinfo.UserID}";
                            if (tools.isConn())
                            {
                                using (var cl = new HttpClient())
                                {
                                    var res1 = await cl.GetAsync(url);
                                    var res2 = await res1.Content.ReadAsStringAsync();
                                    var jOb = JObject.Parse(res2);
                                    var success = bool.Parse(tools.GetJArrayValue(jOb, "Success"));
                                    if (success)
                                    {
                                        var str = tools.GetJArrayValue(jOb, "Datas");
                                        var data = JsonConvert.DeserializeObject<List<userOrdered>>(str);
                                        List<userOrdered> userOrdereds = data;
                                        var historyPage = new VBMTablet._pages._home.historypage();
                                        tvCustomer.Items[3].Content = historyPage;
                                        historyPage.Render(userOrdereds);
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}