namespace Timeboxer.Domain.Models;

public record Timebox(
    string Id,
    string? TaskId,
    DateTimeOffset Start,
    DateTimeOffset End,
    TimeboxKind Kind,
    string? PrimaryTag = null
);
