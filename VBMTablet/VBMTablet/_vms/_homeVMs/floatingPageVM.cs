using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VBMTablet._vms._homeVMs
{
    public class floatingPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void pchange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public floatingPageVM()
        {

        }
    }
}
