using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {          
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDIzMzcyQDMxMzkyZTMxMmUzMFd5Q3VEbkFlWFIzY0F3bExsZlRlK2ExSzREZFVHdUxHSTlJL002eDQreEU9");
            MainPage = new NavigationPage(new _pages._login.cover_page());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
