using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._cashPages._thanhtoan._orderoption
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Delivery : Rg.Plugins.Popup.Pages.PopupPage
    {
        public Delivery()
        {
            InitializeComponent();
        }

        public async Task Render()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PickDay.Date = DateTime.Now;
                PickTime.Time = DateTime.Now.TimeOfDay;
            });
        }

        async void ff_close_tapped(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}