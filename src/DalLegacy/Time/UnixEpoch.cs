using System;

namespace DalLegacy.Time
{
    public static class UnixEpoch
    {
        static DateTime _unixStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long FromDateTime(DateTime in_dateTime)
        {
            return (long)(in_dateTime.ToUniversalTime() - _unixStart).TotalSeconds;
        }
        public static DateTime ToDateTime(long in_epoch)
        {
            return _unixStart.AddSeconds(in_epoch).ToLocalTime();
        }
    };
}
