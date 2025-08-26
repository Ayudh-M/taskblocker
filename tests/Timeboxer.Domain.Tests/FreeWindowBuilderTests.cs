using Xunit;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;
using FluentAssertions;

namespace Timeboxer.Domain.Tests;

public class FreeWindowBuilderTests
{
  [Fact]
  public void SubtractsFixedEvents()
  {
    var settings = new Settings
    {
      WorkWindows = new()
      {
        [DayOfWeek.Monday] = new List<WorkWindow>{ new(DayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(17)) }
      }
    };
    var events = new List<CalendarEvent>
    {
      new() { Start = new DateTimeOffset(2025,8,25,10,0,0,TimeSpan.Zero), End = new DateTimeOffset(2025,8,25,11,0,0,TimeSpan.Zero) }
    };
    var free = FreeWindowBuilder.BuildFreeWindows(new DateTime(2025,8,25), new DateTime(2025,8,26), events, settings);
    free.Should().HaveCount(2);
    free[0].Start.Hour.Should().Be(9);
    free[0].End.Hour.Should().Be(10);
    free[1].Start.Hour.Should().Be(11);
  }
}
