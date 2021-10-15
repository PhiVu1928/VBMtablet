using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VBMTablet._vms._home;
using VBMTablet._objs._userObjs;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class customer_page : ContentView
    {
        CustomerStatus CustomerStatus { get; set; }
        public customer_page()
        {
            InitializeComponent();
        }
        public async Task Render(userinfo userinfo)
        {
            CustomerStatus = new CustomerStatus(userinfo);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = CustomerStatus;
            });
        }
    }
}