using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._promoObjs
{
    public class promoValidStatus
    {
        public bool IsTimeInUser { get; set; }
        public bool IsShopInUse { get; set; }
        public bool isValid { get; set; }
        public Dictionary<string, string> msgs { get; set; }
        public string msgVN
        {
            get
            {
                return tools.getVNName(msgs);
            }
        }
    }
}
