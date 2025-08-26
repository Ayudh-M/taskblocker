namespace Timeboxer.Domain.Models;

public class CalendarEvent
{
  public string Id { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public DateTimeOffset Start { get; set; }
  public DateTimeOffset End { get; set; }
  public bool IsAllDay { get; set; }
  public string? Location { get; set; }
  public bool IsBusy { get; set; }
  public string Source { get; set; } = "Local";
  public string? Recurrence { get; set; }
}
