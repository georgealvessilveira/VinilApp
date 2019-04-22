using NodaTime;

namespace VinilApp.RestApi.Util
{
    public class DateUtil
    {
        public static LocalDate Now()
        {
            Instant instant = SystemClock.Instance.GetCurrentInstant();
            DateTimeZone dateTimeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault();
            return instant.InZone(dateTimeZone).Date;
        }
    }
}