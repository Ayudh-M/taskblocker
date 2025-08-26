using Xunit;
using FluentAssertions;
using Timeboxer.Infrastructure.Tests.Fakes;
using Timeboxer.Domain.Models;

namespace Timeboxer.Infrastructure.Tests;

public class GoogleProviderMockTests
{
  [Fact]
  public async Task FakeProviderReturnsEvents()
  {
    var provider = new FakeCalendarProvider();
    provider.Events.Add(new CalendarEvent("1","Test",DateTimeOffset.Now,DateTimeOffset.Now.AddHours(1),false,null,true,"Fake"));
    var events = await provider.GetFixedEventsAsync(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddDays(1), CancellationToken.None);
    events.Should().HaveCount(1);
  }
}
