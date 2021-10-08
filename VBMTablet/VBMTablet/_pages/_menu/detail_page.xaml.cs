using Rg.Plugins.Popup.Extensions;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Border;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VBMTablet._vms._detail;
using VBMTablet._objs._menuObjs;
using VBMTablet._objs._cartObjs;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VBMTablet._process;

namespace VBMTablet._pages._menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class detail_page : ContentPage
    {
        vmdetail vmdetail { get; set; }
        public detail_page()
        {
            InitializeComponent();
        }

        public async Task Render(eMenu eMenu)
        {
            vmdetail = new vmdetail(eMenu);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmdetail;
            });
        }
        public async Task RenderPromo()
        {
            
        }
        public void ff_backicon_tapped(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }

        async void ff_spice_tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            var cv = (SpiceSizeStatus)ctr.BindingContext;
            try
            {
                if(vmdetail != null)
                {
                    foreach(var item in vmdetail.spiceStatuses)
                    {
                        foreach(var item1 in item.spiceSizeStatuses.Where(x => x.SpicesItems.ref_id == cv.SpicesItems.ref_id))
                        {
                            if (item1.SpicesItems.id == cv.SpicesItems.id)
                            {
                                item1.Selected = true;
                            }
                            else
                            {
                                item1.Selected = false;
                            }
                        }
                        
                    }
                }
            }
            catch { }
        }
        async void ff_drink_tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            var cv = (DrinkComboRender)ctr.BindingContext;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                if (vmdetail != null)
                {
                    foreach (var item in vmdetail.drink_Combos)
                    {
                        if (item.Drink_Combo.spId == cv.Drink_Combo.spId)
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                    vmdetail.ChangePrice();
                }
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch (Exception ex)
            {
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);

            }
        }

        private void decreaseExSl_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            var cv = (ExtraStatus)ctr.BindingContext;
            if(cv.sl > 0)
            {
                cv.sl--;
            }
            if(vmdetail != null)
            {
                vmdetail.ChangePrice();
            }
        } 
        private void increaseExSl_tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            var cv = (ExtraStatus)ctr.BindingContext;
            cv.sl++;
            if (vmdetail != null)
            {
                vmdetail.ChangePrice();
            }
        } 
        private void decreaseSl_tapped(object sender, EventArgs e)
        {
            if(vmdetail != null)
            {
                if(vmdetail.slg > 1)
                {
                    vmdetail.slg--;
                    vmdetail.ChangePrice();
                }
            }
        } 
        private void increaseSl_tapped(object sender, EventArgs e)
        {
            if (vmdetail != null)
            {
                vmdetail.slg++;
                vmdetail.ChangePrice();
            }
        }

        async void bd_dathang_tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                if(vmdetail != null)
                {
                    long id = 0;
                    foreach(var item in vmdetail.drink_Combos)
                    {
                        if (item.Selected == true)
                        {
                            id = item.Drink_Combo.spId;
                        }
                    }
                    var data = CartProd.createAddProd(vmdetail.monid, id != null ? id : 0);
                    if(data != null)
                    {
                        data.slg = vmdetail.slg;
                        foreach(var t1 in vmdetail.extraStatuses)
                        {
                            if(t1.sl > 0)
                            {
                                data.LstExts.Add(new cartExtra { dongia = t1.Extra.price, id = t1.Extra.id, nguyengia = t1.Extra.price, name = new Dictionary<string, string>(t1.Extra.names), solg = t1.sl });
                            }
                        }
                        foreach(var t1 in vmdetail.spiceStatuses)
                        {
                            foreach(var t2 in t1.spiceSizeStatuses)
                            {
                                if(t2.idselected != 2 && t2.Selected == true)
                                {
                                    data.LstSpls.Add(new cartSpice { id = t2.SpicesItems.id, name = new Dictionary<string, string>(t2.SpicesItems.names) });
                                }
                            }
                        }
                    }
                    localdb.CartProd.Add(data);                    
                }
                Navigation.RemovePage(this);

                if (localdb.home_Page != null)
                    localdb.home_Page.updateSlCart();
            }
            catch(Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        async void proSize_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            var cv = (SizeRender)ctr.BindingContext;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                if(vmdetail != null)
                {
                    vmdetail.monid = cv.Size.id;
                    foreach(var item in vmdetail.sizeRenders)
                    {
                        if(item.Size.id == cv.Size.id)
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                    vmdetail.ChangePrice();
                }
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch(Exception ex)
            {
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);

            }
        }
    }
}