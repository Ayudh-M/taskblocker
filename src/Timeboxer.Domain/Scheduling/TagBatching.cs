using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Scheduling;

public static class TagBatching
{
  public static IEnumerable<TaskItem> Apply(IEnumerable<TaskItem> tasks)
  {
    return tasks.OrderBy(t => t.Tags.FirstOrDefault());
  }
}
