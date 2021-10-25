using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using VBMTablet._objs._cashObjs._orderedInDayObjs;
using VBMTablet._process;
using Xamarin.Forms;

namespace VBMTablet._vms._billsInDay
{
    public class vmBillsInDay : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmBillsInDay()
        {
            isbusy = true;
            RenderBillInDay();
            isbusy = false;
        }

        #region bien
        bool isbusy_;
        ObservableCollection<BillStatus> billStatuses_;
        public bool isbusy
        {
            get
            {
                return isbusy_;
            }
            set
            {
                isbusy_ = value;
                OnPropertyChanged("isbusy");
            }
        }
        public ObservableCollection<BillStatus> billStatuses
        {
            get
            {
                return billStatuses_;
            }
            set
            {
                billStatuses_ = value;
                OnPropertyChanged("billStatuses");
            }
        }
        #endregion

        #region progress
        async void RenderBillInDay()
        {
            var billInDay = new ObservableCollection<BillStatus>();
            var data = await billDay.getBillsInDay(localdb.shopID);
            if(data != null)
            {
                foreach(var item in data.Take(15))
                {
                    billInDay.Add(new BillStatus(item));
                }
            }
            billStatuses = billInDay;
        }
        #endregion
    }
    public class BillStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public BillStatus(billDay billDay)
        {
            this.BillDay = billDay;
            this.BillID = billDay.BillID;
            var date = billDay.BillDate.ToString("dd/MM/yyyy");
            var time = billDay.BillDate.ToString("HH:mm:ss");
            this.BillDate = time + " " + date;
            this.name = billDay.HoTenKhach;
            this.tgTien = billDay.TgTien.ToString("##,000");
            switch(billDay.BillType)
            {
                case 0:
                    this.BillType = "Đặt tại chỗ";
                    break;
                case 1:
                    this.BillType = "Đặt đến lấy";
                    break;
                case 2:
                    this.BillType = "Giao tận nơi";
                    break;
                case 10:
                    this.BillType = "Khách ngồi lại";
                    break;
            }
            switch(billDay.Status)
            {
                case 0:
                    this.billStatus = "Đơn đang xử lý";
                    this.BillStatusColor = (Color)Application.Current.Resources["vbmpinttext"];
                    break;
                case 1:
                    this.billStatus = "Đơn Làm Bánh Xong";
                    this.BillStatusColor = (Color)Application.Current.Resources["vbmgreen"];
                    break;
                case 2:
                    this.billStatus = "Đơn Đang Giao Tận Nơi";
                    this.BillStatusColor = (Color)Application.Current.Resources["vbmblack"];
                    break;
                case 9:
                    this.billStatus = "Đơn đã xong";
                    this.BillStatusColor = (Color)Application.Current.Resources["vbmgreen"];
                    break;                           
            }
            RenderListBillDetail();
        }
        bool visDetail_ = false;
        public bool visDetail
        {
            get
            {
                return visDetail_;
            }
            set
            {
                visDetail_ = value;
                OnPropertyChanged("visDetail");
            }
        }
        public int BillID { get; set; }
        public string BillDate { get; set; }
        public string name { get; set; }
        public string tgTien { get; set; }
        public string BillType { get; set; }
        public string billStatus { get; set; }
        public Color BillStatusColor { get; set; }
        public ObservableCollection<BillDetail> billDetails { get; set; }
        public billDay BillDay { get; set; }
        void RenderListBillDetail()
        {
            var listbill = new ObservableCollection<BillDetail>();
            foreach(var item in BillDay.ListBillDetails)
            {
                listbill.Add(new BillDetail(item,BillDay.TableNotes));
            }
            billDetails = listbill;
        }
    }
    public class BillDetail
    {
        public BillDetail(ListBillDetail listBillDetail,string note)
        {
            this.ListBillDetail = listBillDetail;
            this.name = listBillDetail.SpName;
            this.solg = "x" + listBillDetail.SoLg;
            this.price = listBillDetail.DonGia.ToString("##,000");
            this.billNote = note;
        }
        public string billNote { get; set; }
        public string name { get; set; }
        public string solg { get; set; }
        public string price { get; set; }
        public ListBillDetail ListBillDetail { get; set; }
    }
}
