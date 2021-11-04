using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VBMTablet._objs._cartObjs;
using VBMTablet._objs._menuObjs;
using VBMTablet._process;
using VBMTablet._utils;
using Xamarin.Forms;

namespace VBMTablet._vms._cart
{
    public class vmcart : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public vmcart()
        {
            RenderCartItem();
            RenderOrderOption();
            CallMoney();
        }
        #region bien
        string _tongtien;
        string _thangtien;
        string _giamgia;
        ObservableCollection<cartItem> _cartItems;

        public List<OrderOption> orderOptions { get; set; }
        public string tongtien
        {
            get
            {
                return _tongtien;
            }
            set
            {
                _tongtien = value;
                OnPropertyChanged("tongtien");
            }
        }
        public string thanhtien
        {
            get
            {
                return _thangtien;
            }
            set
            {
                _thangtien = value;
                OnPropertyChanged("thanhtien");
            }
        }
        public string giamgia
        {
            get
            {
                return _giamgia;
            }
            set
            {
                _giamgia = value;
                OnPropertyChanged("giamgia");
            }
        }
        public ObservableCollection<cartItem> cartItems
        {
            get
            {
                return _cartItems;
            }
            set
            {
                _cartItems = value;
                OnPropertyChanged("cartItems");
            }
        }
        #endregion

