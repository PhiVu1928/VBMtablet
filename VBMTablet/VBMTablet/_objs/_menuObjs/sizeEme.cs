using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._menuObjs
{

    public class sizeMenu
    {
        public long id { get; set; }
        public int size { get; set; }
        public string sizeName_vn
        {
            get
            {
                return tools.getVNName(sizeNames);
            }
        }
        public Dictionary<string, string> sizeNames { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(names);
            }
        }
        public Dictionary<string, string> names { get; set; }
        public double price { get; set; }

        public sizeMenu clone()
        {
            return new sizeMenu
            {
                id = id,
                size = size,
                names = new Dictionary<string, string>(names),
                price = price,
                sizeNames = new Dictionary<string, string>(sizeNames)
            };
        }
    }

}
