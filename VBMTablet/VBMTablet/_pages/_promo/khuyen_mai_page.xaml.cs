using Acr.UserDialogs;
using Syncfusion.XForms.Border;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._promo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class khuyen_mai_page : ContentView
    {
        public khuyen_mai_page()
        {
            InitializeComponent();
        }
        public async Task Render()
        {      
        }
       
        

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch
            {
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            
        }
    }
}