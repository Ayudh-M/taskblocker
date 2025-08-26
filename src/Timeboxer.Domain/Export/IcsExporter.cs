using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Timeboxer.Domain.Models;
using IcalEvent = Ical.Net.CalendarComponents.CalendarEvent;

namespace Timeboxer.Domain.Export;

public static class IcsExporter
{
  public static void Export(IEnumerable<Timebox> timeboxes, string path)
  {
    var calendar = new Calendar();
    foreach (var tb in timeboxes)
    {
      var evt = new IcalEvent
      {
        Summary = tb.Kind == TimeboxKind.Work ? $"[WORK] {tb.TaskId}" : $"[{tb.Kind}]",
        DtStart = new CalDateTime(tb.Start.UtcDateTime),
        DtEnd = new CalDateTime(tb.End.UtcDateTime),
        Uid = tb.Id
      };
      calendar.Events.Add(evt);
    }
    var serializer = new CalendarSerializer();
    File.WriteAllText(path, serializer.SerializeToString(calendar));
  }
}
