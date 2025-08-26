using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Scheduling;

public static class Validation
{
  public static bool Validate(IEnumerable<Timebox> timeboxes, Settings settings)
  {
    var ordered = timeboxes.OrderBy(t => t.Start).ToList();
    for (int i = 1; i < ordered.Count; i++)
    {
      if (ordered[i].Start < ordered[i - 1].End)
        return false;
      if (ordered[i - 1].Kind == TimeboxKind.Work && ordered[i].Kind == TimeboxKind.Work)
      {
        if ((ordered[i].Start - ordered[i - 1].End).TotalMinutes < settings.MinBreakMinutes)
          return false;
      }
    }
    return true;
  }
}
