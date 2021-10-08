using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._vms._home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class home_view : ContentView
    {
        vmhome vm { get; set; }
        public home_view()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            vm = new vmhome();
            this.BindingContext = vm;
        }
        async void stl_title_tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var title = sender as StackLayout;
            await title.ScaleTo(0.8, 1);
            var selected = (MenuTab)title.BindingContext;
            foreach (var items in vm.menuTabs)
            {
                if (items.Index == selected.Index)
                {
                    if (selected.Index == 0)
                    {
                        stlHomeMenu.Children.Clear();
                        items.Selected = true;
                        stlHomeMenu.Children.Add(new _pages._home.customer_page());
                    }
                    if (selected.Index == 1)
                    {
                        stlHomeMenu.Children.Clear();
                        items.Selected = true;
                        stlHomeMenu.Children.Add(new _pages._home.script_page());
                    }
                    if (selected.Index == 2)
                    {
                        stlHomeMenu.Children.Clear();
                        items.Selected = true;
                        stlHomeMenu.Children.Add(new _pages._home.gift_page());
                    }
                    if (selected.Index == 3)
                    {
                        stlHomeMenu.Children.Clear();
                        items.Selected = true;
                        stlHomeMenu.Children.Add(new _pages._home.historypage());
                    }
                }
                else
                {
                    items.Selected = false;
                }
            }
            await title.ScaleTo(1, 250);
            this.IsEnabled = true;
        }

    }

}