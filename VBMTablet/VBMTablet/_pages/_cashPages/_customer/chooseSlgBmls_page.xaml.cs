using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using VBMTablet._objs._userObjs;
using VBMTablet._vms._home;
using VBMTablet._pages._home;
using Rg.Plugins.Popup.Extensions;
using VBMTablet._process;
using Syncfusion.XForms.Buttons;

namespace VBMTablet._pages._cashPages._customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class chooseSlgBmls_page : Rg.Plugins.Popup.Pages.PopupPage
    {
        gift_page gift_Page;
        CustomerGiftStatus bmls;
        public chooseSlgBmls_page(gift_page gift_Page)
        {
            InitializeComponent();
            this.gift_Page = gift_Page;
        }
        public async Task Render(CustomerGiftStatus customerGiftStatus)
        {
            this.bmls = customerGiftStatus;
            lblName.Text = customerGiftStatus.name;
            lblSlg.Text = "1";
        }

        async void grdSub_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            int sl = int.Parse(lblSlg.Text);
            if(sl > 1)
            {
                sl--;
                lblSlg.Text = sl.ToString();
            }
            else
            {
                await Navigation.PopPopupAsync();
            }
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = true;
        }
        async void grdPlus_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            int sl = int.Parse(lblSlg.Text);
            sl++;
            var slgPreview = localdb.CartProd.Where(x => x.id == bmls.bmls_obj.SpID && x.orderType == -1301).ToList().Sum(x => x.slg) + sl;
            var total = bmls.bmls_obj.SoLg;
            if(slgPreview <= total)
            {
                lblSlg.Text = sl.ToString();
            }
            else 
            {
               await Application.Current.MainPage.DisplayAlert("", "Số lượng bánh đã đạt tối đa","OK");               
            }
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = true;
        }
        async void btnOK_Clicked(object sender, EventArgs e)
        {
            var ctr = sender as SfButton;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;

            int sl = int.Parse(lblSlg.Text);
            gift_Page.addBMLSTCard(bmls, sl);
            await Navigation.PopPopupAsync();

            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = true;
        }
    }
}