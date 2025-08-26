using Xunit;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;
using FluentAssertions;

namespace Timeboxer.Domain.Tests;

public class DeadlineFeasibilityTests
{
  [Fact]
  public void DetectsOverflow()
  {
    var settings = new Settings
    {
      WorkWindows = new()
      {
        [DayOfWeek.Monday] = new List<WorkWindow>{ new(DayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(10)) }
      },
      MinBreakMinutes = 10,
      MaxTimeboxMinutes = 50
    };
    var task = new TaskItem { Id="T", EstimateMinutes=120, Deadline=new DateTimeOffset(2025,8,25,12,0,0,TimeSpan.Zero) };
    var result = Planner.Plan(new[]{task}, Array.Empty<CalendarEvent>(), settings, new DateTime(2025,8,25), new DateTime(2025,8,26));
    result.Overflow.Should().Contain(task);
  }
}
