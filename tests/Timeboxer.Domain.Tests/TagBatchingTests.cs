using Xunit;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;
using FluentAssertions;

namespace Timeboxer.Domain.Tests;

public class TagBatchingTests
{
  [Fact]
  public void GroupsByPrimaryTag()
  {
    var t1 = new TaskItem{ Id="1", Tags = new(){"b"} };
    var t2 = new TaskItem{ Id="2", Tags = new(){"a"} };
    var t3 = new TaskItem{ Id="3", Tags = new(){"a"} };
    var ordered = TagBatching.Apply(new[]{t1,t2,t3}, true).Select(t=>t.Id).ToList();
    ordered.Should().Equal("2","3","1");
  }
}
