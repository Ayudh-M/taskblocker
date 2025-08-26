using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Scheduling;

public static class FreeWindowBuilder
{
  public static List<(DateTimeOffset Start, DateTimeOffset End)> BuildFreeWindows(DateTime start, DateTime end, IEnumerable<CalendarEvent> events, Settings settings)
  {
    var free = new List<(DateTimeOffset Start, DateTimeOffset End)>();
    for (var day = start.Date; day < end.Date; day = day.AddDays(1))
    {
      var dow = day.DayOfWeek;
      if (!settings.WorkWindows.TryGetValue(dow, out var windows)) continue;
      foreach (var w in windows)
      {
        var ws = new DateTimeOffset(day + w.Start, TimeSpan.Zero);
        var we = new DateTimeOffset(day + w.End, TimeSpan.Zero);
        var spans = new List<(DateTimeOffset Start, DateTimeOffset End)> { (ws, we) };
        foreach (var ev in events.Where(e => e.Start.Date == day))
        {
          var newSpans = new List<(DateTimeOffset Start, DateTimeOffset End)>();
          foreach (var span in spans)
          {
            if (ev.End <= span.Start || ev.Start >= span.End)
            {
              newSpans.Add(span);
            }
            else
            {
              if (ev.Start > span.Start)
                newSpans.Add((span.Start, ev.Start));
              if (ev.End < span.End)
                newSpans.Add((ev.End, span.End));
            }
          }
          spans = newSpans;
        }
        free.AddRange(spans);
      }
    }
    return free;
  }
}
