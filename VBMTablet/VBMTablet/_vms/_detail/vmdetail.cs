using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._objs._menuObjs;
using VBMTablet._objs._cartObjs;
using VBMTablet._process;
using Xamarin.Forms;

namespace VBMTablet._vms._detail
{
    public class vmdetail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmdetail(eMenu eMenu)
        {
            isbusy = true;
            this.eMenu = eMenu;
            this.name = eMenu.nameVN;
            RenderSize();
            RenderDrinkCombo(monid);
            RenderExtraSpice();
            slg = 1;
            foreach(var item in eMenu.lst_size)
            {
                if(item.size == 2 || item.size < 1)
                {
                    price = item.price.ToString("#,##0") + " đ";
                }
            }
            isbusy = false;
        }
        #region bien
        bool _isbusy;
        bool _visSize;
        int _slg;
        string _price;
        ObservableCollection<SizeRender> _sizeRenders;
        List<DrinkComboRender> _drink_Combos;
        ObservableCollection<SpiceStatus> _spiceStatuses;
        ObservableCollection<ExtraStatus> _extraStatuses;

        public string price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged("price");
            }
        }
        public int slg
        {
            get
            {
                return _slg;
            }
            set
            {
                _slg = value;
                OnPropertyChanged("slg");
            }
        }
        public ObservableCollection<ExtraStatus> extraStatuses
        {
            get
            {
                return _extraStatuses;
            }
            set
            {
                _extraStatuses = value;
                OnPropertyChanged("extraStatuses");
            }
        }
        public ObservableCollection<SpiceStatus> spiceStatuses
        {
            get
            {
                return _spiceStatuses;
            }
            set
            {
                _spiceStatuses = value;
                OnPropertyChanged("spiceStatuses");
            }
        }
        public string name { get; set; }
        public bool visSize
        {
            get
            {
                return _visSize;
            }
            set
            {
                _visSize = value;
                OnPropertyChanged("visSize");
            }
        }
        public long monid { get; set; }

        public List<drink_combo> lstDrinkcb { get; set; }
        public List<DrinkComboRender> drink_Combos
        {
            get
            {
                return _drink_Combos;
            }
            set
            {
                _drink_Combos = value;
                OnPropertyChanged("drink_Combos");
            }
        }
        public ObservableCollection<SizeRender> sizeRenders
        {
            get
            {
                return _sizeRenders;
            }
            set
            {
                _sizeRenders = value;
                OnPropertyChanged("sizeRenders");
            }
        }
        public eMenu eMenu { get; set; }
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
        #endregion
        #region Progess
        void RenderSize()
        {
            var sizerender = new ObservableCollection<SizeRender>();
            foreach(var item in eMenu.lst_size.OrderBy(x => x.price))
            {
                if(item.size == 2 || item.size < 1)
                {
                    sizerender.Add(new SizeRender(item, true));
                    monid = item.id;
                }
                else
                {
                    sizerender.Add(new SizeRender(item, false));
                }
            }
            sizeRenders = sizerender;
            if(sizeRenders.Count > 1)
            {
                visSize = true;
            }
            else
            {
                visSize = false;
            }
        }

        async Task RenderDrinkCombo(long id)
        {
            var drk_Combos = new List<DrinkComboRender>();
            try
            {
                lstDrinkcb = await drink_combo.getLstDrCbs(id);
            }
            catch { }
            if(lstDrinkcb != null)
            {
                foreach(var item in lstDrinkcb)
                {
                    drk_Combos.Add(new DrinkComboRender(item));
                }
            }
            drink_Combos = drk_Combos;
        }

        void RenderExtraSpice()
        {
            var spice = new ObservableCollection<SpiceStatus>();
            try
            {
                foreach (var item in localdb.extra_Spices.spices)
                {
                    var cond = false;
                    eMenu.lst_size.ForEach(p =>
                    {
                        if (item.mons.Contains(monid))
                        {
                            cond = true;
                        }
                    });
                    if (cond)
                    {
                        spice.Add(new SpiceStatus(item));
                    }
                }
                spiceStatuses = spice;
            }
            catch { }
            var extra = new ObservableCollection<ExtraStatus>();
            try
            {
                foreach (var item in localdb.extra_Spices.extras)
                {
                    extra.Add(new ExtraStatus(item));
                }
            }
            catch { }
            extraStatuses = extra;
        }

        public void ChangePrice()
        {
            try
            {
                var currentProduct = eMenu.lst_size.Where(x => x.id == monid).FirstOrDefault();
                double cost = currentProduct.price;
                foreach(var item in drink_Combos)
                {
                    if(item.Selected == true)
                    {
                        cost += item.dongia;
                    }
                }
                foreach(var item in extraStatuses)
                {
                    cost += item.sl * item.Extra.price;
                }
                cost *= slg;
                price = cost.ToString("#,##0") + " đ";
            }
            catch { }
        }

        #endregion
    }
    public class SizeRender : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public SizeRender(sizeMenu sizeMenu,bool selected)
        {
            this.Size = sizeMenu;
            this.Selected = selected;
            if(sizeMenu.size == 1)
            {
                name = "S";
            }
            else if(sizeMenu.size == 2)
            {
                name = "M";
            }
            else if(sizeMenu.size == 3)
            {
                name = "L";
            }
        }
        public sizeMenu Size { get; set; }

        bool _Selected;
        Color _Textcolor;
        Color _Bordercolor;
        public string name { get; set; }

        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
                if (value)
                {
                    TextColor = (Color)Application.Current.Resources["vbmpinttext"];
                    BorderColor = (Color)Application.Current.Resources["vbmlightgray"];
                }
                else
                {
                    TextColor = (Color)Application.Current.Resources["vbmdeepgray"];
                    BorderColor = (Color)Application.Current.Resources["vbmwhite"];
                }
            }
        }
        public Color BorderColor
        {
            get
            {
                return _Bordercolor;
            }
            set
            {
                _Bordercolor = value;
                OnPropertyChanged("BorderColor");
            }
        }
        public Color TextColor
        {
            get
            {
                return _Textcolor;
            }
            set
            {
                _Textcolor = value;
                OnPropertyChanged("TextColor");
            }
        }
    }
    public class DrinkComboRender : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public DrinkComboRender(drink_combo drink_Combo)
        {
            this.Drink_Combo = drink_Combo;
            this.name = drink_Combo.nameVN;
            this.dongia = drink_Combo.dongia;
        }
        double _dongia;
        bool _Selected;
        Color _Textcolor = (Color)Application.Current.Resources["vbmgray"];
        Color _Bordercolor = (Color)Application.Current.Resources["vbmlightgray"];
        public double dongia
        {
            get
            {
                return _dongia;
            }
            set
            {
                _dongia = value;
                OnPropertyChanged("dongia");
            }
        }
        public string name { get; set; }
        public drink_combo Drink_Combo { get; set; }

        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
                if (value)
                {
                    TextColor = (Color)Application.Current.Resources["vbmlightyellow"];
                    BorderColor = (Color)Application.Current.Resources["vbmlightgreen"];
                }
                else
                {
                    TextColor = (Color)Application.Current.Resources["vbmgray"];
                    BorderColor = (Color)Application.Current.Resources["vbmlightgray"];
                }
            }
        }
        public Color BorderColor
        {
            get
            {
                return _Bordercolor;
            }
            set
            {
                _Bordercolor = value;
                OnPropertyChanged("BorderColor");
            }
        }
        public Color TextColor
        {
            get
            {
                return _Textcolor;
            }
            set
            {
                _Textcolor = value;
                OnPropertyChanged("TextColor");
            }
        }
    }
    public class SpiceStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public SpiceStatus(spices spices)
        {
            this.Spices = spices;
            RenderSpiceSize();
        }
        public spices Spices { get; set; }
        public ObservableCollection<SpiceSizeStatus> spiceSizeStatuses { get; set; }
        void RenderSpiceSize()
        {
            spiceSizeStatuses = new ObservableCollection<SpiceSizeStatus>();
            foreach(var item in Spices.lst_items)
            {
                spiceSizeStatuses.Add(new SpiceSizeStatus(item));
            }
        }
        
    }
    public class SpiceSizeStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public SpiceSizeStatus(spiceItem spiceItem)
        {
            this.SpicesItems = spiceItem;
            if(spiceItem.size == 2)
            {
                this.idselected = spiceItem.size;
                this.name = spiceItem.nameVN;
                this.Selected = true;
            }
            else
            {
                this.idselected = spiceItem.size;
                this.name = spiceItem.nameVN;
            }
        }
        bool _Selected;
        Color _Textcolor = (Color)Application.Current.Resources["vbmdeepmiddlegray"];
        Color _Bordercolor = (Color)Application.Current.Resources["vbmlightgray"];
        public long idselected { get; set; }
        public spiceItem SpicesItems { get; set; }
        public string name { get; set; }
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
                if (value)
                {
                    TextColor = (Color)Application.Current.Resources["vbmwhite"];
                    BorderColor = (Color)Application.Current.Resources["vbmgray"];
                }
                else
                {
                    TextColor = (Color)Application.Current.Resources["vbmdeepmiddlegray"];
                    BorderColor = (Color)Application.Current.Resources["vbmlightgray"];
                }
            }
        }
        public Color BorderColor
        {
            get
            {
                return _Bordercolor;
            }
            set
            {
                _Bordercolor = value;
                OnPropertyChanged("BorderColor");
            }
        }
        public Color TextColor
        {
            get
            {
                return _Textcolor;
            }
            set
            {
                _Textcolor = value;
                OnPropertyChanged("TextColor");
            }
        }
    }
    public class ExtraStatus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ExtraStatus(extra extra, CartProd cartProd = null)
        {
            this.Extra = extra;
            this.name = extra.nameVN;
            this.price = extra.price.ToString("#,##0") + " đ";
            if (cartProd != null)
            {
                var current = cartProd.LstExts.Where(x => x.id == extra.id).FirstOrDefault();
                if(current != null)
                {
                    this.sl = current.solg;
                }
            }
        }
        int _sl;
        public int sl
        {
            get
            {
                return _sl;
            }
            set
            {
                _sl = value;
                OnPropertyChanged("sl");
            }
        }
        public string price { get; set; }
        public string name { get; set; }
        public extra Extra { get; set; }
    }
}
