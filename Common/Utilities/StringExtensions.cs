using System;
using System.Globalization;

namespace Common.Utilities
{
    public static class StringExtensions
    {
        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }

        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }

        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value);
        }

        public static string ToNumeric(this int value)
        {
            return value.ToString("N0"); //"123,456"
        }

        public static string ToNumeric(this decimal value)
        {
            return value.ToString("N0");
        }

        public static string ToCurrency(this int value)
        {
            //fa-IR => current culture currency symbol => ریال
            //123456 => "123,123ریال"
            return value.ToString("C0");
        }

        public static string ToCurrency(this decimal value)
        {
            return value.ToString("C0");
        }

        public static string En2Fa(this string str)
        {
            return str.Replace("0", "۰")
                .Replace("1", "۱")
                .Replace("2", "۲")
                .Replace("3", "۳")
                .Replace("4", "۴")
                .Replace("5", "۵")
                .Replace("6", "۶")
                .Replace("7", "۷")
                .Replace("8", "۸")
                .Replace("9", "۹");
        }

        public static string Fa2En(this string str)
        {
            return str.Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                //iphone numeric
                .Replace("٠", "0")
                .Replace("١", "1")
                .Replace("٢", "2")
                .Replace("٣", "3")
                .Replace("٤", "4")
                .Replace("٥", "5")
                .Replace("٦", "6")
                .Replace("٧", "7")
                .Replace("٨", "8")
                .Replace("٩", "9");
        }

        public static string FixPersianChars(this string str)
        {
            return str.Replace("ﮎ", "ک")
                .Replace("ﮏ", "ک")
                .Replace("ﮐ", "ک")
                .Replace("ﮑ", "ک")
                .Replace("ك", "ک")
                .Replace("ي", "ی")
                .Replace(" ", " ")
                .Replace("‌", " ")
                .Replace("ھ", "ه");//.Replace("ئ", "ی");
        }

        public static string CleanString(this string str)
        {
            return str.Trim().FixPersianChars().Fa2En().NullIfEmpty();
        }

        public static string NullIfEmpty(this string str)
        {
            return str?.Length == 0 ? null : str;
        }
        public static string GetPrsianDate(this DateTime Date)
        {
            //1400 مرداد شنبه
            //var calendar = new PersianCalendar();
            //var result = Date.ToString("yyyy MMM ddd", CultureInfo.GetCultureInfo("fa-Ir"));

            PersianCalendar jc = new PersianCalendar();
            return string.Format("{0:0000}/{1:00}/{2:00}", jc.GetYear(Date), jc.GetMonth(Date), jc.GetDayOfMonth(Date));

        }
        public static string GetDayOfWeekName(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday: return "شنبه";
                case DayOfWeek.Sunday: return "يکشنبه";
                case DayOfWeek.Monday: return "دوشنبه";
                case DayOfWeek.Tuesday: return "سه‏ شنبه";
                case DayOfWeek.Wednesday: return "چهارشنبه";
                case DayOfWeek.Thursday: return "پنجشنبه";
                case DayOfWeek.Friday: return "جمعه";
                default: return "";
            }
        }
        public static DateTime GetGregorianDate(this string persianDate)
        {
            CultureInfo persianCulture = new CultureInfo("fa-IR");
            DateTime persianDateTime = DateTime.ParseExact(persianDate, "yyyy/MM/dd", persianCulture);    // this parses the date as if it were Gregorian
            //persianDateTime = persianDateTime.AddHours(23).AddMinutes(59).AddSeconds(59);
            return persianDateTime;
        }
    }
}
