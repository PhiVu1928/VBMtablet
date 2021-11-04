using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using VBMTablet._objs._promoObjs;
using VBMTablet._process;
using Xamarin.Forms;

namespace VBMTablet._vms._cart
{
    public class vmCartPromo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmCartPromo()
        {
            RenderCartPromo();
        }
        #region bien
        ObservableCollection<cartPromoItem> cartPromoItems_;
        public ObservableCollection<cartPromoItem> cartPromoItems
        {
            get
            {
                return cartPromoItems_;
            }
            set
            {
                cartPromoItems_ = value;
                OnPropertyChanged("cartPromoItems");
            }
        }
        #endregion

        #region progress
        void RenderCartPromo()
        {
            cartPromoItems = new ObservableCollection<cartPromoItem>();
            foreach (var item in localdb.promotionObjs)
            {
                cartPromoItems.Add(new cartPromoItem(item));
            }
        }
        #endregion
    }
    public class cartPromoItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public cartPromoItem(promotionObjs promotionObjs)
        {
            this.promotionObjs = promotionObjs;
            this.name = promotionObjs.nameVN;
        }
        bool Selected_;
        Color textColor_ = (Color)Application.Current.Resources["vbmdeepmiddlegray"];
        Color borderColor_ = Color.FromHex("#F4F9F7");

        public bool Selected
        {
            get
            {
                return Selected_;
            }
            set
            {
                Selected_ = value;
                if(value)
                {
                    textColor = (Color)Application.Current.Resources["vbmgreen"];
                    borderColor = (Color)Application.Current.Resources["vbmlightyellow"];
                }
                else
                {
                    textColor = (Color)Application.Current.Resources["vbmdeepmiddlegray"];
                    borderColor = Color.FromHex("#F4F9F7");
                }
            }
        }
        public Color textColor
        {
            get
            {
                return textColor_;
            }
            set
            {
                textColor_ = value;
                OnPropertyChanged("textColor");
            }
        }
        public Color borderColor
        {
            get
            {
                return borderColor_;
            }
            set
            {
                borderColor_ = value;
                OnPropertyChanged("borderColor");
            }
        }
        public promotionObjs promotionObjs { get; set; }
        public string name { get; set; }
    }
}
