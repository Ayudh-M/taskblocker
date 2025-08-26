using Xunit;
using Timeboxer.Infrastructure.Tests.Fakes;
using FluentAssertions;

namespace Timeboxer.Infrastructure.Tests;

public class GoogleProviderMockTests
{
  [Fact]
  public async Task FakeProviderReturnsEvents()
  {
    var provider = new FakeCalendarProvider();
    provider.Events.Add(new Domain.Models.CalendarEvent{ Title="A" });
    var events = await provider.GetFixedEventsAsync(DateTime.Now, DateTime.Now, CancellationToken.None);
    events.Should().HaveCount(1);
  }
}
