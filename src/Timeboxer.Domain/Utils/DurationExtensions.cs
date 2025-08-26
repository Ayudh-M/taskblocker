namespace Timeboxer.Domain.Utils;

public static class DurationExtensions
{
  public static TimeSpan Minutes(this int minutes) => TimeSpan.FromMinutes(minutes);
}
