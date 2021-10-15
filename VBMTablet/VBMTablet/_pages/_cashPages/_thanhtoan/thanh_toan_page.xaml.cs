﻿using Acr.UserDialogs;
using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

using VBMTablet._vms._cart;
using VBMTablet._objs._cartObjs;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.Border;
using VBMTablet._process;
using System.Linq;

namespace VBMTablet._pages._thanhtoan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class thanh_toan_page : ContentView
    {
        public vmcart vmcart { get; set; }
        public thanh_toan_page()
        {
            InitializeComponent();
        }
        
        public async Task Render()
        {
            vmcart = new vmcart();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmcart;
            });
            localdb.thanh_Toan_Page = this;
        }

        async void ff_thanhtoan_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await borderwallet.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                borderwallet.BackgroundColor = (Color)Application.Current.Resources["vbmgreen"];
                walleticon.Source = "walleticonvang";
                labelwallet.TextColor = (Color)Application.Current.Resources["vbmwhite"];
                var page = new _pages._thanhtoan.hinh_thuc_thanh_toan_page();
                await Navigation.PushPopupAsync(page);
                page.Render();
                this.IsEnabled = true;

                await borderwallet.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                //erros show here
                await borderwallet.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
        async void ff_discount_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await vouchericon.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                voucherborder.BackgroundColor = (Color)Application.Current.Resources["vbmgreen"];
                vouchericon.Source = "vouchericonvang";
                voucherlable.TextColor = (Color)Application.Current.Resources["vbmwhite"];
                var page = new _pages._thanhtoan.discount_page();
                await Navigation.PushPopupAsync(page);
                page.Render();
                this.IsEnabled = true;

                await vouchericon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                //erros show here
                await vouchericon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }

        async void ff_hoadon_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await brhoadon.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                brhoadon.BackgroundColor = (Color)Application.Current.Resources["vbmlightyellow"];
                lblhoadon.TextColor = (Color)Application.Current.Resources["vbmgreen"];
                var popuphoadon = new _pages._thanhtoan.hoa_don_page();
                await Navigation.PushPopupAsync(popuphoadon);
                this.IsEnabled = true;                

                brcombo.BackgroundColor = (Color)Application.Current.Resources["vbmlightgray"];
                lblcombo.TextColor = (Color)Application.Current.Resources["vbmgray"];
            }
            catch(Exception)
            {
                //error show here
                this.IsEnabled = false;
                await brhoadon.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            
        }
        public void ff_combo_tapped(object sender, EventArgs e)
        {
            brcombo.BackgroundColor = (Color)Application.Current.Resources["vbmlightyellow"];
            lblcombo.TextColor = (Color)Application.Current.Resources["vbmgreen"];

            brhoadon.BackgroundColor = (Color)Application.Current.Resources["vbmlightgray"];
            lblhoadon.TextColor = (Color)Application.Current.Resources["vbmgray"];
        }

        

        async void orderOption_tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var cv = (OrderOption)ctr.BindingContext;
                foreach(var item in vmcart.orderOptions)
                {
                    if(item != null)
                    {
                        switch(cv.id)
                        {
                            case 0:
                                if (cv.id == 0 && cv.id == item.id)
                                {
                                    if(localdb.CartProd.Count == 0)
                                    {
                                        await Application.Current.MainPage.DisplayAlert("Không có sản phẩm nào trong giỏ hàng", "Vui lòng chọn sản phẩm và thực hiện thanh toán lại bạn nhé !", "Ok");
                                    }
                                    else
                                    {
                                        var price = localdb.thanh_Toan_Page.vmcart.CallMoney();
                                        var bill = BillCreateDoAddBill.createDoAddBill("", 0);
                                        var Inplace = new VBMTablet._pages._thanhtoan._orderoption.InPlace();
                                        await Navigation.PushPopupAsync(Inplace);
                                        Inplace.Render(bill);
                                        item.Selected = true;
                                    }                                    
                                }
                                else
                                {
                                    item.Selected = false;
                                }
                                break;
                            case 1:
                                if (cv.id == 1 && cv.id == item.id)
                                {
                                    if (localdb.CartProd.Count == 0)
                                    {
                                        await Application.Current.MainPage.DisplayAlert("Không có sản phẩm nào trong giỏ hàng", "Vui lòng chọn sản phẩm và thực hiện thanh toán lại bạn nhé !", "Ok");
                                    }
                                    else
                                    {                                        
                                        item.Selected = true;
                                    }
                                }
                                else
                                {
                                    item.Selected = false;
                                }
                                break;
                            case 2:
                                if (cv.id == 2 && cv.id == item.id)
                                {
                                    if (localdb.CartProd.Count == 0)
                                    {
                                        await Application.Current.MainPage.DisplayAlert("Không có sản phẩm nào trong giỏ hàng", "Vui lòng chọn sản phẩm và thực hiện thanh toán lại bạn nhé !", "Ok");
                                    }
                                    else
                                    {
                                        item.Selected = true;
                                    }
                                }
                                else
                                {
                                    item.Selected = false;
                                }
                                break;
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                //log error
                App.Current.MainPage.DisplayAlert("error", ex.ToString(), "ok");
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }
        async void lbldeletecart_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var cv = (cartItem)ctr.BindingContext;
                var ques = await Application.Current.MainPage.DisplayAlert("", "Bạn thực sự muốn xóa sản phẩm này ?", "Ok", "No");
                if (ques)
                {
                    localdb.CartProd.Remove(cv.prod);
                    vmcart.RenderCartItem();
                    vmcart.CallMoney();
                }
                if (localdb.home_Page != null)
                    localdb.home_Page.updateSlCart();
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch (Exception ex)
            {
                //log error
                App.Current.MainPage.DisplayAlert("error", ex.ToString(), "ok");
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }

        async void lbldecreasecartitem_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var cv = (cartItem)ctr.BindingContext;
                if (cv.prod.slg == 1)
                {
                    var ques = await Application.Current.MainPage.DisplayAlert("", "Bạn thực sự muốn xóa sản phẩm này ?", "Ok", "No");
                    if (ques)
                    {
                        localdb.CartProd.Remove(cv.prod);
                        vmcart.RenderCartItem();
                        vmcart.CallMoney();
                    }
                    
                }
                else
                {
                    cv.solg--;
                    cv.prod.slg--;
                }
                
                if (localdb.home_Page != null)
                    localdb.home_Page.updateSlCart();
                if (localdb.CartProd.Sum(x => x.slg) > 0)
                {
                    vmcart.RenderCartItem();
                    vmcart.CallMoney();
                }
            }
            catch (Exception)
            {
                //log error
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }

        async void lblincreasecartitem_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var cv = (cartItem)ctr.BindingContext;
                cv.solg++;
                cv.prod.slg++;
                if (localdb.home_Page != null)
                    localdb.home_Page.updateSlCart();
                if(localdb.CartProd.Sum(x => x.slg) > 0)
                {
                    vmcart.RenderCartItem();
                    vmcart.CallMoney();
                }
            }
            catch (Exception)
            {
                //log error
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
        }

    }
}