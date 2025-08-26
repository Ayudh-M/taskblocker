using Timeboxer.Domain.Abstractions;
using Timeboxer.Domain.Models;

namespace Timeboxer.Infrastructure.Tests.Fakes;

public class FakeCalendarProvider : ICalendarProvider
{
  public List<CalendarEvent> Events { get; } = new();
  public string ProviderName => "Fake";
  public Task<IReadOnlyList<CalendarEvent>> GetFixedEventsAsync(DateTimeOffset startInclusive, DateTimeOffset endExclusive, CancellationToken ct)
    => Task.FromResult<IReadOnlyList<CalendarEvent>>(Events);
  public Task UpsertTimeboxesAsync(IReadOnlyList<Timebox> timeboxes, CancellationToken ct)
    => Task.CompletedTask;
}
