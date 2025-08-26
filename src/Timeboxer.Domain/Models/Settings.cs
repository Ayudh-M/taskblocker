namespace Timeboxer.Domain.Models;

public class Settings
{
  public Dictionary<DayOfWeek, List<WorkWindow>> WorkWindows { get; set; } = new();
  public Dictionary<DayOfWeek, List<WorkWindow>> DeepWorkWindows { get; set; } = new();
  public int MaxTimeboxMinutes { get; set; } = 50;
  public int MinBreakMinutes { get; set; } = 10;
  public int DailyBufferPercent { get; set; } = 15;
  public Dictionary<Difficulty, int> Contingency { get; set; } = new()
  {
    [Difficulty.Easy] = 0,
    [Difficulty.Med] = 10,
    [Difficulty.Hard] = 25,
    [Difficulty.VHard] = 50
  };
  public bool TagClusteringEnabled { get; set; } = true;
  public string Timezone { get; set; } = "Europe/Amsterdam";
  public bool WritebackToGoogle { get; set; } = false;
}
