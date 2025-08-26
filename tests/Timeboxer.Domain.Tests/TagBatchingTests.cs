using Xunit;
using FluentAssertions;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;

namespace Timeboxer.Domain.Tests;

public class TagBatchingTests
{
  [Fact]
  public void GroupsByTag()
  {
    var now = DateTimeOffset.Now.AddDays(1);
    var tasks = new List<TaskItem>
    {
      new() { Id="1", EstimateMinutes=10, Deadline=now, Priority=Priority.P1, Difficulty=Difficulty.Easy, Tags=new(){"a"} },
      new() { Id="2", EstimateMinutes=10, Deadline=now, Priority=Priority.P1, Difficulty=Difficulty.Easy, Tags=new(){"b"} },
      new() { Id="3", EstimateMinutes=10, Deadline=now, Priority=Priority.P1, Difficulty=Difficulty.Easy, Tags=new(){"a"} }
    };
    var ordered = TagBatching.Apply(tasks).Select(t => t.Id).ToList();
    ordered.Should().Equal(new[]{"1","3","2"});
  }
}
