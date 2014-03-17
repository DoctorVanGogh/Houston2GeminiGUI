using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Houston2DaiGUI
{
    public static class Extensions
    {
        public static int ToInt32(this string val)
        {
            Int32 ret;
            Int32.TryParse(val, out ret);
            return ret;
        }

        public static bool Between(this int val, int min, int max)
        {
            return val >= min && val <= max;
        }

        public static bool IsDigitsOnly(this string val)
        {
            return val.Length > 0 && val.All(c => (c >= '0' && c <= '9') || c == '-' || c == '.');
        }

        public static bool IsHoustonAnchorAttribute(this XName val)
        {
            return val.ToString().EndsWith("AnchorOffset") || val.ToString().EndsWith("AnchorPoint");
        }
    }
}
