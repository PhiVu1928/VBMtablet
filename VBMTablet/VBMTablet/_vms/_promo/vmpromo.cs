using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

using VBMTablet._objs._promoObjs;
using VBMTablet._process;

namespace VBMTablet._vms._promo
{
    public class vmpromo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmpromo()
        {
            RenderPromo();
            isbusy = false;
        }
        #region bien
        bool _isbusy = true;

        public bool isbusy
        {
            get
            {
                return _isbusy;
            }
            set
            {
                _isbusy = value;
                OnPropertyChanged("isbusy");
            }
        }
        public ObservableCollection<PromoStatus> promos { get; set; }
        #endregion

        #region progress
        void RenderPromo()
        {
            var promo = new ObservableCollection<PromoStatus>();
            foreach(var item in localdb.promotionObjs)
            {
                promo.Add(new PromoStatus(item));
            }
            promos = promo;
        }
        #endregion
    }

    public class PromoStatus
    {
        public PromoStatus(promotionObjs promotionObjs)
        {
            this.promotion = promotionObjs;
            this.name = promotionObjs.nameVN;
        }
        public string name { get; set; }
        public promotionObjs promotion { get; set; }
    }
}
