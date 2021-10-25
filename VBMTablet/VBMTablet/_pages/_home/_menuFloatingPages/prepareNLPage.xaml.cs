using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home._menuFloatingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class prepareNLPage : ContentPage
    {
        public prepareNLPage()
        {
            InitializeComponent();
        }
        public async Task Render()
        {
            string raw = DateTime.Now.ToString("yyyy/MM/dd");
            string hashed = tools.signSHA256(raw, "uks4tqKcoenCeSW9FdxlWDlRLrouHuTF");
            WVPrepareNL.Source = "http://manage.vuabanhmi.com/PrepNLKiosk3Moment.aspx?ShopID=" + localdb.shopID + "&signature=" + hashed + "&token=" + tools.genToken();
            WVPrepareNL.Navigated += WVPrepareNL_Navigated;
            WVPrepareNL.Navigating += WVPrepareNL_Navigating;
        }

        private void WVPrepareNL_Navigating(object sender, WebNavigatingEventArgs e)
        {
            lblPrepareName.Text = "Đang tải, vui lòng chờ...";
            WVPrepareNL.IsVisible = false;
            busyindicator.IsEnabled = true;
            busyindicator.IsVisible = true;
        }

        private void WVPrepareNL_Navigated(object sender, WebNavigatedEventArgs e)
        {
            lblPrepareName.Text = "Chuẩn bị nguyên liệu";
            WVPrepareNL.IsVisible = true;
            busyindicator.IsEnabled = false;
            busyindicator.IsVisible = false;
        }

        private void FF_left_Tapped(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }
    }
}