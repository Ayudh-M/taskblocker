using Xunit;
using FluentAssertions;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;

namespace Timeboxer.Domain.Tests;

public class FreeWindowBuilderTests
{
  [Fact]
  public void SubtractsEventsFromWorkWindow()
  {
    var events = new List<CalendarEvent>
    {
      new("E1","Meeting", new DateTimeOffset(2025,8,27,10,0,0,TimeSpan.FromHours(2)), new DateTimeOffset(2025,8,27,12,0,0,TimeSpan.FromHours(2)), false, null, true, "Google")
    };
    var windows = new List<(TimeSpan, TimeSpan)> { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(18)) };
    var tz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Amsterdam");
    var free = FreeWindowBuilder.BuildForDay(events, new DateOnly(2025,8,27), windows, tz);
    free.Count.Should().Be(2);
  }
}
