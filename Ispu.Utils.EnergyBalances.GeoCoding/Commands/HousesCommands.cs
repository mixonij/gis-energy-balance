using System.Globalization;
using System.Text.Json;
using System.Web;
using CsvHelper;
using CsvHelper.Configuration;
using Ispu.Utils.EnergyBalances.GeoCoding.Models;
using Ispu.Utils.EnergyBalances.GeoCoding.Options;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Commands;

public class HousesCommands
{
    public static async Task<int> ProcessRowsAsync(HousesOptions housesOptions)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null,
            Delimiter = ";"
        };
        using var reader = new StreamReader(housesOptions.CsvPath);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<House>().Take(100).ToList();

        foreach (var house in records)
        {
            if (house is null)
            {
                continue;
            }

            var encodedAddress = HttpUtility.UrlEncode(house.Address).ToUpper();

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "testdsf");
            var response = await httpClient.GetAsync($"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json");
            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<dynamic>(responseStream);
        }

        return await Task.FromResult(1);
    }
}