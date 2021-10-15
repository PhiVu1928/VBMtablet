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
    public partial class historypage : ContentView
    {
        vmCustomerOrdered CustomerOrderedStatus { get; set; }
        public historypage()
        {
            InitializeComponent();
        }
        public async Task Render(List<userOrdered> userOrdered)
        {
            CustomerOrderedStatus = new vmCustomerOrdered(userOrdered);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = CustomerOrderedStatus;
            });
        }
    }
}