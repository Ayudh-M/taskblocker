using Timeboxer.Domain.Abstractions;

namespace Timeboxer.Infrastructure.Tests.Fakes;

public class FakeClock : IClock
{
  public DateTimeOffset Now { get; set; }
}
