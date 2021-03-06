using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
using VBMTablet._process;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class popup_xacnhan : Rg.Plugins.Popup.Pages.PopupPage
    {
        public popup_xacnhan()
        {
            InitializeComponent();
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        async void ff_ok_tapped(object sender, EventArgs e)
        {
            await xacnhan.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {                
                if(localdb.NhanVieninfo != null)
                {
                    if(ETInputMNV.Text == localdb.NhanVieninfo.UserID.ToString())
                    {
                        var homepage = new _home.home_page();
                        await Navigation.PushAsync(homepage);
                        homepage.render();
                        await Navigation.PopPopupAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Mã nhân viên không đúng", "OK");
                    }
                }                
            }
            catch (Exception)
            {
                
            }
            await xacnhan.ScaleTo(1, 100);
            this.IsEnabled = true;
        }

        async void ff_close_tapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}