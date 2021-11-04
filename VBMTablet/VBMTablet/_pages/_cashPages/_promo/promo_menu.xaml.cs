using Acr.UserDialogs;
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
    public partial class promo_menu : ContentPage
    {
        public promo_menu()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }
        async void GrPromoItemTapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {
                using (var progress = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
                {
                    
                }           
            }
            catch
            {
                
            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }
    }
}