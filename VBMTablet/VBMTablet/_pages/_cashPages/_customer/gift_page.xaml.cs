using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VBMTablet._objs._userObjs;
using VBMTablet._process;
using VBMTablet._vms._home;
using VBMTablet._pages._cashPages._customer;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using Acr.UserDialogs;
using VBMTablet._objs._cartObjs;

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

        async void grdGift_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var cv = (CustomerGiftStatus)ctr.BindingContext;
                if(cv.bmls_obj != null)
                {
                    var added = localdb.CartProd.Where(x => x.id == cv.bmls_obj.SpID && x.orderType == -1301).ToList().Sum(x => x.slg);
                    var total = cv.bmls_obj.SoLg;
                    if(total - added > 0)
                    {
                        var PopupBmls = new chooseSlgBmls_page(this);
                        await Navigation.PushPopupAsync(PopupBmls);
                        PopupBmls.Render(cv);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Quà tặng đã có trong giỏ hàng", "OK");
                    }
                }
                else if(cv.GiftObjs != null)
                {
                    var added = localdb.CartProd.Where(x => x.orderType == cv.GiftObjs.TypeID).ToList().Sum(x => x.slg);
                    var total = cv.GiftObjs.slg;
                    if(total - added > 0)
                    {
                        var page = new giftDetail_Page();
                        await Navigation.PushAsync(page);
                        page.Render(cv);
                    }
                    else
                    {

                    }
                }
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch { }
        }
        public async Task addBMLSTCard(CustomerGiftStatus bmls, int slg)
        {
            using (var progress = UserDialogs.Instance.Loading("...", null, null, true, MaskType.Black))
            {
                var existProd = localdb.CartProd.Where(p => p.id == bmls.bmls_obj.SpID && p.orderType == -1301).FirstOrDefault();
                if (existProd != null)
                {
                    existProd.slg += slg;
                    localdb.home_Page.updateSlCart();
                    await Application.Current.MainPage.DisplayAlert("", "Quà tặng đã có trong giỏ hàng", "OK");
                }
                else
                {
                    var data = CartProd.createAddProd(bmls.bmls_obj.SpID, -1);
                    if (data != null)
                    {
                        data.slg = slg;
                        data.orderType = -1301;
                        data.orderCode = "dd";
                        data.dongia = 0;
                        localdb.CartProd.Add(data);
                        localdb.home_Page.updateSlCart();
                        await Application.Current.MainPage.DisplayAlert("", "Sản phẩm đã được đưa vào giỏ hàng", "OK");

                    }
                    else
                    {
                        
                    }
                }
            }
        }
    }
}