using Xunit;
using FluentAssertions;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;

namespace Timeboxer.Domain.Tests;

public class DeadlineFeasibilityTests
{
  [Fact]
  public void ReturnsOverflowWhenDeadlineImpossible()
  {
    var settings = new Settings
    {
      WorkWindows = new Dictionary<DayOfWeek, List<(TimeSpan, TimeSpan)>>
      {
        [DayOfWeek.Monday] = new() { (TimeSpan.FromHours(8), TimeSpan.FromHours(9)) }
      }
    };
    var tasks = new List<TaskItem>
    {
      new() { Id="T1", EstimateMinutes=120, Deadline=new DateTimeOffset(2025,8,25,12,0,0,TimeSpan.FromHours(2)), Priority=Priority.P1, Difficulty=Difficulty.Med, Tags=new(){"x"} }
    };
    var planner = new Planner(settings);
    var res = planner.Plan(tasks, new List<CalendarEvent>(), new DateOnly(2025,8,25), new DateOnly(2025,8,26));
    res.Overflow.Should().ContainSingle(t => t.Id == "T1");
  }
}
