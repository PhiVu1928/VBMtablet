using System;
using System.Collections.Generic;
using System.Text;

namespace VBMTablet._objs._userObjs
{
	public class gift_item
	{
		public long ID { get; set; }
		public int Size { get; set; }
		public string Img { get; set; }
		public double nguyengia { get; set; }
		public string name_vn { get; set; }
		public string name_en { get; set; }
		public long size_refer { get; set; }
		public double dongia { get; set; }
		public gift_item clone()
		{
			var rt = new gift_item
			{
				ID = ID,
				Size = Size,
				Img = Img,
				nguyengia = nguyengia,
				name_vn = name_vn,
				name_en = name_en,
				size_refer = size_refer,
				dongia = dongia,
			};
			return rt;
		}
	}

}
