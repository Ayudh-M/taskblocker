using Xunit;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;
using FluentAssertions;

namespace Timeboxer.Domain.Tests;

public class HeuristicOrderingTests
{
  [Fact]
  public void OrdersByDeadlineThenPriority()
  {
    var t1 = new TaskItem { Id="1", EstimateMinutes=60, Deadline=new DateTimeOffset(2025,8,28,0,0,0,TimeSpan.Zero), Priority=Priority.P2 };
    var t2 = new TaskItem { Id="2", EstimateMinutes=60, Deadline=new DateTimeOffset(2025,8,27,0,0,0,TimeSpan.Zero), Priority=Priority.P4 };
    var ordered = Heuristics.OrderTasks(new[]{t1,t2}).Select(t=>t.Id).ToList();
    ordered.Should().Equal("2","1");
  }
}
