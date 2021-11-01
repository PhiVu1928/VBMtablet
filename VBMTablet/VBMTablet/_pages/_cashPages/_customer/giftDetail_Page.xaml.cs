using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._pages._home;
using VBMTablet._vms._home;
using VBMTablet._vms._cashPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using VBMTablet._objs._userObjs;
using Acr.UserDialogs;
using VBMTablet._process;
using VBMTablet._objs._cartObjs;

namespace VBMTablet._pages._cashPages._customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class giftDetail_Page : ContentPage
    {
        vmGiftDetail vm;
        CustomerGiftStatus CustomerGiftStatus;
        public giftDetail_Page()
        {
            InitializeComponent();
        }
        public async Task Render(CustomerGiftStatus gift)
        {
            this.CustomerGiftStatus = gift;
            vm = new vmGiftDetail(gift);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vm;
            });
        }

        private void ff_backicon_tapped(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }

        async void grdChooseGiftItem_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var cv = (giftDetailItem)ctr.BindingContext;
                var ChooseGiftItemPage = new VBMTablet._pages._cashPages._customer.chooseGiftItem_page();
                await Navigation.PushPopupAsync(ChooseGiftItemPage);
                ChooseGiftItemPage.Render(this, CustomerGiftStatus.GiftObjs, cv.gift_Items, cv.eMenu);
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch { }
        }
        public async Task addGiftItemTCard(gift_item giftSize, long id, int slg)
        {
            using (var progress = UserDialogs.Instance.Loading("...", null, null, true, MaskType.Black))
            {
                var existProd = localdb.CartProd.Where(p => p.id == id && p.orderType == vm.CustomerGiftStatus.GiftObjs.TypeID).FirstOrDefault();
                if (existProd != null)
                {
                    existProd.slg += slg;
                    localdb.home_Page.updateSlCart();
                    await Application.Current.MainPage.DisplayAlert("", "Quà tặng đã có trong giỏ hàng", "OK");
                }
                else
                {
                    var data = CartProd.createAddProd(id, -1);
                    if (data != null)
                    {
                        data.slg = slg;
                        data.orderType = vm.CustomerGiftStatus.GiftObjs.TypeID;
                        data.dongia = giftSize.dongia;
                        data.orderCode = vm.CustomerGiftStatus.GiftObjs.Code;
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