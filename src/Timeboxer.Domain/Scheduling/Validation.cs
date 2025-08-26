using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Scheduling;

public static class Validation
{
  public static bool HasNoOverlaps(IEnumerable<Timebox> boxes)
  {
    var ordered = boxes.OrderBy(b => b.Start).ToList();
    for (int i = 1; i < ordered.Count; i++)
    {
      if (ordered[i].Start < ordered[i - 1].End)
        return false;
    }
    return true;
  }

  public static bool BreaksSatisfied(IEnumerable<Timebox> boxes, int minBreak)
  {
    var ordered = boxes.OrderBy(b => b.Start).ToList();
    for (int i = 1; i < ordered.Count; i++)
    {
      var prev = ordered[i - 1];
      var cur = ordered[i];
      if (prev.Kind == TimeboxKind.Work && cur.Kind == TimeboxKind.Work)
      {
        if ((cur.Start - prev.End).TotalMinutes < minBreak)
          return false;
      }
    }
    return true;
  }
}
