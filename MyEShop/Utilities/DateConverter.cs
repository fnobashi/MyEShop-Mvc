using System.Globalization;

namespace MyEShop.Utilities
{
    public static class DateConverter
    {
        public static string ToShamsiDate(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date).ToString("0000") + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00");
        }
    }
}
