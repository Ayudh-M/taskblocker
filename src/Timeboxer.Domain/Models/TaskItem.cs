namespace Timeboxer.Domain.Models;

public class TaskItem
{
  public string Id { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public int EstimateMinutes { get; set; }
  public DateTimeOffset Deadline { get; set; }
  public Priority Priority { get; set; } = Priority.P3;
  public Difficulty Difficulty { get; set; } = Difficulty.Med;
  public List<string> Tags { get; set; } = new();
  public Environment? Environment { get; set; }
  public DateTimeOffset? MustStartAfter { get; set; }
  public DateTimeOffset? MustEndBefore { get; set; }
  public string? Notes { get; set; }
}
