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
using VBMTablet._pages._cashPages._customer;

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
            localdb.home_Page = this;
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmhome;
                stlEmMenu.IsVisible = false;
                stlPromoMenu.IsVisible = false;
                stlCartMenu.IsVisible = false;
                busyindicator.IsVisible = false;
            });
        }
        
        async void ff_backicon_tapped(object sender, EventArgs e)
        {
            var ques = await Application.Current.MainPage.DisplayAlert("", "Thông tin khách hàng và giỏ hàng sẽ mất, bạn có muốn quay lại không ?", "Ok", "No");
            if(ques)
            {
                localdb.fullUserInfo = null;
                localdb.CartProd.Clear();
                Navigation.RemovePage(this);
            }
        }

        async void ff_homeicon_tapped(object sender, EventArgs e)
        {
            await khachicon.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {
                grHomeMenu.IsVisible = true;
                stlEmMenu.IsVisible = false;
                stlPromoMenu.IsVisible = false;
                stlCartMenu.IsVisible = false;
                if(grHomeMenu.Children.Count == 0)
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
            }
            catch
            {
                
            }
            await khachicon.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        public async void ff_menuicon_tapped(object sender, EventArgs e)
        {
            busyindicator.IsVisible = true;
            busyindicator.IsBusy = true;
            await menuicon.ScaleTo(0.9, 1);
            this.IsEnabled = false;


            try
            {
                grHomeMenu.IsVisible = false;
                stlEmMenu.IsVisible = true;
                stlPromoMenu.IsVisible = false;
                stlCartMenu.IsVisible = false;
                if (stlEmMenu.Children.Count == 0)
                {
                    var menupage = new _pages._menu.menu_page();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        stlEmMenu.Children.Add(menupage);
                    });
                    menupage.Render();
                }
                khachicon.Source = "khachicon";
                menuicon.Source = "menuiconpress";
                Promoicon.Source = "promotionicon";
                ffcarticon.Source = "promotionicon1";

            }
            catch
            {

            }

            await menuicon.ScaleTo(1, 100);
            this.IsEnabled = true;
            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;
        }

        async void ff_promoicon_tapped(object sender, EventArgs e)
        {
            await Promoicon.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {
                grHomeMenu.IsVisible = false;
                stlEmMenu.IsVisible = false;
                stlPromoMenu.IsVisible = true;
                stlCartMenu.IsVisible = false;
                if (stlPromoMenu.Children.Count == 0)
                {
                    var promopage = new _promo.khuyen_mai_page();
                    stlPromoMenu.Children.Add(promopage);
                    promopage.Render();
                }
                khachicon.Source = "khachicon";
                menuicon.Source = "menuicon";
                Promoicon.Source = "promotioniconpress";
                ffcarticon.Source = "promotionicon1";
            }
            catch
            {
                
            }
            this.IsEnabled = true;
            //error show here
            await Promoicon.ScaleTo(1, 100);
        }

        public async void updateSlCart()
        {
            vmhome.Cartcount = localdb.CartProd.Sum(p => p.slg);
        }

        async void bdSearchUser_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
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
                            lblTenKhachHang.Text = localdb.fullUserInfo.UserInfo.Fullname;
                            var Customer = new customer_page();
                            tvCustomer.Items[0].Content = Customer;
                            Customer.Render(localdb.fullUserInfo.UserInfo);
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
                this.IsEnabled = true;
            }
            catch
            {
                await ctr.ScaleTo(1, 100);
                this.IsEnabled = true;
            }
        }

        async void tvCustomer_SelectionChanging(object sender, SelectionChangingEventArgs e)
        {
            switch (e.Index)
            {
                case 0: break;
                case 1:
                    //load script khach
                    if(tvCustomer.Items[1].Content == null)
                    {
                        using (var progress = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
                        {
                            UserGiftObjs userGiftObjs = await UserGiftObjs.getUserGiftData(vmhome.sdt, localdb.fullUserInfo.UserInfo.UserID);
                            if (localdb.fullUserInfo != null)
                            {
                                var ScriptPage = new script_page();
                                tvCustomer.Items[1].Content = ScriptPage;
                                ScriptPage.Render(localdb.fullUserInfo.UserInfo);
                            }
                        }
                    }
                    break;
                case 2:
                    //load qua khach
                    if (tvCustomer.Items[2].Content == null && vmhome.sdt.Length != 1)
                    {
                        using (var progress = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
                        {
                            string sdt = vmhome.sdt;
                            UserGiftObjs userGiftObjs = await UserGiftObjs.getUserGiftData(vmhome.sdt, localdb.fullUserInfo.UserInfo.UserID);
                            if (userGiftObjs != null)
                            {
                                var giftPage = new gift_page();
                                tvCustomer.Items[2].Content = giftPage;
                                giftPage.Render(userGiftObjs);
                            }
                        }
                    }
                    break;
                case 3:
                    //load history bill khach
                    if(tvCustomer.Items[3].Content == null)
                    {
                        using (var progress = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
                        {
                            string url = $"{localdb.endpoin}get_ordered_bills?sdt={vmhome.sdt}&userid={localdb.fullUserInfo.UserInfo.UserID}";
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
                                        var historyPage = new historypage();
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

        async void grdResetCart_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            if (localdb.CartProd != null)
            {
                var ques = await Application.Current.MainPage.DisplayAlert("", "Bạn có muốn làm mới giỏ hàng ?", "Ok", "No");
                if(ques)
                {
                    localdb.CartProd.Clear();
                    localdb.home_Page.updateSlCart();
                }
            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        async void ff_cart_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            using (var progress = UserDialogs.Instance.Loading("...", null, null, true, MaskType.Black))
            {
                try
                {
                    var cartpage = new _thanhtoan.thanh_toan_page();
                    await Navigation.PushAsync(cartpage);
                    cartpage.Render();
                }
                catch (Exception ex)
                {

                }
            }
            this.IsEnabled = true;
        }

        async void bdVoucher_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            var voucher = new voucher_page();
            await Navigation.PushAsync(voucher);
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        public async void GotoMenu()
        {
            busyindicator.IsVisible = true;
            busyindicator.IsBusy = true;
            try
            {
                grHomeMenu.IsVisible = false;
                stlEmMenu.IsVisible = true;
                stlPromoMenu.IsVisible = false;
                stlCartMenu.IsVisible = false;
                if (stlEmMenu.Children.Count == 0)
                {
                    var menupage = new _pages._menu.menu_page();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        stlEmMenu.Children.Add(menupage);
                    });
                    menupage.Render();
                }
                khachicon.Source = "khachicon";
                menuicon.Source = "menuiconpress";
                Promoicon.Source = "promotionicon";
                ffcarticon.Source = "promotionicon1";

            }
            catch { }
            busyindicator.IsVisible = false;
            busyindicator.IsBusy = false;
        }
    }
}