using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using VBMTablet._objs._storeObjs;
using VBMTablet._process;
using Xamarin.Forms;

namespace VBMTablet._vms._cart
{
    public class vmPickUp : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmPickUp()
        {
            renderStore();
        }
        #region bien
        bool isTakeNow_ = false;
        Color swichBg_ = Color.FromHex("#7EA39C");
        LayoutOptions swichLayout_ = LayoutOptions.End;
        ObservableCollection<StoreStatus> storeStatuses_;
        public ObservableCollection<StoreStatus> storeStatuses
        {
            get
            {
                return storeStatuses_;
            }
            set
            {
                storeStatuses_ = value;
                OnPropertyChanged("storeStatuses");
            }
        }
        public bool isTakeNow
        {
            get
            {
                return isTakeNow_;
            }
            set
            {
                isTakeNow_ = value;
                if (value)
                {
                    swichBg = Color.FromHex("#7EA39C");
                    swichLayout = LayoutOptions.End;
                }
                else
                {
                    swichBg = Color.FromHex("#d2d2d2");
                    swichLayout = LayoutOptions.Start;
                }
            }
        }
        public Color swichBg
        {
            get
            {
                return swichBg_;
            }
            set
            {
                swichBg_ = value;
                OnPropertyChanged("swichBg");
            }
        }
        public LayoutOptions swichLayout
        {
            get
            {
                return swichLayout_;
            }
            set
            {
                swichLayout_ = value;
                OnPropertyChanged("swichLayout");
            }
        }
        #endregion

        #region progress
        void renderStore()
        {
            var store = new ObservableCollection<StoreStatus>();
            foreach(var item in localdb.storeObjs)
            {
                store.Add(new StoreStatus(item));
            }
            storeStatuses = store;
        }
        #endregion
    }
    public class StoreStatus
    {
        public StoreStatus(storeObj storeObj)
        {
            this.StoreObj = storeObj;
            this.name = storeObj.ShopName;
        }
        public string name { get; set; }
        public storeObj StoreObj { get; set; }
    }
}
