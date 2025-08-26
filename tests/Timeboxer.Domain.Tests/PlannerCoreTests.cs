using Xunit;
using FluentAssertions;
using Timeboxer.Domain.Models;
using Timeboxer.Domain.Scheduling;
using Timeboxer.Persistence;

namespace Timeboxer.Domain.Tests;

public class PlannerCoreTests
{
  [Fact]
  public async Task PlansSampleData()
  {
    var tasks = await JsonStore.LoadAsync<List<TaskItem>>(Path.Combine("Fixtures","sample_tasks.json"));
    var events = await JsonStore.LoadAsync<List<CalendarEvent>>(Path.Combine("Fixtures","sample_events.json"));
    var settings = new Settings
    {
      WorkWindows = new Dictionary<DayOfWeek, List<(TimeSpan, TimeSpan)>>
      {
        [DayOfWeek.Monday] = new() { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(18)) },
        [DayOfWeek.Tuesday] = new() { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(18)) },
        [DayOfWeek.Wednesday] = new() { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(18)) },
        [DayOfWeek.Thursday] = new() { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(18)) },
        [DayOfWeek.Friday] = new() { (TimeSpan.FromHours(8.5), TimeSpan.FromHours(18)) }
      }
    };
    var planner = new Planner(settings);
    var result = planner.Plan(tasks, events, new DateOnly(2025,8,26), new DateOnly(2025,9,2));
    result.Timeboxes.Should().NotBeEmpty();
    Validation.HasNoOverlaps(result.Timeboxes).Should().BeTrue();
  }
}
