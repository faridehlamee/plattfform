using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Enums
{
    public static class EnumHelpers
    {
        public static string ToDescription(this Enum value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                var d = value.GetType().GetField(value.ToString());
                if (d == null) return "";

                var attributes = (DescriptionAttribute[])d.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return attributes.Length > 0 ? attributes[0].Description : value.ToString();
            }

        }
        public static string GetPrsianDate(this DateTime Date)
        {
            //1400 مرداد شنبه
            //var calendar = new PersianCalendar();
            //var result = Date.ToString("yyyy MMM ddd", CultureInfo.GetCultureInfo("fa-Ir"));

            PersianCalendar jc = new PersianCalendar();
            return string.Format("{0:0000}/{1:00}/{2:00}", jc.GetYear(Date), jc.GetMonth(Date), jc.GetDayOfMonth(Date));


        }
    }
}
