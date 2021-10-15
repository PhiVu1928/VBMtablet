using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._thanhtoan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class hinh_thuc_thanh_toan_page : Rg.Plugins.Popup.Pages.PopupPage
    {
        public hinh_thuc_thanh_toan_page()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
        }
        async void ff_close_tapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           
        }
    }
}