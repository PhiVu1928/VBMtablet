using Syncfusion.XForms.Border;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VBMTablet._vms._orderedInDay;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home._menuFloatingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class billInDayPage : ContentPage
    {
        vmBillsInDay vm { get; set; }
        public billInDayPage()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            vm = new vmBillsInDay();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vm;
            });
        }

        private void FF_left_Tapped(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }
        async void brdBillDetail_tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            var cv = (BillStatus)ctr.BindingContext;
            foreach(var item in vm.billStatuses)
            {
                if(item.BillID == cv.BillID)
                {
                    item.visDetail = true;
                }
                else
                {
                    item.visDetail = false;
                }
            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }
    }
}