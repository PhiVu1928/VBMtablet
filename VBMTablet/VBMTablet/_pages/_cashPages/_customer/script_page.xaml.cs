using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._objs._userObjs;
using VBMTablet._vms._home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class script_page : ContentView
    {
        vmCustomerScript vm { get; set; }
        public script_page()
        {
            InitializeComponent();
        }
        public async Task Render(userinfo userinfo)
        {
            vm = new vmCustomerScript(userinfo);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vm;
            }); 
        }
    }
}