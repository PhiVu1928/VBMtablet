using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Border;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._vms._cart;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._cashPages._thanhtoan._orderoption
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        vmPickUp vm { get; set; }
        public PickUp()
        {
            InitializeComponent();
        }

        public async Task Render()
        {
            vm = new vmPickUp();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vm;
                PickDay.Date = DateTime.Now;
                PickTime.Time = DateTime.Now.TimeOfDay;
            });
            
        }

        async void bdChangeMode_Tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            vm.isTakeNow = !vm.isTakeNow;
            await ctr.ScaleTo(1, 00);
            this.IsEnabled = true;
        }

        private void sfpickstore_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ctr = sender as Picker;
            var cv = (StoreStatus)ctr.SelectedItem;
        }

        private void ff_close_tapped(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        

    }
}