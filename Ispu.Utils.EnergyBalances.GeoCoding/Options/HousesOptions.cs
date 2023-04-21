using CommandLine;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Options;

/// <summary>
/// Опции геокодирования
/// </summary>
[Verb("houses", HelpText = "Геокодирование информации о домах")]
public class HousesOptions
{
    /// <summary>
    /// Полное имя файла с данными CSV
    /// </summary>
    [Option('p', "path", Default = "/Users/maksimmalafeev/Downloads/export-reestrmkd-37-20221201.csv",
        HelpText = "Полное имя файла для экспорта")]
    public string CsvPath { get; set; }

    /// <summary>
    /// Полное имя файла с данными OSM
    /// </summary>
    [Option('o', "osm", Default = "/Users/maksimmalafeev/Downloads/export.osm.pbf",
        HelpText = "Полное имя файла OSM для экспорта")]
    public string PbfPath { get; set; }
    
    /// <summary>
    /// Строка подключения к БД
    /// </summary>
    [Option('c', "connection-string", Default = "Server=localhost;Port=5432;Database=energy_balances;UserId=postgres;Password=postgres;")]
    public string PgConnectionString { get; set; }
}