using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home._contentCash
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class karunaOrder : ContentPage
    {
        public karunaOrder()
        {
            InitializeComponent();
        }
        public async Task render(string userid)
        {
            string url = $"http://karuna.vuabanhmi.com/?tablet=true&UserID={userid}";
            Device.BeginInvokeOnMainThread(() =>
            {
                wv.Source = url;
            });
        }

        private void wv_Navigated(object sender, WebNavigatedEventArgs e)
        {
            acti.IsVisible = false;
            acti.IsRunning = false;
        }
    }
}