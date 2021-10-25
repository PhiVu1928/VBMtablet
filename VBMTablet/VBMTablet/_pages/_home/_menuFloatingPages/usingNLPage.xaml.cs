using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Border;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._home._menuFloatingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class usingNLPage : ContentPage
    {
        public usingNLPage()
        {
            InitializeComponent();
        }

        public async Task Render()
        {
            localdb.usingNLPage = this;
        }

        public void renderData()
        {
            if(localdb.nlBarcode != null)
            {
                lblNguyenLieu.Text = localdb.nlBarcode.TenNguyenLieu;
                lblNgayHetHan.Text = localdb.nlBarcode.NgayHetHan.ToString("dd/MM/yyyy");
                lblSlConLai.Text = localdb.nlBarcode.SoLgAvail.ToString("0.00");
            }            
        }

        async void stlOpenQR_tapped(object sender, EventArgs e)
        {
            var ctr = sender as StackLayout;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                var ScanQRPage = new VBMTablet._pages._home._menuFloatingPages.scanBarCodePage();
                await Navigation.PushPopupAsync(ScanQRPage);
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch { }
        }
        private void ff_backicon_tapped(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }

        async void brdSave_tapped(object sender, EventArgs e)
        {
            var ctr = sender as SfBorder;
            await ctr.ScaleTo(0.9, 1);
            await this.FadeTo(0.9, 1);
            try
            {
                if(localdb.nlBarcode != null)
                {
                    double SolgSd = 0;
                    string note = etNote.Text.Trim();
                    SolgSd = double.Parse(etSlSD.Text.Trim());
                    if (SolgSd > localdb.nlBarcode.SoLgAvail || SolgSd == 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Số lượng sử dụng lớn hơn số lượng còn lại, vui lòng kiểm tra lại!", "OK");
                    }
                    else
                    {
                        string data = localdb.nlBarcode.Code + "$" + localdb.nlBarcode.TenNguyenLieu + "$" 
                            + SolgSd.ToString("0,00") + "$" + localdb.NhanVieninfo.Username + "$" + localdb.NhanVieninfo.HoTen
                            + "$" + localdb.nlBarcode.NLID + "$" + localdb.nlBarcode.AutoID + "$" + note;
                        localdb.signalR.HubProxy.Invoke("SystemAction", "doInsertBarCodeHis{}" + data + "{}");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("", "Vui lòng scan lại barcode trước khi lưu!", "OK");
                }
                await ctr.ScaleTo(1, 100);
                await this.FadeTo(1, 100);
            }
            catch { }
        }
        public void actionAfterSave()
        {
            lblBarCode.Text = "";
            lblNgayHetHan.Text = "";
            lblNguyenLieu.Text = "";
            lblSlConLai.Text = "";
            localdb.nlBarcode = null;
        }    
    }
}