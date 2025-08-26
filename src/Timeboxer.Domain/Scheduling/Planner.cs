using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Scheduling;

public class Planner
{
  private readonly Settings _settings;

  public Planner(Settings settings)
  {
    _settings = settings;
  }

  public PlanResult Plan(IEnumerable<TaskItem> tasks, IEnumerable<CalendarEvent> events, DateOnly from, DateOnly to)
  {
    var tz = TimeZoneInfo.FindSystemTimeZoneById(_settings.Timezone);
    var free = new Dictionary<DateOnly, List<(DateTimeOffset Start, DateTimeOffset End)>>();
    for (var day = from; day < to; day = day.AddDays(1))
    {
      if (!_settings.WorkWindows.TryGetValue(day.DayOfWeek, out var windows) || windows.Count == 0)
        continue;
      var dayEvents = events.Where(e => e.Start.Date == day.ToDateTime(TimeOnly.MinValue).Date);
      free[day] = FreeWindowBuilder.BuildForDay(dayEvents, day, windows, tz);
    }

    IEnumerable<TaskItem> ordered = Heuristics.Order(tasks);
    if (_settings.TagClusteringEnabled)
      ordered = TagBatching.Apply(ordered);

    var plan = new List<Timebox>();
    var overflow = new List<TaskItem>();
    int idCounter = 1;

    foreach (var task in ordered)
    {
      var contingency = _settings.Contingency.GetValueOrDefault(task.Difficulty, 0);
      var required = (int)Math.Ceiling(task.EstimateMinutes * (1 + contingency / 100.0));
      var remaining = required;
      var deadlineDay = DateOnly.FromDateTime(task.Deadline.DateTime);


        while (remaining > 0)
        {
          var chosenDay = free.Keys.OrderBy(d => d).FirstOrDefault(d => d <= deadlineDay && free[d].Any());
          var hasDay = chosenDay != default && free.ContainsKey(chosenDay);
          if (!hasDay)
          {
            overflow.Add(task);
            break;
          }
          var intervals = free[chosenDay];
          var interval = intervals[0];
          var intervalMinutes = (int)(interval.End - interval.Start).TotalMinutes;
          var chunk = Math.Min(Math.Min(remaining, _settings.MaxTimeboxMinutes), intervalMinutes);
          var workStart = interval.Start;
          var workEnd = workStart.AddMinutes(chunk);
          plan.Add(new Timebox($"tb{idCounter++}", task.Id, workStart, workEnd, TimeboxKind.Work, task.Tags.FirstOrDefault()));
          remaining -= chunk;
          var cursor = workEnd;
          if (remaining > 0)
          {
            var breakEnd = cursor.AddMinutes(_settings.MinBreakMinutes);
            plan.Add(new Timebox($"tb{idCounter++}", null, cursor, breakEnd, TimeboxKind.Break));
            cursor = breakEnd;
          }
          if (cursor < interval.End)
            intervals[0] = (cursor, interval.End);
          else
            intervals.RemoveAt(0);
        }
        }
    return new PlanResult(plan.OrderBy(p => p.Start).ToList(), overflow);
  }
}

public record PlanResult(IReadOnlyList<Timebox> Timeboxes, IReadOnlyList<TaskItem> Overflow);
