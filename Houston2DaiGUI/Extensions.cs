﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
            Int32.TryParse(val, NumberStyles.Number, CultureInfo.InvariantCulture, out ret);
            return ret;
        }

        public static decimal ToDecimal(this string val)
        {
            decimal ret;
            decimal.TryParse(val, NumberStyles.Number, CultureInfo.InvariantCulture, out ret);
            return ret;
        }

        public static bool Between(this int val, int min, int max)
        {
            return val >= min && val <= max;
        }

        public static bool IsDigitsOnly(this string val)
        {
            if (val == null)
                return false;

            decimal number;
            return decimal.TryParse(val, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out number);
        }

        public static bool IsHoustonAnchorAttribute(this XName val)
        {
            return val.ToString().EndsWith("AnchorOffset") || val.ToString().EndsWith("AnchorPoint");
        }
    }
}
