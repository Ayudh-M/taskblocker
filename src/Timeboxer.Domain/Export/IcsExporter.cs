using System.Text;
using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Export;

public static class IcsExporter
{
  public static string Export(IEnumerable<Timebox> boxes)
  {
    var sb = new StringBuilder();
    sb.AppendLine("BEGIN:VCALENDAR");
    sb.AppendLine("VERSION:2.0");
    sb.AppendLine("PRODID:-//Timeboxer//EN");
    foreach (var tb in boxes)
    {
      sb.AppendLine("BEGIN:VEVENT");
      sb.AppendLine($"UID:{tb.Id}");
      sb.AppendLine($"DTSTART:{tb.Start.UtcDateTime:yyyyMMdd'T'HHmmss'Z'}");
      sb.AppendLine($"DTEND:{tb.End.UtcDateTime:yyyyMMdd'T'HHmmss'Z'}");
      var summary = tb.Kind == TimeboxKind.Work ? $"[WORK] {tb.PrimaryTag}" : $"[{tb.Kind}]";
      sb.AppendLine($"SUMMARY:{summary}");
      sb.AppendLine("END:VEVENT");
    }
    sb.AppendLine("END:VCALENDAR");
    return sb.ToString();
  }
}
