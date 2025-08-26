namespace Timeboxer.Domain.Models;

public record WorkWindow(DayOfWeek Day, TimeSpan StartLocalTime, TimeSpan EndLocalTime);
