using System;

namespace App.DateTimeProvider
{
    public class AppDateTimeProvider : IDateTimeProvider
    {
        public DateTime DateTimeNow { get { return DateTime.Now; } }
    }
}
