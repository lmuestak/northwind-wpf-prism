using System;

namespace Northwind
{
    public static class DateExtensions
    {
        public static DateTime ToUtc(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
                return dateTime;
            return dateTime.ToUniversalTime();
        }
        public static DateTime? ToUtc(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return null;
            if (dateTime.Value.Kind == DateTimeKind.Utc)
                return dateTime.Value;
            return dateTime.Value.ToUniversalTime();
        }
        public static DateTime ToLocal(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Local)
                return dateTime;
            return dateTime.ToLocalTime();
        }
        public static DateTime? ToLocal(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return null;
            if (dateTime.Value.Kind == DateTimeKind.Local)
                return dateTime.Value;
            return dateTime.Value.ToLocalTime();
        }

        public static bool IsValid(this DateTime dateTime)
        {
            return dateTime != DateTime.MinValue && dateTime != DateTime.MaxValue;
        }

        public static bool IsValid(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return false;
            return IsValid(dateTime.Value);
        }

    }
}
