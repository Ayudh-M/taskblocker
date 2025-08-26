using Timeboxer.Domain.Models;
using Timeboxer.Domain.Utils;

namespace Timeboxer.Domain.Scheduling;

public class PlannerResult
{
  public List<Timebox> Timeboxes { get; } = new();
  public List<TaskItem> Overflow { get; } = new();
}

public static class Planner
{
  public static PlannerResult Plan(IEnumerable<TaskItem> tasks, IEnumerable<CalendarEvent> events, Settings settings, DateTime start, DateTime end)
  {
    var result = new PlannerResult();
    var free = FreeWindowBuilder.BuildFreeWindows(start, end, events, settings)
      .OrderBy(f => f.Start)
      .ToList();
    var queue = new Queue<(DateTimeOffset Start, DateTimeOffset End)>(free);

    foreach (var task in TagBatching.Apply(Heuristics.OrderTasks(tasks), settings.TagClusteringEnabled))
    {
      var remaining = (int)Math.Ceiling(task.EstimateMinutes * (1 + settings.Contingency[task.Difficulty] / 100.0));
      var beforeDeadline = queue.Where(f => f.Start < task.Deadline).ToList();
      queue = new Queue<(DateTimeOffset Start, DateTimeOffset End)>(beforeDeadline);
      if (queue.Count == 0)
      {
        result.Overflow.Add(task);
        continue;
      }
      while (remaining > 0 && queue.Count > 0)
      {
        var (s, e) = queue.Dequeue();
        if (s >= task.Deadline) break;
        if (e > task.Deadline) e = task.Deadline;
        var available = (int)(e - s).TotalMinutes;
        if (available <= 0) continue;
        var chunk = Math.Min(remaining, Math.Min(settings.MaxTimeboxMinutes, available));
        var tb = new Timebox
        {
          TaskId = task.Id,
          Start = s,
          End = s.AddMinutes(chunk),
          Kind = TimeboxKind.Work,
          PrimaryTag = task.Tags.FirstOrDefault()
        };
        result.Timeboxes.Add(tb);
        remaining -= chunk;
        var nextStart = tb.End.AddMinutes(settings.MinBreakMinutes);
        result.Timeboxes.Add(new Timebox
        {
          Start = tb.End,
          End = tb.End.AddMinutes(settings.MinBreakMinutes),
          Kind = TimeboxKind.Break
        });
        if (nextStart < e)
          queue.Enqueue((nextStart, e));
      }
      if (remaining > 0)
        result.Overflow.Add(task);
    }

    // daily buffer
    var grouped = result.Timeboxes.Where(t => t.Kind == TimeboxKind.Work)
      .GroupBy(t => t.Start.Date);
    foreach (var g in grouped)
    {
      var workMinutes = g.Sum(t => (int)(t.End - t.Start).TotalMinutes);
      var bufferMinutes = (int)Math.Ceiling(workMinutes * settings.DailyBufferPercent / 100.0);
      if (bufferMinutes > 0)
      {
        var endOfDay = g.Max(t => t.End);
        result.Timeboxes.Add(new Timebox
        {
          Start = endOfDay,
          End = endOfDay.AddMinutes(bufferMinutes),
          Kind = TimeboxKind.Buffer
        });
      }
    }

    result.Timeboxes.Sort((a, b) => a.Start.CompareTo(b.Start));
    return result;
  }
}
