using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Scheduling;

public static class Heuristics
{
  public static IEnumerable<TaskItem> OrderTasks(IEnumerable<TaskItem> tasks)
  {
    return tasks
      .OrderBy(t => t.Deadline)
      .ThenBy(t => t.Priority)
      .ThenByDescending(t => t.Difficulty)
      .ThenByDescending(t => t.EstimateMinutes);
  }
}
