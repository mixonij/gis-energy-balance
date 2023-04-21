using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Loaders;

/// <summary>
/// Загрузчик CSV
/// </summary>
public class CsvLoader: IDataLoader<List<House>>
{
    public static List<House> LoadData(string path)
    {
        return GetFileRecords(path);
    }
    
    private static List<House> GetFileRecords(string path)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null,
            Delimiter = ";"
        };
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<House>().ToList();
    }
}