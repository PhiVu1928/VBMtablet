using System;
using System.Collections.Generic;
using System.Text;
using VBMTablet._objs._menuObjs;
using VBMTablet._process;

namespace VBMTablet._objs._cartObjs
{
    public class CartProd
    {
        public int slg { get; set; }
        public long id { get; set; }
        public int orderType { get; set; } //0: dat hang ko giam gia, >0: km, <0: loai qua
        public List<cartSpice> LstSpls { get; set; }
        public List<cartExtra> LstExts { get; set; }
        public double nguyengia { get; set; }
        public double dongia { get; set; }
        public bool isGetLater { get; set; }
        public bool isGetLaterOption { get; set; }
        public string orderCode { get; set; }
        public long groupID { get; set; }

        public CartProd clone()
        {
            var res = new CartProd
            {
                nguyengia = nguyengia,
                dongia = dongia,
                groupID = groupID,
                id = id,
                isGetLater = isGetLater,
                isGetLaterOption = isGetLaterOption,
                LstExts = new List<cartExtra>(),
                LstSpls = new List<cartSpice>(),
                orderCode = orderCode,
                orderType = orderType,
                slg = slg
            };
            LstExts.ForEach(p => res.LstExts.Add(p.clone()));
            LstSpls.ForEach(p => res.LstSpls.Add(p.clone()));
            return res;
        }

        public (double, double) callPrice()
        {
            double ng = nguyengia * slg;
            double dg = dongia * slg;
            LstExts.ForEach(p =>
            {
                ng += p.nguyengia * p.solg * slg;
                dg += p.dongia * p.solg * slg;
            });
            return (ng, dg);
        }
        public static eMenu getDetailInfo(long id)
        {
            foreach (var t1 in localdb.groupMenus)
            {
                foreach (var t2 in t1.lst_sub_menu)
                {
                    foreach (var t3 in t2.lst_emes)
                    {
                        foreach (var t4 in t3.lst_combo)
                        {
                            if (t4.id == id)
                            {
                                return t3;
                            }
                        }
                        foreach (var t4 in t3.lst_size)
                        {
                            if (t4.id == id)
                            {
                                return t3;
                            }
                        }
                    }
                }
            }
            return null;
        }
        public static CartProd createAddHisProd(long mainId)
        {
            CartProd data = null;
            foreach(var t1 in localdb.groupMenus)
            {
                foreach(var t2 in t1.lst_sub_menu)
                {
                    foreach(var t3 in t2.lst_emes)
                    {
                        foreach(var t4 in t3.lst_size)
                        {
                            foreach(var t5 in t3.lst_combo)
                            {
                                if (t4.id == mainId)
                                {
                                    data = new CartProd
                                    {
                                        dongia = t4.price,
                                        groupID = t1.id,
                                        id = t4.id,
                                        isGetLater = false,
                                        isGetLaterOption = false,
                                        LstExts = new List<cartExtra>(),
                                        LstSpls = new List<cartSpice>(),
                                        nguyengia = t4.price,
                                        orderCode = "",
                                        orderType = 0,
                                    };
                                }
                                else if (t5.id == mainId)
                                {
                                    data = new CartProd
                                    {
                                        groupID = 0,
                                        dongia = t5.price,
                                        id = t5.id,
                                        isGetLater = false,
                                        isGetLaterOption = false,
                                        LstExts = new List<cartExtra>(),
                                        LstSpls = new List<cartSpice>(),
                                        nguyengia = t5.price,
                                        orderCode = "",
                                        orderType = 0,
                                    };
                                    return data;
                                }
                            }                                                     
                        }
                    }
                }
            }
            return data;
        }
        public static CartProd createAddProd(long mainId, long drinkID)
        {
            CartProd data = null;
            foreach (var t1 in localdb.groupMenus)
            {
                foreach (var t2 in t1.lst_sub_menu)
                {
                    foreach (var t3 in t2.lst_emes)
                    {
                        if (drinkID == 0)
                        {
                            foreach (var t4 in t3.lst_size)
                            {
                                if (t4.id == mainId)
                                {
                                    data = new CartProd
                                    {
                                        dongia = t4.price,
                                        groupID = t1.id,
                                        id = t4.id,
                                        isGetLater = false,
                                        isGetLaterOption = false,
                                        LstExts = new List<cartExtra>(),
                                        LstSpls = new List<cartSpice>(),
                                        nguyengia = t4.price,
                                        orderCode = "",
                                        orderType = 0,
                                        slg = 1
                                    };
                                }
                            }
                        }
                        else if (drinkID > 0)
                        {
                            string cbid = $"{mainId}#{drinkID}";
                            foreach (var t4 in t3.lst_combo)
                            {
                                if (t4.combine_id == cbid)
                                {
                                    data = new CartProd
                                    {
                                        groupID = 0,
                                        dongia = t4.price,
                                        id = t4.id,
                                        isGetLater = false,
                                        isGetLaterOption = false,
                                        LstExts = new List<cartExtra>(),
                                        LstSpls = new List<cartSpice>(),
                                        nguyengia = t4.price,
                                        orderCode = "",
                                        orderType = 0,
                                        slg = 1
                                    };
                                    return data;
                                }
                            }
                        }
                        //case bat ky, drinkid <0
                        else
                        {
                            foreach (var t4 in t3.lst_size)
                            {
                                if (t4.id == mainId)
                                {
                                    data = new CartProd
                                    {
                                        dongia = t4.price,
                                        groupID = t1.id,
                                        id = t4.id,
                                        isGetLater = false,
                                        isGetLaterOption = false,
                                        LstExts = new List<cartExtra>(),
                                        LstSpls = new List<cartSpice>(),
                                        nguyengia = t4.price,
                                        orderCode = "",
                                        orderType = 0,
                                        slg = 1
                                    };
                                }
                            }
                            foreach (var t4 in t3.lst_combo)
                            {
                                if (t4.id == mainId)
                                {
                                    data = new CartProd
                                    {
                                        groupID = 0,
                                        dongia = t4.price,
                                        id = t4.id,
                                        isGetLater = false,
                                        isGetLaterOption = false,
                                        LstExts = new List<cartExtra>(),
                                        LstSpls = new List<cartSpice>(),
                                        nguyengia = t4.price,
                                        orderCode = "",
                                        orderType = 0,
                                        slg = 1
                                    };
                                    return data;
                                }
                            }
                        }
                    }
                }
            }
            return data;
        }

    }

}
