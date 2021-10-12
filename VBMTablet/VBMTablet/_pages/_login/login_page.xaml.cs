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
        async void login_Tapped(object sender, EventArgs e)
        {
            await btnlogin.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            string sdt = vmlogin.sdt;
            string pwd = vmlogin.pwd;
            try
            {
                if(string.IsNullOrEmpty(sdt))
                {
                    await Application.Current.MainPage.DisplayAlert("","Bạn chưa nhập số điện thoại bạn nhé !", "OK");
                }
                if(string.IsNullOrEmpty(pwd))
                {
                    await Application.Current.MainPage.DisplayAlert("","Bạn chưa nhập mật khẩu bạn nhé !", "OK");
                }
                sdt = sdt[0].ToString() == "0" ? sdt : 0 + sdt;
                pwd = tools.EncodePassword(pwd);
                string url = $"http://vuabanhmi.com:1986/api/User/LogIn?Username={sdt}&encodedPwd={pwd}";
                if(tools.isConn())
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
                                var outlinePage = new VBMTablet._pages._info.outline_page();
                                await Navigation.PushAsync(outlinePage);
                                outlinePage.start_app();
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("", "Sai sdt hoặc mật khẩu", "Ok");
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("", ex.ToString(), "Ok");
                    }
                }
                await btnlogin.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch(Exception)
            {
                await btnlogin.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            
            
        }

        private void sfpickstore_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ctr = sender as Picker;
            var a =  JsonConvert.SerializeObject(ctr.SelectedItem);
            var res = JsonConvert.DeserializeObject<StoreStatus>(a);
            if(res != null)
            {
                localdb.SelectedStore = res.StoreObj;
            }
        }
    }
}