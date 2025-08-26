using System.Text.Json;
using System.Text.Json.Serialization;
using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Utils;

public static class Parsing
{
  private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
  {
    PropertyNameCaseInsensitive = true,
    Converters = { new JsonStringEnumConverter() }
  };

  public static List<CalendarEvent> LoadEvents(string json)
    => JsonSerializer.Deserialize<List<CalendarEvent>>(json, Options) ?? new();

  public static List<TaskItem> LoadTasks(string json)
    => JsonSerializer.Deserialize<List<TaskItem>>(json, Options) ?? new();

  public static Settings LoadSettings(string json)
  {
    var doc = JsonDocument.Parse(json);
    var s = new Settings();
    if (doc.RootElement.TryGetProperty("MaxTimeboxMinutes", out var mt)) s.MaxTimeboxMinutes = mt.GetInt32();
    if (doc.RootElement.TryGetProperty("MinBreakMinutes", out var mb)) s.MinBreakMinutes = mb.GetInt32();
    if (doc.RootElement.TryGetProperty("DailyBufferPercent", out var db)) s.DailyBufferPercent = db.GetInt32();
    if (doc.RootElement.TryGetProperty("TagClusteringEnabled", out var tc)) s.TagClusteringEnabled = tc.GetBoolean();
    if (doc.RootElement.TryGetProperty("WorkWindows", out var ww))
    {
      foreach (var dayProp in ww.EnumerateObject())
      {
        if (!Enum.TryParse<DayOfWeek>(dayProp.Name, true, out var day)) continue;
        var list = new List<WorkWindow>();
        foreach (var arr in dayProp.Value.EnumerateArray())
        {
          var start = TimeSpan.Parse(arr[0].GetString()!);
          var end = TimeSpan.Parse(arr[1].GetString()!);
          list.Add(new WorkWindow(day, start, end));
        }
        s.WorkWindows[day] = list;
      }
    }
    return s;
  }
}
