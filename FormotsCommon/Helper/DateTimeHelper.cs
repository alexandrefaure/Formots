using System;
using System.Globalization;

namespace FormotsCommon.Helper
{
    public class DateTimeHelper
    {
        public static string GetDayNameFromDateTime(DateTime? dateTime)
        {
            if (dateTime != null)
            {
                var dayOfWeek = ((DateTime) dateTime).DayOfWeek;
                return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dayOfWeek);
            }
            return null;
        }
    }
}
