using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace VBMTablet._vms._cashPage
{
    public class addNewUserPageVM
    {
        public addNewUserPageVM()
        {
            renderUserAge();
            renderUserGioiTinh();
        }
        #region bien
        public ObservableCollection<UserAgeStatus> userAgeStatuses { get; set; }
        public ObservableCollection<UserGioiTinhStatus> userGioiTinhStatuses { get; set; }
        #endregion

        #region progress
        void renderUserAge()
        {
            userAgeStatuses = new ObservableCollection<UserAgeStatus>();
            for(int i = 0; i <= 3; i++)
            {
                userAgeStatuses.Add(new UserAgeStatus(i));
            }
        }
        void renderUserGioiTinh()
        {
            userGioiTinhStatuses = new ObservableCollection<UserGioiTinhStatus>();
            for(int i = 0; i <= 2; i++)
            {
                userGioiTinhStatuses.Add(new UserGioiTinhStatus(i));
            }
        }
        #endregion
    }

    public class UserAgeStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public UserAgeStatus(int id)
        {
            this.id = id;
            if(id == 0)
            {
                this.age = "Dưới 18 tuổi";
            }
            if(id == 1)
            {
                this.age = "Từ 18 tuổi đến 22 tuổi";
            }
            if(id == 2)
            {
                this.age = "Từ 22 tuổi đến 25 tuổi";
            }
            if(id == 3)
            {
                this.age = "Trên 30 tuổi";
            }
        }
        bool selected_;
        bool visCheck_;
        public bool visCheck
        {
            get
            {
                return visCheck_;
            }
            set
            {
                visCheck_ = value;
                OnPropertyChanged("visCheck");
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
                if (value)
                {
                    visCheck = true;
                }
                else
                {
                    visCheck = false;
                }
                OnPropertyChanged("selected");
            }
        }
        public int id { get; set; }
        public string age { get; set; }
       
    }
    public class UserGioiTinhStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public UserGioiTinhStatus(int id)
        {
            this.id = id;
            if(id == 0)
            {
                this.gioiTinh = "Nam";
            }
            if(id == 1)
            {
                this.gioiTinh = "Nữ";
            }
            if(id == 2)
            {
                this.gioiTinh = "Khác";
            }
        }
        bool selected_;
        bool visCheck_;
        public bool visCheck
        {
            get
            {
                return visCheck_;
            }
            set
            {
                visCheck_ = value;
                OnPropertyChanged("visCheck");
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
                if(value)
                {
                    visCheck = true;
                }
                else
                {
                    visCheck = false;
                }
                OnPropertyChanged("selected");
            }
        }
        public int id { get; set; }
        public string gioiTinh { get; set; }
    }
}
