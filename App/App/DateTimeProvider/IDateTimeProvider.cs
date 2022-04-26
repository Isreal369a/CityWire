using System;

namespace App.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime DateTimeNow { get; }
    }
}
