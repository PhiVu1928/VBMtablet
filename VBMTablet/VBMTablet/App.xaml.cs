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
            #if DEBUG
            HotReloader.Current.Run(this);
            #endif
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {          
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTE3NDY5QDMxMzkyZTMzMmUzME0vcHpQc21LMVZaQ2xRd3JBN3dKdGhKSmFKQzNzeUZ5V1FjdnZCZkd3RUk9");
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
