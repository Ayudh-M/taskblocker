namespace Timeboxer.Domain.Utils;

public static class DurationExtensions
{
  public static TimeSpan Minutes(this int value) => TimeSpan.FromMinutes(value);
}
