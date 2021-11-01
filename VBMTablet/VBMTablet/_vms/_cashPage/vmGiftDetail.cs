using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VBMTablet._objs._menuObjs;
using VBMTablet._objs._userObjs;
using VBMTablet._vms._home;

namespace VBMTablet._vms._cashPage
{
    public class vmGiftDetail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmGiftDetail(CustomerGiftStatus customerGiftStatus)
        {            
            this.CustomerGiftStatus = customerGiftStatus;
            var gift = customerGiftStatus.GiftObjs;
            giftDetailItems = new List<giftDetailItem>();
            var groupITems = gift.lst_EmeIDs.GroupBy(p => p.size_refer).Select(p => p.ToList()).ToList();
            foreach (var t1 in groupITems)
            {
                var emes = gift_item.findoutGiftEme(t1[0].ID);
                if (emes != null)
                {
                    giftDetailItems.Add(new giftDetailItem(t1, emes));
                }
            }
            isbusy = false;
        }
        #region bien
        bool isbusy_ = true;
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
        public CustomerGiftStatus CustomerGiftStatus { get; set; }
        public string titlePage
        {
            get
            {
                return CustomerGiftStatus.name;
            }
        }
        public List<giftDetailItem> giftDetailItems { get; set; }
        #endregion
    }
    public class giftDetailItem
    {
        public giftDetailItem(List<gift_item> gift_Items,eMenu eMenu)
        {
            this.gift_Items = gift_Items;
            this.eMenu = eMenu;
            this.img = $"http://manage.vuabanhmi.com/SpImg/{eMenu.img}";
            name = eMenu.nameVN;
        }
        public List<gift_item> gift_Items { get; set; }
        public eMenu eMenu { get; set; }
        public string img { get; set; }
        public string name { get; set; }
    }
}
