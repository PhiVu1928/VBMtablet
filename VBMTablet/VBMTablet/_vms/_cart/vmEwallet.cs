using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace VBMTablet._vms._cart
{
    public class vmEwallet : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmEwallet()
        {
            renderEwallet();
        }
        #region bien
        ObservableCollection<EwalletItem> ewalletItems_;
        public ObservableCollection<EwalletItem> ewalletItems
        {
            get
            {
                return ewalletItems_;
            }
            set
            {
                ewalletItems_ = value;
                OnPropertyChanged("ewalletItems");
            }
        }
        #endregion

        #region progress
        void renderEwallet()
        {
            var ewallet = new ObservableCollection<EwalletItem>();
            for(int i = 0; i < 6; i++)
            {
                ewallet.Add(new EwalletItem(i));
            }
            ewalletItems = ewallet;
        }
        #endregion
    }
    public class EwalletItem:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public EwalletItem(int id)
        {
            this.id = id;
            if(id == 0)
            {
                this.img = "tienmaticon";
                this.name = "TIỀN MẶT";
                this.Selected = true;
            }
            if(id == 1)
            {
                this.img = "atmicon";
                this.name = "ATM CARD";
            }
            if(id == 2)
            {
                this.img = "visaicon";
                this.name = "VISA / MASTER";
            }
            if(id == 3)
            {
                this.img = "logomomo";
                this.name = "MOMO";
            }
            if(id == 4)
            {
                this.img = "zalopay";
                this.name = "ZALO PAY";
            }
            if(id == 5)
            {
                this.img = "vinidlogo";
                this.name = "VINID";
            }
        }
        bool Selected_;
        public bool Selected
        {
            get
            {
                return Selected_;
            }
            set
            {
                Selected_ = value;
                OnPropertyChanged("Selected");
            }
        }
        public int id { get; set; }
        public string img { get; set; }
        public string name { get; set; }
    }
}
