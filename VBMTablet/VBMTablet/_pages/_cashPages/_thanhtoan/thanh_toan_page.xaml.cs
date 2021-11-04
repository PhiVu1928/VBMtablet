using Acr.UserDialogs;
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
using VBMTablet._pages._menu;
using System.Collections.Generic;
using VBMTablet._objs._userObjs;
using VBMTablet._pages._cashPages._thanhtoan._orderoption;

namespace VBMTablet._pages._thanhtoan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class thanh_toan_page : ContentPage
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

        async void ff_backicon_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await Navigation.PopAsync();
            this.IsEnabled = true;
        }

        async void ff_thanhtoan_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await borderwallet.ScaleTo(0.9, 1);
            try
            {
                var page = new _pages._thanhtoan.hinh_thuc_thanh_toan_page();
                await Navigation.PushPopupAsync(page);
                page.Render();
            }
            catch
            {
                
            }
            await borderwallet.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        async void ff_discount_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await vouchericon.ScaleTo(0.9, 1);
            try
            {
                var page = new discount_page();
                await Navigation.PushPopupAsync(page);
                page.Render();
            }
            catch (Exception ex)
            {
            }
            this.IsEnabled = true;
            await vouchericon.ScaleTo(1, 100);
        }

        async void ff_hoadon_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            await brhoadon.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {
                var popuphoadon = new _pages._thanhtoan.hoa_don_page();
                await Navigation.PushPopupAsync(popuphoadon);        
            }
            catch(Exception)
            {
                //error show here
            }
            this.IsEnabled = true;
            await brhoadon.ScaleTo(1, 100);
        }

        public void ff_combo_tapped(object sender, EventArgs e)
        {
            
        }

        async void orderOption_tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
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
                                //dat tai cho
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
                                //dat den lay
                                if (cv.id == 1 && cv.id == item.id)
                                {
                                    if (localdb.CartProd.Count == 0)
                                    {
                                        await Application.Current.MainPage.DisplayAlert("Không có sản phẩm nào trong giỏ hàng", "Vui lòng chọn sản phẩm và thực hiện thanh toán lại bạn nhé !", "Ok");
                                    }
                                    else
                                    {
                                        var page = new PickUp();
                                        await Navigation.PushPopupAsync(page);
                                        page.Render();
                                        item.Selected = true;
                                    }
                                }
                                else
                                {
                                    item.Selected = false;
                                }
                                break;
                            case 2:
                                //dat mang di
                                if (cv.id == 2 && cv.id == item.id)
                                {
                                    if (localdb.CartProd.Count == 0)
                                    {
                                        await Application.Current.MainPage.DisplayAlert("Không có sản phẩm nào trong giỏ hàng", "Vui lòng chọn sản phẩm và thực hiện thanh toán lại bạn nhé !", "Ok");
                                    }
                                    else
                                    {
                                        var page = new Delivery();
                                        await Navigation.PushPopupAsync(page);
                                        page.Render();
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
            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        async void lbldeletecart_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
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
            }
            catch (Exception ex)
            {
                //log error
                App.Current.MainPage.DisplayAlert("error", ex.ToString(), "ok");
            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        async void lbldecreasecartitem_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
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

            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        async void lblincreasecartitem_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {
                var cv = (cartItem)ctr.BindingContext;
                if(cv.prod.orderType == 0 )
                {

                }
                else if (cv.prod.orderType == -1301)
                {
                    var bmls = localdb.fullUserInfo.userGiftObjs.lst_bmls.Where(x => x.SpID == cv.prod.id).FirstOrDefault();
                    if (bmls != null)
                    {
                        if (bmls.SoLg - cv.prod.slg < 1)
                        {
                            await Application.Current.MainPage.DisplayAlert("", "Vượt quá số lượng cho phép", "Ok");
                            return;
                        }
                    }
                }
                else if(cv.prod.orderType < 0)
                {
                    List<giftObjs> giftObjs = null;
                    if (localdb.fullUserInfo != null)
                    {
                        if (localdb.fullUserInfo.userGiftObjs != null)
                        {
                            giftObjs = localdb.fullUserInfo.userGiftObjs.lst_gifts;
                        }
                    }
                    if (giftObjs != null)
                    {
                        var gift = localdb.fullUserInfo.userGiftObjs.lst_gifts.Where(x => x.TypeID == cv.prod.orderType).ToList();
                        if (gift.Count > 0)
                        {
                            var prods = localdb.CartProd.Where(x => x.orderType == cv.prod.orderType).ToList();
                            if (gift.Sum(p => p.slg) - prods.Sum(p => p.slg) < 1)
                            {
                                await Application.Current.MainPage.DisplayAlert("", "Vượt quá số lượng cho phép", "Ok");
                                return;
                            }
                        }
                    }
                }
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

            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        async void brEdit_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            using (var progress = UserDialogs.Instance.Loading("...", null, null, true, MaskType.Black))
            {
                var ctr = sender as Grid;
                var cv = (cartItem)ctr.BindingContext;
                var page = new detail_page();
                await Navigation.PushAsync(page);
                page.RenderCart(cv.prod);
            }
            this.IsEnabled = true;
        }

    }
}