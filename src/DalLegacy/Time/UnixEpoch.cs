using System;

namespace DalLegacy.Time
{
    public static class UnixEpoch
    {
        static readonly DateTime s_unixStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long FromDateTime(DateTime in_dateTime) => (long)(in_dateTime.ToUniversalTime() - s_unixStart).TotalSeconds;

        public static DateTime ToDateTime(long in_epoch) => s_unixStart.AddSeconds(in_epoch).ToLocalTime();
    };
}
