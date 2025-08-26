namespace Timeboxer.Domain.Models;

public record CalendarEvent(
    string Id,
    string Title,
    DateTimeOffset Start,
    DateTimeOffset End,
    bool IsAllDay,
    string? Location,
    bool IsBusy,
    string Source);
