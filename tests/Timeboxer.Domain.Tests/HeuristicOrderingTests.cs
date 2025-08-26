using Xunit;
using FluentAssertions;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;

namespace Timeboxer.Domain.Tests;

public class HeuristicOrderingTests
{
  [Fact]
  public void OrdersByDeadlinePriorityAndDifficulty()
  {
    var now = DateTimeOffset.Now;
    var tasks = new List<TaskItem>
    {
      new() { Id = "A", EstimateMinutes = 30, Deadline = now.AddDays(1), Priority = Priority.P2, Difficulty = Difficulty.Easy },
      new() { Id = "B", EstimateMinutes = 30, Deadline = now.AddDays(1), Priority = Priority.P1, Difficulty = Difficulty.Easy },
      new() { Id = "C", EstimateMinutes = 30, Deadline = now.AddDays(2), Priority = Priority.P1, Difficulty = Difficulty.VHard }
    };
    var ordered = Heuristics.Order(tasks).Select(t => t.Id).ToList();
    ordered.Should().ContainInOrder("B", "A", "C");
  }
}
