namespace Timeboxer.Domain.Models;

public record WorkWindow(DayOfWeek Day, TimeSpan Start, TimeSpan End);
