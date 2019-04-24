using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace VFKN.Extensions
{
    public static class DateTimeExtensions
    {
        private const string dateTimePattern = "dd.MM.yyyy hh:mm:ss";

        public static DateTime? VfkToDateTime(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            if (DateTime.TryParseExact(value, dateTimePattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                return result;
            return null;
        }
    }
}
