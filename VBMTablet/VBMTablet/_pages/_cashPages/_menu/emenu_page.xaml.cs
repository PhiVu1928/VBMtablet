using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using VBMTablet._objs._menuObjs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using VBMTablet._vms._menu;

namespace VBMTablet._pages._menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class emenu_page : ContentView
    {
        public emenu_page(GroupMenu groupMenu)
        {
            InitializeComponent();
            this.BindingContext = groupMenu;
        }           
            

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.8, 1);
            this.FadeTo(0.8, 1);
            try
            {
                var cv = (emenuRender)ctr.BindingContext;
                var detail = new _pages._menu.detail_page();
                await Navigation.PushAsync(detail);
                detail.Render(cv.emenu);
                await ctr.ScaleTo(1, 100);
                this.FadeTo(1, 100);
            }
            catch(Exception)
            {
                await ctr.ScaleTo(1, 100);
                this.FadeTo(1, 100);
            }
            
        }
    }
}