using System;
using System.Collections.Generic;
using System.Text;
using VBMTablet._objs._menuObjs;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._userObjs
{
	public class giftObjs
	{
		public int TypeID { get; set; }
		public List<int> use_channel { get; set; }
		public string Code { get; set; }
		public string nameVN { get; set; }
		public string nameEN { get; set; }
		public Dictionary<string, string> names { get; set; }
		public string giftnameVN
		{
			get
			{
				return tools.getVNName(names);
			}
		}
		public List<gift_item> lst_EmeIDs { get; set; }
		public int slg { get; set; }
		public string url_vn { get; set; }
		public string url_en { get; set; }
		public double mintt_deli { get; set; }
		public double mintht_deli { get; set; }
		public string img { get; set; }
		public DateTime expireDate { get; set; }
		public giftObjs clone()
        {
			var rt = new giftObjs
			{
				TypeID = TypeID,
				use_channel = new List<int>(),
				Code = Code,
				nameVN = nameVN,
				nameEN = nameEN,
				names = new Dictionary<string, string>(names),
				lst_EmeIDs = new List<gift_item>(),
				slg = slg,
				url_vn = url_vn,
				url_en = url_en,
				mintht_deli = mintht_deli,
				mintt_deli = mintt_deli,
				img = img,
				expireDate = expireDate
			};
			lst_EmeIDs.ForEach(p => rt.lst_EmeIDs.Add(p));
			return rt;
        }
	}
	public class bmls_obj
	{
		public long SpID { get; set; }

		public int SoLg { get; set; }
		public static eMenu findEme(long id)
		{
			foreach (var t1 in localdb.groupMenus)
			{
				foreach (var t2 in t1.lst_sub_menu)
				{
					foreach (var t3 in t2.lst_emes)
					{
						foreach (var t4 in t3.lst_size)
						{
							if (t4.id == id)
							{
								return t3;
							}
						}
						foreach (var t4 in t3.lst_combo)
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
	}

}
