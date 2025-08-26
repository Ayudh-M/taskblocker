using Xunit;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;
using Timeboxer.Domain.Utils;
using FluentAssertions;

namespace Timeboxer.Domain.Tests;

public class BreaksAndBuffersTests
{
  [Fact]
  public void AddsDailyBuffer()
  {
    var tasks = Parsing.LoadTasks(File.ReadAllText("Fixtures/sample_tasks.json"));
    var events = Parsing.LoadEvents(File.ReadAllText("Fixtures/sample_events.json"));
    var settings = Parsing.LoadSettings(File.ReadAllText("Fixtures/sample_settings.json"));
    var result = Planner.Plan(tasks, events, settings, new DateTime(2025,8,26), new DateTime(2025,9,2));
    var buffers = result.Timeboxes.Where(t=>t.Kind==TimeboxKind.Buffer).ToList();
    buffers.Should().NotBeEmpty();
  }
}
