using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Border;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._objs._menuObjs;
using VBMTablet._objs._userObjs;
using VBMTablet._process;
using VBMTablet._vms._cashPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._cashPages._customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class chooseGiftItem_page : Rg.Plugins.Popup.Pages.PopupPage
    {
        giftDetail_Page giftDetail_Page;
        giftObjs giftObjs;
        vmChooseGiftItem vmChooseGiftItem;

        public chooseGiftItem_page()
        {
            InitializeComponent();
        }
        public async Task Render(giftDetail_Page giftDetail_Page, giftObjs giftObjs, List<gift_item> gift_Items,eMenu eMenu)
        {
            this.giftDetail_Page = giftDetail_Page;
            this.giftObjs = giftObjs;
            vmChooseGiftItem = new vmChooseGiftItem(gift_Items, eMenu);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmChooseGiftItem;
            });
        }
        async void imgSub_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as Image;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            if (vmChooseGiftItem.solg == 1)
            {
                await Navigation.PopPopupAsync();
            }
            else
            {
                vmChooseGiftItem.solg--;
            }

            await ctr.ScaleTo(1, 100);
            await this.FadeTo(1, 100);
        }
        async void imgPlus_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as Image;
            await ctr.ScaleTo(0.9, 150);

            int curSlg = localdb.CartProd.Where(p => p.orderType == giftObjs.TypeID).ToList().Sum(p => p.slg);
            if (curSlg + vmChooseGiftItem.solg < giftObjs.slg)
            {
                vmChooseGiftItem.solg++;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("","Vượt quá số lượng cho phép","Ok");
            }

            await ctr.ScaleTo(1, 100);
            await this.FadeTo(1, 100);
        }
        async void bdSize_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);

            var cv = (chooseGiftItemSizeVM)ctr.BindingContext;
            if (!cv.selected)
            {
                foreach (var item in vmChooseGiftItem.chooseGiftItemSizeVMs)
                {
                    item.selected = false;
                }
                cv.selected = true;
            }

            await ctr.ScaleTo(1, 100);
            await this.FadeTo(1, 100);
        }

        async void btnOK_Clicked(object sender, EventArgs e)
        {
            var ctr = sender as SfButton;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);

            await Navigation.PopPopupAsync();
            var cv = vmChooseGiftItem.chooseGiftItemSizeVMs.Where(p => p.selected).FirstOrDefault();
            await giftDetail_Page.addGiftItemTCard(cv.giftSize, cv.size_menu.id, vmChooseGiftItem.solg);

            await ctr.ScaleTo(1, 100);
            await this.FadeTo(1, 100);
        }
    }
}