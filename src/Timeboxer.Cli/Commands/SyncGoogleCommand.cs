using System.CommandLine;

namespace Timeboxer.Cli.Commands;

public static class SyncGoogleCommand
{
  public static Command Create()
  {
    var cmd = new Command("sync-google", "Sync plan to Google Calendar");
    var icsOpt = new Option<string>("--ics") { IsRequired = true };
    cmd.AddOption(icsOpt);
    cmd.SetHandler((string _) =>
    {
      Console.WriteLine("Sync not implemented in this sample.");
    }, icsOpt);
    return cmd;
  }
}
