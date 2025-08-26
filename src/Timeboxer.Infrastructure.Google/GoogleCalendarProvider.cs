using Timeboxer.Domain.Abstractions;
using Timeboxer.Domain.Models;

namespace Timeboxer.Infrastructure.Google;

public class GoogleCalendarProvider : ICalendarProvider
{
  public string ProviderName => "Google";

  public Task<IReadOnlyList<CalendarEvent>> GetFixedEventsAsync(DateTime startInclusive, DateTime endExclusive, CancellationToken ct)
  {
    return Task.FromResult<IReadOnlyList<CalendarEvent>>(new List<CalendarEvent>());
  }

  public Task UpsertTimeboxesAsync(IReadOnlyList<Timebox> timeboxes, CancellationToken ct)
  {
    return Task.CompletedTask;
  }
}
