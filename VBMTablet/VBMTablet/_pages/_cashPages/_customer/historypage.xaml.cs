using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._objs._cartObjs;
using VBMTablet._objs._userObjs;
using VBMTablet._process;
using VBMTablet._vms._home;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class historypage : ContentView
    {
        vmCustomerOrdered CustomerOrderedStatus { get; set; }
        public historypage()
        {
            InitializeComponent();
        }
        public async Task Render(List<userOrdered> userOrdered)
        {
            CustomerOrderedStatus = new vmCustomerOrdered(userOrdered);
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = CustomerOrderedStatus;
            });
        }

        async void grdReOrder_Tapped(object sender, EventArgs e)
        {
            var ctr = sender as Grid;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var cv = (CustomerOrderedStatus)ctr.BindingContext;
                var ques = await Application.Current.MainPage.DisplayAlert("", "Bạn muốn đặt lại theo đơn hàng này ?", "Ok", "No");
                if (ques)
                {
                    foreach (var item in cv.listBillDetail.Where(x => x.IsExtra == 0))
                    {
                        var data = CartProd.createAddHisProd(item.SpID);
                        if (data != null)
                        {
                            data.slg = item.SoLg;
                            foreach (var item2 in cv.listBillDetail.Where(x => x.IsExtra == 1))
                            {
                                if (item2 != null)
                                {
                                    if (item2.DonGia > 0)
                                    {
                                        foreach (var item3 in localdb.extra_Spices.extras)
                                        {
                                            if (item2.SpID == item3.id)
                                            {
                                                data.LstExts.Add(new cartExtra { dongia = item2.DonGia, id = item2.SpID, nguyengia = item2.NguyenGia, solg = item2.SoLg, name = new Dictionary<string, string>(item3.names) });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach(var item3 in localdb.extra_Spices.spices)
                                        {
                                            foreach(var item4 in item3.lst_items)
                                            {
                                                if (item2.SpName  == item4.nameVN)
                                                {
                                                    data.LstSpls.Add(new cartSpice { id = item2.SpID, name = new Dictionary<string, string>(item4.names) });
                                                }
                                            }                                            
                                        }
                                    }
                                }
                            }
                        }
                        localdb.CartProd.Add(data);
                        localdb.home_Page.updateSlCart();
                    }
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