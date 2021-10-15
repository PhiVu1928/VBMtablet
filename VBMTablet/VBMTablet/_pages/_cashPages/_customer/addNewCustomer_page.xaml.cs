using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VBMTablet._vms._cashPage;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class addNewCustomer_page : Rg.Plugins.Popup.Pages.PopupPage
    {
        addNewUserPageVM addNewUserPageVM { get; set; }
        public addNewCustomer_page()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            addNewUserPageVM = new addNewUserPageVM();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = addNewUserPageVM;
            });
        }
        async void ff_close_tapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        private void stlAge_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as StackLayout;
            var cv = (UserAgeStatus)ctr.BindingContext;
            try
            {
                var selected = cv.id;
                foreach(var item in addNewUserPageVM.userAgeStatuses)
                {
                    if(selected == item.id)
                    {
                        item.selected = true;
                    }
                    else
                    {
                        item.selected = false;
                    }
                }
            }
            catch { }
        }

        private void stlGioiTinh_tapped(object sender, EventArgs e)
        {
            var ctr = sender as StackLayout;
            var cv = (UserGioiTinhStatus)ctr.BindingContext;
            try
            {
                var selected = cv.id;
                foreach(var item in addNewUserPageVM.userGioiTinhStatuses)
                {
                    if(selected == item.id)
                    {
                        item.selected = true;
                    }
                    else
                    {
                        item.selected = false;
                    }
                }
            }
            catch { }
        }
    }
}