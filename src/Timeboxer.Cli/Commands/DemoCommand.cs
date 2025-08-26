using Timeboxer.Domain.Utils;
using Timeboxer.Domain.Scheduling;
using Timeboxer.Domain.Export;

namespace Timeboxer.Cli.Commands;

public static class DemoCommand
{
  private const string TasksJson = "[{\"Id\":\"Tdemo\",\"Title\":\"Demo Task\",\"EstimateMinutes\":60,\"Deadline\":\"2025-08-27T18:00:00+02:00\",\"Priority\":\"P2\",\"Difficulty\":\"Med\",\"Tags\":[\"demo\"]}]";
  private const string EventsJson = "[]";
  private const string SettingsJson = "{\"WorkWindows\":{\"Monday\":[[\"08:30\",\"17:00\"]],\"Tuesday\":[[\"08:30\",\"17:00\"]],\"Wednesday\":[[\"08:30\",\"17:00\"]],\"Thursday\":[[\"08:30\",\"17:00\"]],\"Friday\":[[\"08:30\",\"17:00\"]]},\"DeepWorkWindows\":{},\"MaxTimeboxMinutes\":50,\"MinBreakMinutes\":10,\"DailyBufferPercent\":15,\"Contingency\":{\"Easy\":0,\"Med\":10,\"Hard\":25,\"VHard\":50},\"TagClusteringEnabled\":true}";

  public static void Run(string[] args)
  {
    var tasks = Parsing.LoadTasks(TasksJson);
    var events = Parsing.LoadEvents(EventsJson);
    var settings = Parsing.LoadSettings(SettingsJson);
    var res = Planner.Plan(tasks, events, settings, DateTime.Today, DateTime.Today.AddDays(5));
    var summary = ConsolePrinter.Render(res.Timeboxes);
    Console.Write(summary);
  }
}
