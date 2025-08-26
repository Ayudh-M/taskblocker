using System.CommandLine;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;
using Timeboxer.Domain.Export;

namespace Timeboxer.Cli.Commands;

public static class DemoCommand
{
  public static Command Create()
  {
    var cmd = new Command("demo", "Run demo planner");
    cmd.SetHandler(() =>
    {
      var settings = new Settings
      {
        WorkWindows = new Dictionary<DayOfWeek, List<(TimeSpan, TimeSpan)>>
        {
          [DayOfWeek.Monday] = new() { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(17)) },
          [DayOfWeek.Tuesday] = new() { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(17)) },
          [DayOfWeek.Wednesday] = new() { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(17)) },
          [DayOfWeek.Thursday] = new() { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(17)) },
          [DayOfWeek.Friday] = new() { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(17)) }
        }
      };
      var tasks = new List<TaskItem>
      {
        new TaskItem{ Id="T1", Title="Demo Task", EstimateMinutes=60, Deadline=DateTimeOffset.Now.AddDays(2), Priority=Priority.P1, Difficulty=Difficulty.Med, Tags=new(){"demo"}}
      };
      var events = new List<CalendarEvent>();
      var planner = new Planner(settings);
      var today = DateOnly.FromDateTime(DateTime.Now.Date);
      var result = planner.Plan(tasks, events, today, today.AddDays(5));
      Console.WriteLine(ConsolePrinter.Render(result.Timeboxes));
    });
    return cmd;
  }
}
