using System.CommandLine;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;
using Timeboxer.Domain.Export;
using Timeboxer.Persistence;

namespace Timeboxer.Cli.Commands;

public static class PlanCommand
{
  public static Command Create()
  {
    var cmd = new Command("plan", "Plan tasks");
    var tasksOpt = new Option<string>("--tasks") { IsRequired = true };
    var eventsOpt = new Option<string>("--events") { IsRequired = true };
    var settingsOpt = new Option<string>("--settings") { IsRequired = true };
    var fromOpt = new Option<DateTime>("--from") { IsRequired = true };
    var toOpt = new Option<DateTime>("--to") { IsRequired = true };
    var icsOpt = new Option<string>("--out-ics", () => string.Empty);
    var textOpt = new Option<string>("--out-text", () => string.Empty);
    cmd.AddOption(tasksOpt);
    cmd.AddOption(eventsOpt);
    cmd.AddOption(settingsOpt);
    cmd.AddOption(fromOpt);
    cmd.AddOption(toOpt);
    cmd.AddOption(icsOpt);
    cmd.AddOption(textOpt);

    cmd.SetHandler(async (string tasksPath, string eventsPath, string settingsPath, DateTime from, DateTime to, string ics, string text) =>
    {
      var tasks = await JsonStore.LoadAsync<List<TaskItem>>(tasksPath);
      var events = await JsonStore.LoadAsync<List<CalendarEvent>>(eventsPath);
      var settings = await JsonStore.LoadAsync<Settings>(settingsPath);
      var planner = new Planner(settings);
      var result = planner.Plan(tasks, events, DateOnly.FromDateTime(from), DateOnly.FromDateTime(to));
      var summary = ConsolePrinter.Render(result.Timeboxes);
      Console.WriteLine(summary);
      if (!string.IsNullOrWhiteSpace(ics))
        await File.WriteAllTextAsync(ics, IcsExporter.Export(result.Timeboxes));
      if (!string.IsNullOrWhiteSpace(text))
        await File.WriteAllTextAsync(text, summary);
      if (result.Overflow.Any())
        Environment.ExitCode = 2;
    }, tasksOpt, eventsOpt, settingsOpt, fromOpt, toOpt, icsOpt, textOpt);

    return cmd;
  }
}
