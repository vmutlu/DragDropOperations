using System;

namespace DragDrop.API
{
    public static class DateTimeExtensions
    {
        public static string FullDateAndTimeStringWithUnderscore(this DateTime dateTime) => $"{dateTime.Millisecond}_{dateTime.Second}_{dateTime.Minute}_{dateTime.Hour}_{dateTime.Date.ToShortDateString()}_{dateTime.Month}_{dateTime.Year}";
    }
}
