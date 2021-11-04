using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._vms._login;
using VBMTablet._objs._storeObjs;
using VBMTablet._objs._staffObjs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VBMTablet._utils;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using VBMTablet._pages._info;
using Syncfusion.XForms.Border;

namespace VBMTablet._pages._login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login_page : ContentPage
    {
        vmlogin vmlogin { get; set; }
        public login_page()
        {
            InitializeComponent();
        }

        public async Task Render()
        {
            vmlogin = new vmlogin();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.BindingContext = vmlogin;
            });
        }

        private void sfpickstore_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ctr = sender as Picker;
            var cv = (StoreStatus)ctr.SelectedItem;
            localdb.shopID = cv.StoreObj.ShopID;
            //lay full nhan vien info su dung signalR
            //localdb.signalR.HubProxy.Invoke("SystemAction", "doListAllNV{}" + localdb.shopID + "$0{}");
        }

        async void bdChangeMode_Tapped(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.8, 150);

            vmlogin.isMl = !vmlogin.isMl;

            await ctr.ScaleTo(1, 150);
            this.IsEnabled = true;
        }

        async void login_Tapped(object sender, EventArgs e)
        {
            await btnlogin.ScaleTo(0.9, 1);
            this.IsEnabled = false;
            string sdt = vmlogin.sdt;
            string pwd = vmlogin.pwd;

            if (string.IsNullOrEmpty(sdt))
            {
                await Application.Current.MainPage.DisplayAlert("", "Bạn chưa nhập số điện thoại bạn nhé !", "OK");
            }
            if (string.IsNullOrEmpty(pwd))
            {
                await Application.Current.MainPage.DisplayAlert("", "Bạn chưa nhập mật khẩu bạn nhé !", "OK");
            }
            if(sfpickstore.SelectedItem == null)
            {
                await Application.Current.MainPage.DisplayAlert("", "bạn chưa chọn cửa hàng", "OK");
            }
            else
            {
                sdt = sdt[0].ToString() == "0" ? sdt : 0 + sdt;
                pwd = tools.EncodePassword(pwd);

                //Login theo fullNhanVienInfo su dung signalR

                //foreach(var item in localdb.FullNhanVienInfo)
                //{
                //    if (sdt != item.Username)
                //    {
                //        await Application.Current.MainPage.DisplayAlert("", "Số điện thoại không đúng", "OK");
                //        break;
                //    }
                //    if (pwd != item.Pwd)
                //    {
                //        await Application.Current.MainPage.DisplayAlert("", "Mật khẩu không đúng", "OK");
                //        break;
                //    }
                //    if (sdt == item.Username && pwd == item.Pwd)
                //    {
                //        localdb.NhanVieninfo = item;
                //        var outlinePage = new outline_page();
                //        await Navigation.PushAsync(outlinePage);
                //        outlinePage.start_app();
                //        break;
                //    }

                //}

                //Login du dung api


                string url = $"http://vuabanhmi.com:1986/api/User/LogIn?Username={sdt}&encodedPwd={pwd}";
                if (tools.isConn())
                {
                    try
                    {
                        using (var cl = new HttpClient())
                        {
                            var res = await cl.GetAsync(url);
                            var res2 = await res.Content.ReadAsStringAsync();
                            var job = JObject.Parse(res2);
                            var success = bool.Parse(tools.GetJArrayValue(job, "Success"));
                            if (success)
                            {
                                var str = tools.GetJArrayValue(job, "Data");
                                var staffinfo = JsonConvert.DeserializeObject<staff>(str);
                                localdb.NhanVieninfo = staffinfo;
                                localdb.isMLMode = vmlogin.isMl;
                                var outlinePage = new outline_page();
                                await Navigation.PushAsync(outlinePage);
                                outlinePage.start_app();
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("", "Sai sdt hoặc mật khẩu", "Ok");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("", ex.ToString(), "Ok");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("", "Mất kết nối", "Ok");
                }
            }

            await btnlogin.ScaleTo(1, 100);
            this.IsEnabled = true;
        }
    }



}