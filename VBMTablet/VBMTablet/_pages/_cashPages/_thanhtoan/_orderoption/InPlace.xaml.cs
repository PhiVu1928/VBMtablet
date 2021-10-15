using Acr.UserDialogs;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VBMTablet._objs._cartObjs;
using VBMTablet._process;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VBMTablet._pages._thanhtoan._orderoption
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InPlace : Rg.Plugins.Popup.Pages.PopupPage
    {
        public BillCreateDoAddBill bill { get; set; }
        public List<SfButton> customButton;
		public bool isEnoughMoney = false;

		public InPlace()
        {
            InitializeComponent();
        }
        public async Task Render(BillCreateDoAddBill bill)
        {
            customButton = new List<SfButton>();
            this.bill = bill;
        }
		private void OnEditorTextChanged(object sender, TextChangedEventArgs e)
		{
			string oldText = e.OldTextValue;
			string newText = e.NewTextValue;
		}
		private void OnEditorCompleted(object sender, EventArgs e)
		{
			string text = ((Editor)sender).Text;
		}
		private void CustomButton(object sender, EventArgs e)
		{
			SfButton btn = (sender as SfButton);
			if (!customButton.Contains(btn))
			{
				customButton.Add(btn);
			}
			foreach (var item in customButton)
			{
				if (item.IsChecked && item != btn)
				{
					item.IsChecked = false;
					break;
				}
			}
			if (!btn.IsChecked)
			{
				lblExcessCash.Text = "";
			}
			else
			{
				if (btn == btnFull)
				{
					isEnoughMoney = true;
					lblExcessCash.Text = "Đưa đủ!";
				}
				else if (btn == btn40k)
				{
					if (bill.ThanhTien < 40000)
					{
						double tmp = 40000 - bill.ThanhTien;
						lblExcessCash.Text = "Tiền thối: " + tmp.ToString("#,###") + " đ";
						isEnoughMoney = true;
					}
					else if (bill.ThanhTien > 40000)
					{
						lblExcessCash.Text = "Chưa đủ!";
						isEnoughMoney = false;
					}
					else
					{
						lblExcessCash.Text = "Đưa đủ!";
						isEnoughMoney = true;
					}
				}
				else if (btn == btn50k)
				{
					if (bill.ThanhTien < 50000)
					{
						double tmp = 50000 - bill.ThanhTien;
						lblExcessCash.Text = "Tiền thối: " + tmp.ToString("#,###") + " đ";
						isEnoughMoney = true;
					}
					else if (bill.ThanhTien > 50000)
					{
						lblExcessCash.Text = "Chưa đủ!";
						isEnoughMoney = false;
					}
					else
					{
						lblExcessCash.Text = "Đưa đủ!";
						isEnoughMoney = true;
					}
				}
				else if (btn == btn100k)
				{
					if (bill.ThanhTien < 100000)
					{
						double tmp = 100000 - bill.ThanhTien;
						lblExcessCash.Text = "Tiền thối: " + tmp.ToString("#,###") + " đ";
						isEnoughMoney = true;
					}
					else if (bill.ThanhTien > 100000)
					{
						lblExcessCash.Text = "Chưa đủ!";
						isEnoughMoney = false;
					}
					else
					{
						lblExcessCash.Text = "Đưa đủ!";
						isEnoughMoney = true;
					}
				}
				else if (btn == btn200k)
				{
					if (bill.ThanhTien < 200000)
					{
						double tmp = 200000 - bill.ThanhTien;
						lblExcessCash.Text = "Tiền thối: " + tmp.ToString("#,###") + " đ";
						isEnoughMoney = true;
					}
					else if (bill.ThanhTien > 200000)
					{
						lblExcessCash.Text = "Chưa đủ!";
						isEnoughMoney = false;
					}
					else
					{
						lblExcessCash.Text = "Đưa đủ!";
						isEnoughMoney = true;
					}
				}
				else if (btn == btn500k)
				{
					if (bill.ThanhTien < 500000)
					{
						double tmp = 500000 - bill.ThanhTien;
						lblExcessCash.Text = "Tiền thối: " + tmp.ToString("#,###") + " đ";
						isEnoughMoney = true;
					}
					else if (bill.ThanhTien > 500000)
					{
						lblExcessCash.Text = "Chưa đủ!";
						isEnoughMoney = false;
					}
					else
					{
						lblExcessCash.Text = "Đưa đủ!";
						isEnoughMoney = true;
					}
				}
			}
		}
		async void ff_close_tapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        async void btnSubmit_Clicked(object sender, EventArgs e)
        {
			var ctr = sender as SfButton;
			await ctr.ScaleTo(0.9, 1);
			await this.FadeTo(0.9, 1);
            try
            {
				bool success = false;
				foreach(var item in customButton)
                {
					if(item.IsChecked)
                    {
						success = true;
						break;
                    }
                }
				if(success)
                {
					using (var progess = UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
					{
						if(isEnoughMoney)
                        {
							bill.TableNotes = lblNote.Text;
						    var res = await BillCreateDoAddBill.orderAction(bill);	
							string dt = JsonConvert.SerializeObject(bill);
							if(dt != null)
                            {
								ActionAfterOrder();
							}
						}
					}
				}
			}
            catch { }
        }
		public async Task ActionAfterOrder()
        {
			localdb.CartProd.Clear();
			localdb.home_Page.updateSlCart();
			localdb.thanh_Toan_Page.vmcart.RenderCartItem();
			localdb.thanh_Toan_Page.vmcart.CallMoney();
			Device.BeginInvokeOnMainThread(() =>
			{
				Navigation.PopPopupAsync();
			});
		}
    }
}