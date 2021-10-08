using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VBMTablet._objs;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using VBMTablet._process;
using VBMTablet._utils;
using Syncfusion.XForms.TabView;
using System.Linq;
using VBMTablet._pages._menu;

namespace VBMTablet._vms._menu
{
    public class vmmenu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmmenu()
        {
            RenderMenu();
        }
        #region bien
        public List<GroupMenu> groupMenus { get; set; } = new List<GroupMenu>();
        public TabItemCollection sfTabItems { get; set; }
        public SfTabView SfTabView { get; set; }
        #endregion
        public void RenderMenu()
        {
            bool vis = true;
            foreach(var t1 in localdb.groupMenus.OrderBy(p => p.index).ToList())
            {
                if(t1.lst_sub_menu.Count > 1)
                {
                    foreach(var t2 in t1.lst_sub_menu.OrderBy(p=>p.index).ToList())
                    {
                        groupMenus.Add(new GroupMenu(t2, vis));
                        vis = false;
                    }
                }
                else
                {
                    foreach (var t2 in t1.lst_sub_menu.OrderBy(p => p.index).ToList())
                    {
                        t2.names = t1.names;
                        groupMenus.Add(new GroupMenu(t2, vis));
                    }
                }
            }
            sfTabItems = new TabItemCollection();
            SfTabView = new SfTabView
            {
                BackgroundColor = Color.Transparent,
                EnableSwiping = true,
                TabWidthMode = TabWidthMode.BasedOnText,
                SelectionIndicatorSettings = new SelectionIndicatorSettings
                {
                    Position = SelectionIndicatorPosition.Bottom,
                    Color = (Color)Application.Current.Resources["vbmlightyellow"],
                    StrokeThickness = 3
                }
            };
            foreach(var item in groupMenus)
            {
                var tab = new SfTabItem
                {
                    Title = item.subname,
                    TitleFontColor = item.TextColor,
                    TitleFontSize = 30,
                    Content = new emenu_page(item),
                    SelectionColor = Color.FromHex("#0D432E")
                };
                sfTabItems.Add(tab);
            }
            SfTabView.Items = sfTabItems;
        }
    }
    public class GroupMenu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public GroupMenu(_objs._menuObjs.subMenu subMenu, bool vis)
        {
            this.subMenu = subMenu;
            this.vis = vis;
            subname = subMenu.nameVN;
            if(vis)
            {
                Selected = true;
                RenderEmenu(vis);
            }
        }

        bool Selected_;
        bool vis;
        Color TextColor_ = (Color)Application.Current.Resources["vbmdeeplightgray"];
        Color BoxColor_;
        ObservableCollection<emenuRender> emenu_;
        
        public _objs._menuObjs.subMenu subMenu;
        public string subname { get; set; }
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
        public Color TextColor
        {
            get
            {
                return TextColor_;
            }
            set
            {
                TextColor_ = value;
                OnPropertyChanged("TextColor");
            }
        }
        public Color BoxColor
        {
            get
            {
                return BoxColor_;
            }
            set
            {
                BoxColor_ = value;
                OnPropertyChanged("BoxColor");
            }
        }
        public ObservableCollection<emenuRender> emenu
        {
            get
            {
                return emenu_;
            }
            set
            {
                emenu_ = value;
                OnPropertyChanged("emenu");
            }
        }
        public void RenderEmenu(bool b)
        {
            if(b)
            {
                ObservableCollection<emenuRender> lstemenu = new ObservableCollection<emenuRender>();
                foreach(var item in subMenu.lst_emes)
                {
                    lstemenu.Add(new emenuRender(item));
                }
                emenu = lstemenu;
            }
        }
    }
    public class emenuRender
    {
        public emenuRender(_objs._menuObjs.eMenu eMenu)
        {
            this.emenu = eMenu;
            this.name = emenu.nameVN;
            this.img = "http://manage.vuabanhmi.com/SpImg/" + eMenu.img;
        }
        public string name { get; set; }
        public string img { get; set; }
        public string price { get; set; }
        public _objs._menuObjs.eMenu emenu { get; set; }
    }
}
