using Xunit;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;
using Timeboxer.Domain.Utils;
using FluentAssertions;

namespace Timeboxer.Domain.Tests;

public class PropertyInvariantsTests
{
  [Fact]
  public void WorkTimeboxesRespectMaxDuration()
  {
    var tasks = Parsing.LoadTasks(File.ReadAllText("Fixtures/sample_tasks.json"));
    var events = Parsing.LoadEvents(File.ReadAllText("Fixtures/sample_events.json"));
    var settings = Parsing.LoadSettings(File.ReadAllText("Fixtures/sample_settings.json"));
    var result = Planner.Plan(tasks, events, settings, new DateTime(2025,8,26), new DateTime(2025,9,2));
    result.Timeboxes.Where(t=>t.Kind==TimeboxKind.Work).All(t=> (t.End - t.Start).TotalMinutes <= settings.MaxTimeboxMinutes).Should().BeTrue();
  }
}
