using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login_page : ContentPage
    {
        public login_page()
        {
            InitializeComponent();
        }

        public async Task Render()
        {
            
        }
        async void login_Tapped(object sender, EventArgs e)
        {
            await btnlogin.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var outlinepage = new VBMTablet._pages._info.outline_page();
                    Navigation.PushAsync(outlinepage);
                    outlinepage.start_app();
                });
                await btnlogin.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch(Exception)
            {
                await btnlogin.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            
            
        }
    }
}