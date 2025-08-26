using System.CommandLine;
using Timeboxer.Cli.Commands;

var root = new RootCommand("Timeboxer CLI");
root.AddCommand(PlanCommand.Create());
root.AddCommand(DemoCommand.Create());
root.AddCommand(ImportGoogleCommand.Create());
root.AddCommand(SyncGoogleCommand.Create());
root.AddCommand(ValidateCommand.Create());

return await root.InvokeAsync(args);
