using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._objs._cashObjs._billVRObjs;

namespace VBMTablet._vms._karuna
{

    public class vmKarunaPage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void pchange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public vmKarunaPage()
        {

        }

        ObservableCollection<KarunaItemVM> items_;
        public ObservableCollection<KarunaItemVM> items
        {
            get
            {
                return items_;
            }
            set
            {
                items_ = value;
                pchange("items");
            }
        }

        public async Task generateVM()
        {
            var lst = await BillVRObj.getVRBills();
            if (lst != null)
            {
                var its = new ObservableCollection<KarunaItemVM>();
                foreach (var item in lst)
                {
                    its.Add(new KarunaItemVM(item));
                }
                items = its;
            }
        }

    }

    public class KarunaItemVM
    {
        public KarunaItemVM(BillVRObj bill)
        {
            this.bill = bill;
            string msg = $"Giờ giao hàng: {bill.henGioLayFrom.ToString("HH:mm")} - {bill.henGioLayTo.ToString("HH:mm")} ({bill.henGioLayFrom.ToString("dd/MM")})\n\n";
            msg += $"Mã Bill: {bill.MaBill}\n\n";
            msg += $"Địa chỉ: {bill.deliverAddress}\n\n";
            msg += $"Tên khách: {bill.HoTenKhach}\n\n";
            msg += $"SDT khách: {bill.PhoneKhach}\n\n";
            msg += $"Tổng tiền: {bill.TgTien.ToString("#,##0")}\n\n";
            msg += $"Phí ship: {(bill.ShippingFee + bill.ExtraShipFee).ToString("#,##0")}\n\n";
            msg += $"Giảm giá: {bill.giamGia.ToString("#,##0")}\n\n";
            msg += $"Thành tiền: {bill.ThanhTien.ToString("#,##0")}\n\n";
            msg += $"Chi tiết:\n";
            string dt = "";
            foreach (var item in bill.ListBillVRDetails)
            {
                dt += "   " + item.SpName + " x" + item.SoLg + "\n";
            }
            msg += dt;
            detail = msg;
        }

        public BillVRObj bill { get; set; }
        public string detail { get; set; }

    }
}
