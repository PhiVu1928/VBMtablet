using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;

using VBMTablet._objs._userObjs;
using VBMTablet._process;

namespace VBMTablet._vms._home
{
    public class vmhome : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmhome()
        {

        }
        #region bien
        bool visUserInfo_ = false;
        bool visNonUserInfo_ = true;
        string sdt_;
        int Cartcount_;
        public bool visUserInfo
        {
            get
            {
                return visUserInfo_;
            }
            set
            {
                visUserInfo_ = value;
                OnPropertyChanged("visUserInfo");
            }
        }
        public bool visNonUserInfo
        {
            get
            {
                return visNonUserInfo_;
            }
            set
            {
                visNonUserInfo_ = value;
                OnPropertyChanged("visNonUserInfo");
            }
        }
        public string sdt
        {
            get
            {
                return sdt_;
            }
            set
            {
                sdt_ = value;
                OnPropertyChanged("sdt");
            }
        }
        public int Cartcount
        {
            get
            {
                return Cartcount_;
            }
            set
            {
                Cartcount_ = value;
                OnPropertyChanged("Cartcount");
            }
        }
        #endregion

    }

    public class vmCustomerScript : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void pchange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public vmCustomerScript()
        {

        }

        public vmCustomerScript(userinfo userinfo)
        {
            var script = new ObservableCollection<CustomerScriptStatus>();
            foreach(var item in userinfo.wow_histories)
            {
                script.Add(new CustomerScriptStatus(item));
            }
            customerScriptStatuses = script;
            int count = customerScriptStatuses.Count();
            if(count == 0)
            {
                CountScript = "Khách có trải nghiệm tốt";
                vislydo = false;
            }
            else
            {
                CountScript = "Khách đã có " + count.ToString() + " lần trải nghiệm không tốt";
                vislydo = true;
            }
        }
        #region bien
        bool vislydo_;

        public bool vislydo
        {
            get
            {
                return vislydo_;
            }
            set
            {
                vislydo_ = value;
                pchange("vislydo");
            }
        }

        public string CountScript { get; set; }
        public ObservableCollection<CustomerScriptStatus> customerScriptStatuses { get; set; }
        #endregion
    }
    public class vmCustomerOrdered
    {
        public vmCustomerOrdered()
        {

        }
        public vmCustomerOrdered(List<userOrdered> userOrdereds)
        {
            var order = new ObservableCollection<CustomerOrderedStatus>();
            foreach (var item in userOrdereds)
            {
                order.Add(new CustomerOrderedStatus(item));
            }
            orderedStatuses = order;
        }
        #region bien
        public ObservableCollection<CustomerOrderedStatus> orderedStatuses { get; set; }
        #endregion
    }

    public class vmCustomerGift
    {
        public vmCustomerGift()
        {

        }
        public vmCustomerGift(UserGiftObjs userGiftObjs)
        {
            localdb.fullUserInfo.userGiftObjs = userGiftObjs;
            var gift = new ObservableCollection<CustomerGiftStatus>();
            foreach(var item in userGiftObjs.lst_gifts)
            {
                gift.Add(new CustomerGiftStatus(item));
            }
            foreach(var item in userGiftObjs.lst_bmls)
            {
                gift.Add(new CustomerGiftStatus(item));
            }
            customerGiftStatuses = gift;
        }
        #region bien
        public ObservableCollection<CustomerGiftStatus> customerGiftStatuses { get; set; }
        #endregion
    }

    public class CustomerScriptStatus 
    {
        public CustomerScriptStatus(wow_histories wow_Histories)
        {
            this.wow_Histories = wow_Histories;
            this.lydo = "• " + wow_Histories.LyDo;
            this.shopname = wow_Histories.ShopName;
        }
        public wow_histories wow_Histories { get; set; }
        public string lydo { get; set; }
        public string shopname { get; set; }
    }
    public class CustomerGiftStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public CustomerGiftStatus(giftObjs giftObjs)
        {
            this.name = giftObjs.nameVN;
            this.solg = "Số lượng: " + giftObjs.slg.ToString();
            this.expireDate = "Hạn sử dụng:" + giftObjs.expireDate.ToString("dd/MM/yyyy");
            this.GiftObjs = giftObjs;
        }
        public CustomerGiftStatus(bmls_obj bmls_obj)
        {
            this.bmls_obj = bmls_obj;
            var eme = bmls_obj.findEme(bmls_obj.SpID);
            if(eme != null)
            {
                this.solg = "Số lượng: " + bmls_obj.SoLg.ToString();
                var eme2 = eme.lst_size.Where(p => p.id == bmls_obj.SpID).FirstOrDefault();
                if (eme2 == null)
                {
                    var eme3 = eme.lst_combo.Where(p => p.id == bmls_obj.SpID).FirstOrDefault();
                    if (eme3 != null)
                    {
                        this.name = eme3.nameVN;
                    }
                }
                else
                {
                    this.name = eme2.nameVN;
                }
            }
        }
        public string name { get; set; }
        public string solg { get; set; }
        public string expireDate { get; set; }
        public giftObjs GiftObjs { get; set; }
        public bmls_obj bmls_obj { get; set; }
    }
    public class CustomerStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public CustomerStatus(userinfo userinfo)
        {
            this.Userinfo = userinfo;
            this.name = userinfo.Fullname;
            this.userlevel = userinfo.UserLevel;
            switch (userinfo.UserStatus)
            {
                case 0:
                    this.userStatus = (Color)Application.Current.Resources["vbmdeepgreen"];
                    break;
                case 1:
                    this.userStatus = (Color)Application.Current.Resources["vbmred"];
                    break;
                case 2:
                    this.userStatus = (Color)Application.Current.Resources["vbmlightyellow"];
                    break;
            }
            this.birthDay = userinfo.Birthday.ToString("dd/MM/yyyy");
            this.point = "Điểm:" + userinfo.Point.ToString();
            this.soquatichluy = "Số quà khả dụng:" + userinfo.SoBanhTichLuy.ToString();
            this.banhmitichluy = "Bánh Mì Tích Lũy:" + userinfo.SoBanhTichLuy.ToString();
        }
        Color userStatus_;

        public string name { get; set; }
        public string birthDay { get; set; }
        public string point { get; set; }
        public string banhmitichluy { get; set; }
        public string userlevel { get; set; }
        public string soquatichluy { get; set; }
        
        public Color userStatus
        {
            get
            {
                return userStatus_;
            }
            set
            {
                userStatus_ = value;
                OnPropertyChanged("userStatus");
            }
        }
        public userinfo Userinfo { get; set; }

    }
    public class CustomerOrderedStatus
    {
        public CustomerOrderedStatus(userOrdered userOrdered)
        {
            this.ordered = userOrdered;
            this.orderedDate = userOrdered.BillDate.ToString("dd/MM/yyyy");
            this.listBillDetail = userOrdered.ListBillDetails;
            foreach(var item in userOrdered.ListBillDetails)
            {
                if(item.IsExtra == 0)
                {
                    this.name = item.SpName;
                    this.solg = "x " + item.SoLg.ToString();
                    this.spId = item.SpID;
                }                
            }
        }
        public int spId { get; set; }
        public userOrdered ordered { get; set; }
        public List<ListBillDetail> listBillDetail { get; set; }
        public string orderedDate { get; set; }
        public string name { get; set; }
        public string solg { get; set; }
    }

}
