using System.CommandLine;

namespace Timeboxer.Cli.Commands;

public static class ValidateCommand
{
  public static Command Create()
  {
    var cmd = new Command("validate", "Validate a generated plan");
    var planOpt = new Option<string>("--plan") { IsRequired = true };
    cmd.AddOption(planOpt);
    cmd.SetHandler(async (string planPath) =>
    {
      var text = await File.ReadAllTextAsync(planPath);
      Console.WriteLine($"Plan size {text.Length} chars");
    }, planOpt);
    return cmd;
  }
}
