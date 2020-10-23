using System;
using System.Globalization;

namespace file_uploader.Extensions
{
    public static class StringExtension
    {
        public static decimal AsDecimalUs(this string value)
        {
           return Convert.ToDecimal(value, new CultureInfo("en-US"));
        }
    }
}