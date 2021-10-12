using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VBMTablet._objs._promoObjs;
using VBMTablet._utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._promo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class promo_detail : Rg.Plugins.Popup.Pages.PopupPage
    {
        public promo_detail()
        {
            InitializeComponent();
        }
        public async Task Render(promotionObjs promotionObjs)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                webview.Source = promotionObjs.urlVN;
            });
        }

        async void ff_close_tapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}