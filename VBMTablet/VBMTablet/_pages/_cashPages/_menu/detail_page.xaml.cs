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
using VBMTablet._objs._userObjs;

namespace VBMTablet._pages._menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class detail_page : ContentPage
    {
        vmdetail vmdetail { get; set; }
        vmCartEdit vmCartEdit { get; set; }
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
                grMain.IsVisible = true;
            });
        }
        public async Task RenderCart(CartProd cartProd)
        {
            vmCartEdit = new vmCartEdit(cartProd);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmCartEdit;
                grMain.IsVisible = true;
            });
            vmCartEdit.ChangePrice();
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
                if(vmCartEdit != null)
                {
                    foreach(var item in vmCartEdit.spiceStatuses)
                    {
                        foreach (var item1 in item.spiceSizeStatuses.Where(x => x.SpicesItems.ref_id == cv.SpicesItems.ref_id))
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
            this.IsEnabled = false;
            try
            {
                if (vmdetail != null)
                {
                    foreach (var item in vmdetail.drink_Combos)
                    {
                        if (item.Drink_Combo.spId == cv.Drink_Combo.spId && item.Selected == true)
                        {
                            item.Selected = false;
                        }
                        else if (item.Drink_Combo.spId == cv.Drink_Combo.spId)
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
                if(vmCartEdit != null)
                {
                    vmCartEdit.nuocid = cv.Drink_Combo.spId;
                    vmCartEdit.ChangePrice();
                    foreach(var item in vmCartEdit.drink_Combos)
                    {
                        if (item.Drink_Combo.spId == cv.Drink_Combo.spId && item.Selected == true)
                        {
                            item.Selected = false;
                        }
                        else if (item.Drink_Combo.spId == cv.Drink_Combo.spId)
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
            }
            catch (Exception ex)
            {

            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
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
            if(vmCartEdit != null)
            {
                vmCartEdit.ChangePrice();
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
            if (vmCartEdit != null)
            {
                vmCartEdit.ChangePrice();
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
            if(vmCartEdit != null)
            {
                if(vmCartEdit.slg > 1)
                {
                    vmCartEdit.slg--;
                    vmCartEdit.ChangePrice();
                }
            }
        } 
        async void increaseSl_tapped(object sender, EventArgs e)
        {
            if (vmdetail != null)
            {
                vmdetail.slg++;
                vmdetail.ChangePrice();
            }
            if(vmCartEdit != null)
            {
                if (vmCartEdit.CartProd.orderType < 0)
                {
                    if (vmCartEdit.CartProd.orderType == -1301)
                    {
                        var bmls = localdb.fullUserInfo.userGiftObjs.lst_bmls.Where(x => x.SpID == vmCartEdit.CartProd.id).FirstOrDefault();
                        if (bmls != null)
                        {
                            if (bmls.SoLg - vmCartEdit.slg < 1)
                            {
                                await Application.Current.MainPage.DisplayAlert("", "Vượt quá số lượng cho phép", "Ok");
                                return;
                            }
                        }
                    }
                    else
                    {
                        List<giftObjs> giftObjs = null;
                        if (localdb.fullUserInfo != null)
                        {
                            if(localdb.fullUserInfo.userGiftObjs != null)
                            {
                                giftObjs = localdb.fullUserInfo.userGiftObjs.lst_gifts;
                            }
                        }
                        if(giftObjs != null)
                        {
                            var gift = localdb.fullUserInfo.userGiftObjs.lst_gifts.Where(x => x.TypeID == vmCartEdit.CartProd.orderType).ToList();
                            if(gift.Count > 0)
                            {
                                var prods = localdb.CartProd.Where(x => x.orderType == vmCartEdit.CartProd.orderType).ToList();
                                if (gift.Sum(p => p.slg) - (prods.Sum(p => p.slg) - vmCartEdit.CartProd.slg + vmCartEdit.slg) < 1)
                                {
                                    await Application.Current.MainPage.DisplayAlert("", "Vượt quá số lượng cho phép", "Ok");
                                    return;
                                }
                            }
                        }
                    }
                }
                vmCartEdit.slg++;
                vmCartEdit.ChangePrice();
            }
        }

        async void bd_dathang_tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            this.IsEnabled = false;
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
                if(vmCartEdit != null)
                {
                    long id = 0;
                    foreach (var item in vmCartEdit.drink_Combos)
                    {
                        if (item.Selected == true)
                        {
                            id = item.Drink_Combo.spId;
                        }
                    }
                    if(id != 0)
                    {
                        vmCartEdit.nuocid = id;
                    }
                    else
                    {
                        vmCartEdit.nuocid = 0;
                    }
                    var data = CartProd.createAddProd(vmCartEdit.monid, vmCartEdit.nuocid);
                    data.slg = vmCartEdit.slg;
                    if (vmCartEdit.CartProd.orderType < 0)
                    {
                        data.orderType = vmCartEdit.CartProd.orderType;
                        data.orderCode = vmCartEdit.CartProd.orderCode;
                        data.dongia = vmCartEdit.CartProd.dongia;
                        data.isGetLater = vmCartEdit.CartProd.isGetLater;
                        data.isGetLaterOption = vmCartEdit.CartProd.isGetLaterOption;
                    }

                    foreach (var t1 in vmCartEdit.extraStatuses)
                    {
                        if (t1.sl > 0)
                        {
                            data.LstExts.Add(new cartExtra { dongia = t1.Extra.price, nguyengia = t1.Extra.price, id = t1.Extra.id, name = new Dictionary<string, string>(t1.Extra.names), solg = t1.sl });
                        }
                    }
                    foreach (var t1 in vmCartEdit.spiceStatuses)
                    {
                        foreach (var t2 in t1.spiceSizeStatuses)
                        {
                            if (t2.idselected != 2 && t2.Selected == true)
                            {
                                data.LstSpls.Add(new cartSpice { id = t2.SpicesItems.id, name = new Dictionary<string, string>(t2.SpicesItems.names) });
                            }
                        }
                    }
                    localdb.CartProd.Insert(localdb.CartProd.IndexOf(vmCartEdit.CartProd), data);
                    

                    localdb.CartProd.Remove(vmCartEdit.CartProd);

                    localdb.thanh_Toan_Page.vmcart.RenderCartItem();
                    localdb.thanh_Toan_Page.vmcart.CallMoney();
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
            this.IsEnabled = false;
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
                if(vmCartEdit != null)
                {
                    vmCartEdit.monid = cv.Size.id;
                    vmCartEdit.ChangePrice();
                    foreach(var item in vmCartEdit.sizeRenders)
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
                }
            }
            catch(Exception ex)
            {
                
            }
            await ctr.ScaleTo(1, 100);
            this.IsEnabled = true;
        }
    }
}