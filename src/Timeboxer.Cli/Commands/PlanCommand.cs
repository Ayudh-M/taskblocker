using Timeboxer.Domain.Utils;
using Timeboxer.Domain.Scheduling;
using Timeboxer.Domain.Export;
using Timeboxer.Persistence;

namespace Timeboxer.Cli.Commands;

public static class PlanCommand
{
  public static void Run(string[] args)
  {
    string? tasks = null, events = null, settings = null, ics = null, text = null;
    DateTime from = DateTime.Today, to = DateTime.Today;
    for (int i = 0; i < args.Length; i++)
    {
      switch (args[i])
      {
        case "--tasks": tasks = args[++i]; break;
        case "--events": events = args[++i]; break;
        case "--settings": settings = args[++i]; break;
        case "--from": from = DateTime.Parse(args[++i]); break;
        case "--to": to = DateTime.Parse(args[++i]); break;
        case "--out-ics": ics = args[++i]; break;
        case "--out-text": text = args[++i]; break;
      }
    }
    if (tasks == null || events == null || settings == null)
    {
      Console.WriteLine("Missing required options");
      return;
    }
    var tasksData = JsonStore.LoadTasks(tasks);
    var eventsData = JsonStore.LoadEvents(events);
    var settingsData = JsonStore.LoadSettings(settings);
    var res = Planner.Plan(tasksData, eventsData, settingsData, from, to);
    if (ics != null) IcsExporter.Export(res.Timeboxes, ics);
    var summary = ConsolePrinter.Render(res.Timeboxes);
    Console.Write(summary);
    if (text != null) File.WriteAllText(text, summary);
  }
}
