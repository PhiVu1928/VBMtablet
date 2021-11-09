using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._vms._karuna;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home._menuFloatingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class lstBillKarunaPage : ContentPage
    {
        vmKarunaPage vm { get; set; }
        public lstBillKarunaPage()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            vm = new vmKarunaPage();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vm;
            });
            vm.generateVM();
        }
        private void FF_left_Tapped(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }
    }
}