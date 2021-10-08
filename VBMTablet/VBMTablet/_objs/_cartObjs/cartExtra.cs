using System;
using System.Collections.Generic;
using System.Text;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._cartObjs
{
    public class cartExtra
    {
        public long id { get; set; }
        public int solg { get; set; }
        public Dictionary<string, string> name { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(name);
            }
        }
        public double nguyengia { get; set; }
        public double dongia { get; set; }
        public cartExtra clone()
        {
            return new cartExtra { dongia = dongia, id = id, name = new Dictionary<string, string>(name), nguyengia = nguyengia, solg = solg };
        }
    }

}
