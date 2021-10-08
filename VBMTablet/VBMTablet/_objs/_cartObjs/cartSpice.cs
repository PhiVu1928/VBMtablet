using System;
using System.Collections.Generic;
using System.Text;
using VBMTablet._process;
using VBMTablet._utils;

namespace VBMTablet._objs._cartObjs
{
    public class cartSpice
    {
        public long id { get; set; }
        public Dictionary<string, string> name { get; set; }
        public string nameVN
        {
            get
            {
                return tools.getVNName(name);
            }
        }
        public cartSpice clone()
        {
            return new cartSpice { id = id, name = new Dictionary<string, string>(name) };
        }
    }
}
