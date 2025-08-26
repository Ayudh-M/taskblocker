using System.Text.Json;
using Timeboxer.Domain.Models;

namespace Timeboxer.Persistence;

public static class JsonStore
{
  public static IReadOnlyList<CalendarEvent> LoadEvents(string path)
    => JsonSerializer.Deserialize<List<CalendarEvent>>(File.ReadAllText(path)) ?? new List<CalendarEvent>();

  public static IReadOnlyList<TaskItem> LoadTasks(string path)
    => JsonSerializer.Deserialize<List<TaskItem>>(File.ReadAllText(path)) ?? new List<TaskItem>();

  public static Settings LoadSettings(string path)
    => JsonSerializer.Deserialize<Settings>(File.ReadAllText(path)) ?? new Settings();

  public static void SaveTimeboxes(string path, IEnumerable<Timebox> timeboxes)
    => File.WriteAllText(path, JsonSerializer.Serialize(timeboxes, new JsonSerializerOptions { WriteIndented = true }));
}
