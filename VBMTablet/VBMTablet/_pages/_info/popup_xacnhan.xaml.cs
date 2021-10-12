﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;

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

        async void ff_ok_tapped(object sender, EventArgs e)
        {
            await xacnhan.ScaleTo(0.9, 1);
            await xacnhan.FadeTo(0.9, 1);
            try
            {                
                var homepage = new VBMTablet._pages._home.home_page();
                await Navigation.PushAsync(homepage);
                homepage.render();
                await Navigation.PopPopupAsync();
                await xacnhan.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch (Exception)
            {
                await xacnhan.ScaleTo(1, 100);
                await xacnhan.FadeTo(1, 100);
            }
        }
        async void ff_close_tapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}