namespace Timeboxer.Domain.Abstractions;

public interface IClock
{
  DateTimeOffset Now { get; }
}
