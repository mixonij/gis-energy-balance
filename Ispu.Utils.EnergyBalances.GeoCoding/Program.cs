using CommandLine;
using Ispu.Utils.EnergyBalances.GeoCoding.Commands;
using Ispu.Utils.EnergyBalances.GeoCoding.Options;
using Serilog;

// Создаем логгер
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Парсим аргументы командной строки и вызываем команды
await Parser.Default.ParseArguments<HousesOptions>(args)
    .MapResult(HousesCommands.ProcessRowsAsync,
        _ => Task.FromResult(1));