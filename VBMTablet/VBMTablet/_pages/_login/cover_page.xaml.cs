using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._pages._login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class cover_page : ContentPage
    {
        public cover_page()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            localdb.cover_Page = this;
            tools.startAppAction();
        }
        public async void start_app()
        {
            var login_page = new _login.login_page();
            await Navigation.PushAsync(login_page);
            login_page.Render(); 
        }
    }
}