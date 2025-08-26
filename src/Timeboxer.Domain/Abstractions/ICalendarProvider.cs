using Timeboxer.Domain.Models;

namespace Timeboxer.Domain.Abstractions;

public interface ICalendarProvider
{
  Task<IReadOnlyList<CalendarEvent>> GetFixedEventsAsync(DateTime startInclusive, DateTime endExclusive, CancellationToken ct);
  Task UpsertTimeboxesAsync(IReadOnlyList<Timebox> timeboxes, CancellationToken ct);
  string ProviderName { get; }
}
