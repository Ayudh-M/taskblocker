using System.Text;
using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Export;

public static class ConsolePrinter
{
  public static string Render(IEnumerable<Timebox> timeboxes)
  {
    var sb = new StringBuilder();
    foreach (var tb in timeboxes.OrderBy(t => t.Start))
    {
      sb.AppendLine($"{tb.Start:yyyy-MM-dd HH:mm}-{tb.End:HH:mm} {tb.Kind} {tb.TaskId}");
    }
    return sb.ToString();
  }
}
