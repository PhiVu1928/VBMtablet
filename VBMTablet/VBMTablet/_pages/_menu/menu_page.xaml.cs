using Syncfusion.XForms.TabView;
using System;
using System.Linq;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._vms._menu;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class menu_page : ContentView
    {
        vmmenu vmmenu { get; set; }
        public menu_page()
        {
            InitializeComponent();
            
        }
        public async Task Render()
        {
            vmmenu = new vmmenu();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmmenu;
                vmmenu.SfTabView.SelectionChanged += SfTabView_SelectionChanged;
                grouptitle.Children.Add(vmmenu.SfTabView);
                busyindicator.IsBusy = false;
                busyindicator.IsVisible = false;
            });
            localdb.menu_Page = this;
        }

        private async void SfTabView_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            int index = vmmenu.SfTabView.SelectedIndex;
            for(int i = 0; i < vmmenu.sfTabItems.Count ; i++)
            {
                var cv = vmmenu.sfTabItems[i];
                if(index == i)
                {
                    var cv2 = (GroupMenu)cv.Content.BindingContext;
                    if(cv2.emenu == null)
                    {
                        busyindicator.IsBusy = true;
                        busyindicator.IsVisible = true;
                        cv2.RenderEmenu(true);
                        await Task.Delay(1000);
                        busyindicator.IsBusy = false;
                        busyindicator.IsVisible = false;
                    }
                }
                else
                {
                    cv.TitleFontColor = Color.Gray;
                }
            }
        }



    }
}