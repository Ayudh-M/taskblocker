using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Scheduling;

public static class FreeWindowBuilder
{
  public static List<(DateTimeOffset Start, DateTimeOffset End)> BuildForDay(
      IEnumerable<CalendarEvent> events,
      DateOnly date,
      IEnumerable<(TimeSpan Start, TimeSpan End)> workWindows,
      TimeZoneInfo tz)
  {
    var dayEvents = events.Where(e => e.Start.Date == date.ToDateTime(TimeOnly.MinValue).Date);
    if (dayEvents.Any(e => e.IsAllDay && e.IsBusy))
      return new();

    var result = new List<(DateTimeOffset Start, DateTimeOffset End)>();
    var dayStart = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);
    foreach (var w in workWindows)
    {
      var start = new DateTimeOffset(dayStart + w.Start, tz.GetUtcOffset(dayStart + w.Start));
      var end = new DateTimeOffset(dayStart + w.End, tz.GetUtcOffset(dayStart + w.End));
      result.Add((start, end));
    }
    foreach (var ev in dayEvents.Where(e => e.IsBusy))
    {
      result = Subtract(result, ev.Start, ev.End);
    }
    return result;
  }

  private static List<(DateTimeOffset Start, DateTimeOffset End)> Subtract(List<(DateTimeOffset Start, DateTimeOffset End)> intervals, DateTimeOffset start, DateTimeOffset end)
  {
    var res = new List<(DateTimeOffset Start, DateTimeOffset End)>();
    foreach (var (s, e) in intervals)
    {
      if (end <= s || start >= e)
      {
        res.Add((s, e));
        continue;
      }
      if (start > s)
        res.Add((s, start));
      if (end < e)
        res.Add((end, e));
    }
    return res;
  }
}
