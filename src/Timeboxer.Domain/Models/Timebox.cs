namespace Timeboxer.Domain.Models;

public class Timebox
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string? TaskId { get; set; }
  public DateTimeOffset Start { get; set; }
  public DateTimeOffset End { get; set; }
  public TimeboxKind Kind { get; set; }
  public string? PrimaryTag { get; set; }
}
