using System.Text.Json.Serialization;

namespace Timeboxer.Domain.Models;

public class TaskItem
{
  public string Id { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public int EstimateMinutes { get; set; }
  public DateTimeOffset Deadline { get; set; }
  public Priority Priority { get; set; }
  public Difficulty Difficulty { get; set; }
  public List<string> Tags { get; set; } = new();
  public string? Notes { get; set; }
}
