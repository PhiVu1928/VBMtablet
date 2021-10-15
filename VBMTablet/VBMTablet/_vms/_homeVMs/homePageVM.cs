using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VBMTablet._process;
using Xamarin.Forms;

namespace VBMTablet._vms._homeVMs
{
    public class homePageVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void pchange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public homePageVM()
        {
            var store = localdb.storeObjs.Where(p => p.ShopID == localdb.shopID).FirstOrDefault();
            shopName = store.ShopName;
            isMl = localdb.isMLMode;
        }

        string modeName_;
        bool isMl_;
        bool visML_;
        bool visCash_;
        Color swichBg_ = Color.FromHex("#7EA39C");
        LayoutOptions swichLayout_ = LayoutOptions.End;
        makelineBillVM ml_Bill_;
        ObservableCollection<givingCOBillVM> co_Billobs_;

        public string shopName { get; set; }
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
                    modeName = "Makeline";
                    visML = true;
                    visCash = false;
                }
                else
                {
                    swichBg = Color.FromHex("#d2d2d2");
                    swichLayout = LayoutOptions.Start;
                    modeName = "Cash";
                    visML = false;
                    visCash = true;
                }
            }
        }
        public string modeName
        {
            get
            {
                return modeName_;
            }
            set
            {
                modeName_ = value;
                pchange("modeName");
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
                pchange("swichBg");
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
                pchange("swichLayout");
            }
        }
        public string version { get; set; } = "1.0.0";
        public bool visML
        {
            get
            {
                return visML_;
            }
            set
            {
                visML_ = value;
                pchange("visML");
            }
        }
        public bool visCash
        {
            get
            {
                return visCash_;
            }
            set
            {
                visCash_ = value;
                pchange("visCash");
            }
        }

        public makelineBillVM ml_Bill
        {
            get
            {
                return ml_Bill_;
            }
            set
            {
                ml_Bill_ = value;
                pchange("ml_Bill");
            }
        }
        public ObservableCollection<givingCOBillVM> co_BillObs
        {
            get
            {
                return co_Billobs_;
            }
            set
            {
                co_Billobs_ = value;
                pchange("co_Billobs");
            }
        }

    }

    public class makelineBillVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void pchange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public makelineBillVM()
        {

        }

    }

    public class givingCOBillVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void pchange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public givingCOBillVM()
        {

        }

    }

}
