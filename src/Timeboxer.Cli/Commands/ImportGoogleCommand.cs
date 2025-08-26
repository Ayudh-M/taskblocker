using System.CommandLine;
using Timeboxer.Domain.Abstractions;
using Timeboxer.Infrastructure.Google;
using Timeboxer.Persistence;
using Timeboxer.Domain.Models;

namespace Timeboxer.Cli.Commands;

public static class ImportGoogleCommand
{
  public static Command Create()
  {
    var cmd = new Command("import-google", "Import Google Calendar events");
    var fromOpt = new Option<DateTime>("--from") { IsRequired = true };
    var toOpt = new Option<DateTime>("--to") { IsRequired = true };
    var outOpt = new Option<string>("--out") { IsRequired = true };
    cmd.AddOption(fromOpt);
    cmd.AddOption(toOpt);
    cmd.AddOption(outOpt);
    cmd.SetHandler(async (DateTime from, DateTime to, string outPath) =>
    {
      ICalendarProvider provider = new GoogleCalendarProvider();
      var events = await provider.GetFixedEventsAsync(from, to, CancellationToken.None);
      await JsonStore.SaveAsync(outPath, events);
      Console.WriteLine($"Saved {events.Count} events.");
    }, fromOpt, toOpt, outOpt);
    return cmd;
  }
}
