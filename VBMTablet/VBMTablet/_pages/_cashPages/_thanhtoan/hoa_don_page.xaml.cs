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
    public partial class hoa_don_page : Rg.Plugins.Popup.Pages.PopupPage
    {
        public hoa_don_page()
        {
            InitializeComponent();
        }
        async void ff_close_tapped(object sender,EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}