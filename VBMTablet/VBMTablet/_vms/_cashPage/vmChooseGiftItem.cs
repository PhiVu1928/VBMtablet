using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VBMTablet._objs._menuObjs;
using VBMTablet._objs._userObjs;
using Xamarin.Forms;

namespace VBMTablet._vms._cashPage
{
    public class vmChooseGiftItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmChooseGiftItem(List<gift_item> gift_Items,eMenu eMenu)
        {
            this.gift_Items = gift_Items;
            this.eMenu = eMenu;
            name = eMenu.nameVN;
            visSizeView = gift_Items.Count > 0 ? true : false;
            if(visSizeView)
            {
                chooseGiftItemSizeVMs = new List<chooseGiftItemSizeVM>();
                var gift_Items_order = gift_Items.OrderBy(p => p.Size).ToList();
                int index = 0;
                if (gift_Items_order.Where(p => p.Size == 2).FirstOrDefault() != null)
                {
                    index = 2;
                }
                else
                {
                    index = gift_Items_order[0].Size;
                }
                foreach (var t1 in gift_Items_order)
                {
                    foreach (var t2 in eMenu.lst_size)
                    {
                        if (t1.ID == t2.id)
                        {
                            chooseGiftItemSizeVMs.Add(new chooseGiftItemSizeVM(t1, t2, t2.size == index ? true : false));
                        }
                    }
                }
            }
            
        }
        #region bien
        int solg_ = 1;
        public int solg
        {
            get
            {
                return solg_;
            }
            set
            {
                solg_ = value;
                OnPropertyChanged("solg");
            }
        }
        public List<gift_item> gift_Items { get; set; }
        public eMenu eMenu { get; set; }
        public string name { get; set; }
        public bool visSizeView { get; set; }
        public List<chooseGiftItemSizeVM> chooseGiftItemSizeVMs { get; set; }
        #endregion
    }
    public class chooseGiftItemSizeVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public chooseGiftItemSizeVM(gift_item giftSize, sizeMenu size_menu, bool select)
        {
            this.giftSize = giftSize;
            this.size_menu = size_menu;
            sizeName = size_menu.nameVN;
            selected = select;
        }

        bool selected_;
        Color bg_;

        public sizeMenu size_menu;
        public gift_item giftSize;
        public string sizeName { get; set; }
        public Color bg
        {
            get
            {
                return bg_;
            }
            set
            {
                bg_ = value;
                OnPropertyChanged("bg");
            }
        }
        public bool selected
        {
            get
            {
                return selected_;
            }
            set
            {
                selected_ = value;
                bg = value ? (Color)Application.Current.Resources["vbmyellow"] : (Color)Application.Current.Resources["vbmdeeplightgray"];
            }
        }

    }
}
