using CommandLine;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Options;

[Verb("houses", HelpText = "Геокодирование информации о домах")]
public class HousesOptions
{
    /// <summary>
    /// Полное имя файла с данными
    /// </summary>
    [Option('p', "path", Default = "/Users/maksimmalafeev/Downloads/export-reestrmkd-37-20221201.csv",
        HelpText = "Полное имя файла для экспорта")]
    public string CsvPath { get; set; }
}