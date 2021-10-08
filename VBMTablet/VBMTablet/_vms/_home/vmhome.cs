using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace VBMTablet._vms._home
{
    public class vmhome : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ObservableCollection<MenuTab> menuTabs { get; set; }
        public vmhome()
        {
            CreateMenuTab();
        }
        int Cartcount_;
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
        void CreateMenuTab()
        {
            menuTabs = new ObservableCollection<MenuTab>();
            for(int i = 0; i <= 3; i++ )
            {
                menuTabs.Add(new MenuTab(i));
            }
        }
    }
    public class MenuTab : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public MenuTab(int id)
        {
            this.Index = id;
            if(id == 0)
            {
                Name = "KHÁCH HÀNG";
                Selected = true;
            }
            if(id == 1 )
            {
                Name = "SCRIPT";
            }
            if(id == 2)
            {
                Name = "QUÀ TẶNG";
            }
            if(id == 3)
            {
                Name = "ĐƠN HÀNG";
            }
        }
        Color _TextColor;
        
        bool _Selected;
        public Color TextColor
        {
            get
            {
                return _TextColor;
            }
            set
            {
                _TextColor = value;
                OnPropertyChanged("TextColor");
            }
        }
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
                if(value)
                {
                    TextColor = (Color)Application.Current.Resources["vbmgreen"];
                }
                else
                {
                    TextColor = (Color)Application.Current.Resources["vbmdeeplightgray"];
                }
                OnPropertyChanged("Selected");
            }
        }
        public int Index { get; set; }
        public string Name { get; set; }
    }

}
