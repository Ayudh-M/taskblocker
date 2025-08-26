using Timeboxer.Cli.Commands;

if (args.Length == 0)
{
  Console.WriteLine("Timeboxer CLI");
  return;
}

var cmd = args[0];
var rest = args.Skip(1).ToArray();

switch (cmd)
{
  case "plan":
    PlanCommand.Run(rest);
    break;
  case "demo":
    DemoCommand.Run(rest);
    break;
  case "import-google":
    ImportGoogleCommand.Run(rest);
    break;
  case "sync-google":
    SyncGoogleCommand.Run(rest);
    break;
  case "validate":
    ValidateCommand.Run(rest);
    break;
  default:
    Console.WriteLine("Unknown command");
    break;
}
