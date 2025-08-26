using System.Text;
using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Export;

public static class ConsolePrinter
{
  public static string Render(IEnumerable<Timebox> boxes)
  {
    var sb = new StringBuilder();
    foreach (var tb in boxes.OrderBy(b => b.Start))
    {
      sb.AppendLine($"{tb.Start:yyyy-MM-dd HH:mm} - {tb.End:HH:mm} | {tb.Kind} | {tb.TaskId ?? tb.Kind.ToString()} {tb.PrimaryTag}");
    }
    return sb.ToString();
  }
}
