using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

using VBMTablet._objs._storeObjs;
using VBMTablet._process;
using Xamarin.Forms;

namespace VBMTablet._vms._login
{
    public class vmlogin : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmlogin()
        {
            RenderStore();
        }

        #region bien
        string sdt_;
        string pwd_;
        bool isMl_=true;
        Color swichBg_=Color.FromHex("#7EA39C");
        LayoutOptions swichLayout_=LayoutOptions.End;

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
        public string pwd
        {
            get
            {
                return pwd_;
            }
            set
            {
                pwd_ = value;
                OnPropertyChanged("pwd");
            }
        }
        public bool isMl
        {
            get
            {
                return isMl_;
            }
            set
            {
                isMl_ = value;
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

        public ObservableCollection<StoreStatus> storeStatuses { get; set; }
        #endregion

        #region progess
        void RenderStore()
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
