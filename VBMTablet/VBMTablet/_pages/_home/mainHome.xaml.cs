using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using VBMTablet._process;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VBMTablet._vms._homeVMs;
using Syncfusion.XForms.Border;
using VBMTablet._objs._staffObjs;
using VBMTablet._pages._home._contentCash;

namespace VBMTablet._pages._info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class info_page : ContentPage
    {
        homePageVM vm;

        public info_page()
        {
            InitializeComponent();
        }

        public async Task render()
        {
            vm = new homePageVM();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vm;
            });
        }

        private void ffimg_left_tapped(object sender, EventArgs e)
        {
            localdb.outlinePage.open_flyout();
        }

        async void bdChangeMode_Tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.8, 150);

            vm.isMl = !vm.isMl;
            localdb.isMLMode = vm.isMl;

            await ctr.ScaleTo(1, 150);
            this.IsEnabled = true;
        }

        async void ff_order_tapped(object sender, EventArgs e)
        {
            await ordericon.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {
                var popup = new popup_xacnhan();
                await Navigation.PushPopupAsync(popup);
            }
            catch(Exception)
            {
                //error show here
            }
            await ordericon.ScaleTo(0.9, 1);
            this.IsEnabled = true;
        }
        async void imgKaruna_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Image;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            try
            {
                string result = await Application.Current.MainPage.DisplayPromptAsync("Nhân viên", "Mã nhân viên của bạn là gì nhỉ?", "OK", "", "Mã nhân viên", 10, Keyboard.Numeric, "");
                if (!string.IsNullOrEmpty(result))
                {
                    bool isTrue = false;

                    foreach (staff objU in localdb.FullNhanVienInfo)
                    {
                        if (((int)objU.UserID).ToString() == result.Trim())
                        {
                            isTrue = true;
                            break;
                        }
                    }

                    if (!isTrue)
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Vui lòng nhập đúng mã nhân viên", "OK");
                    }
                    else
                    {
                        var page = new karunaOrder();
                        await Navigation.PushAsync(page);
                        page.render(result.Trim());
                    }

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("", "Vui lòng nhập mã nhân viên", "OK");
                }
                await ctr.ScaleTo(1, 100);
            }
            catch(Exception)
            {
                //error show here
            }
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = true;
        }

    }
}