        #region process
        public void RenderCartItem()
        {
            cartItems = new ObservableCollection<cartItem>();
            foreach(var item in localdb.CartProd)
            {
                cartItems.Add(new cartItem(item));
            }
        }
        void RenderOrderOption()
        {
            orderOptions = new List<OrderOption>();
            for(int i = 0; i < 3; i++)
            {
                orderOptions.Add(new OrderOption(i));
            }
        }
        public (double,double) CallMoney()
        {
            double togtien = 0;
            double thttien = 0;
            foreach(var item in localdb.CartProd)
            {
                var cal = item.callPrice();
                togtien += cal.Item1;
                thttien += cal.Item2;
            }
            tongtien = togtien.ToString("#,##0") + " đ";
            thanhtien = thttien.ToString("#,##0") + " đ";
            giamgia = (togtien - thttien).ToString("#,##0") + " đ";
            return (togtien, thttien);
        }
        #endregion
    }
    public class cartItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public cartItem(CartProd prod)
        {
            this.prod = prod;
            solg = prod.slg;
            monchinh_extrasRender = new List<cartExtraVM>();
            monchinh_giavi = "";
            drink_giavi = "";
            eMenu prodInfo = CartProd.getDetailInfo(prod.id);
            if (prodInfo != null)
            {
                if (prod.groupID == 0)
                {
                    foreach (var cb in prodInfo.lst_combo)
                    {
                        if (cb.id == prod.id)
                        {
                            long mcId = long.Parse(cb.combine_id.Split('#')[0]);
                            long ncId = long.Parse(cb.combine_id.Split('#')[1]);
                            var monchinhFull = CartProd.getDetailInfo(mcId);
                            sizeMenu monchinhDetail = monchinhFull != null ? monchinhFull.lst_size.Where(p => p.id == mcId).FirstOrDefault() : null;
                            var ncFull = CartProd.getDetailInfo(ncId);
                            sizeMenu ncDetail = ncFull != null ? ncFull.lst_size.Where(p => p.id == ncId).FirstOrDefault() : null;
                            if (monchinhFull != null && ncFull != null && monchinhDetail != null && ncDetail != null)
                            {
                                monchinh_name = monchinhDetail.nameVN;
                                monchinh_visnguyengia = false;
                                monchinh_dongia = monchinhDetail.price.ToString("#,##0") + " đ";
                                foreach (var item in prod.LstExts)
                                {
                                    if(item.solg > 0)
                                    {
                                        visExtra = true;
                                        monchinh_extrasRender.Add(new cartExtraVM(item));
                                    }
                                }
                                var spices = localdb.extra_Spices.spices.Where(p => p.mons.Contains(monchinhDetail.id)).ToList();
                                foreach (var t1 in prod.LstSpls)
                                {
                                    foreach (var t2 in spices)
                                    {
                                        var spiSize = t2.lst_items.Where(p => p.id == t1.id).FirstOrDefault();
                                        if (spiSize != null)
                                        {
                                            monchinh_giavi += t1.nameVN + "  ";
                                            break;
                                        }
                                    }
                                }
                                if (monchinh_giavi.Length > 0) monchinh_giavi = monchinh_giavi.Substring(0, monchinh_giavi.Length - 2);

                                visDrinkCb = true;
                                drinkname = ncDetail.nameVN;
                                var spicesNc = localdb.extra_Spices.spices.Where(p => p.mons.Contains(ncDetail.id)).ToList();
                                foreach (var t1 in prod.LstSpls)
                                {
                                    foreach (var t2 in spicesNc)
                                    {
                                        var spiSize = t2.lst_items.Where(p => p.id == t1.id).FirstOrDefault();
                                        if (spiSize != null)
                                        {
                                            drink_giavi += t1.nameVN + ", ";
                                            break;
                                        }
                                    }
                                }
                                if (drink_giavi.Length > 0) drink_giavi = drink_giavi.Substring(0, drink_giavi.Length - 2);
                                drink_nguyengia = ncDetail.price.ToString("#,##0") + " đ";
                                drink_dongia = (prod.dongia - monchinhDetail.price).ToString("#,##0") + " đ";
                            }
                        }
                    }
                }
                else
                {
                    sizeMenu monchinhDetail = prodInfo != null ? prodInfo.lst_size.Where(p => p.id == prod.id).FirstOrDefault() : null;
                    if (monchinhDetail != null)
                    {
                        
                        monchinh_name = tools.getVNName(monchinhDetail.names);
                        if (prod.orderType != 0)
                        {
                            monchinh_nguyengia = prod.nguyengia.ToString("#,##0") + " đ";
                            monchinh_visnguyengia = true;
                        }
                        monchinh_dongia = prod.dongia.ToString("#,##0") + " đ";
                        foreach (var item in prod.LstExts)
                        {
                            if (item.solg > 0)
                            {
                                visExtra = true;
                                monchinh_extrasRender.Add(new cartExtraVM(item));
                            }
                        }
                        prod.LstSpls.ForEach(p => monchinh_giavi += tools.getVNName(p.name) + " ");
                        monchinh_giavi = monchinh_giavi.Length > 0 ? monchinh_giavi.Substring(0, monchinh_giavi.Length - 2) : "";

                        if (prod.orderType == 0 && prod.groupID == 1)
                        {
                            visRecommendCb = true;
                        }
                    }

                }
            }
            var cal = prod.callPrice();
            double tonggiaitem = cal.Item2;
            tonggia = tonggiaitem.ToString("#,##0") + " đ";

        }

        int solg_;

        public CartProd prod { get; set; }
        public eMenu prodInfo { get; set; }
        public int solg
        {
            get
            {
                return solg_;
            }
            set
            {
                solg_ = value;
                OnPropertyChanged("solg");
            }
        }
        
        public string orderCode { get; set; }
        public string tonggia { get; set; }
        public string monchinh_name { get; set; }
        public string monchinh_nguyengia { get; set; }
        public bool monchinh_visnguyengia { get; set; }
        public string monchinh_dongia { get; set; }
        public List<cartExtraVM> monchinh_extrasRender { get; set; }
        public string monchinh_giavi { get; set; }
        public bool visExtra { get; set; }

        public bool visDrinkCb { get; set; }
        public string drinkImg { get; set; }
        public string drinkname { get; set; }
        public string drink_giavi { get; set; }
        public string drink_nguyengia { get; set; }
        public string drink_dongia { get; set; }

        public bool visRecommendCb { get; set; }


    }
    public class cartExtraVM
    {
        public cartExtraVM(cartExtra extra)
        {
            solg = extra.solg;
            name = tools.getVNName(extra.name) + " x" + solg;
        }

        public int solg { get; set; }
        public string name { get; set; }

    }
    public class OrderOption : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public OrderOption(int id)
        {
            this.id = id;
            if(id == 0)
            {
                this.name = "ĐẶT TẠI CHỖ";
            }
            if(id == 1)
            {
                this.name = "ĐẶT ĐẾN LẤY";
            }
            if(id == 2)
            {
                this.name = "GIAO TẬN NƠI";
            }

        }
        
        bool _Selected;
        Color _Textcolor = (Color)Application.Current.Resources["vbmdeepmiddlegray"];
        Color _Bordercolor = (Color)Application.Current.Resources["vbmlightgray"];
        public int id { get; set; }
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
                    TextColor = (Color)Application.Current.Resources["vbmdeepgreen"];
                    BorderColor = (Color)Application.Current.Resources["vbmlightyellow"];
                }
                else
                {
                    TextColor = (Color)Application.Current.Resources["vbmdeepmiddlegray"];
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
    
}
