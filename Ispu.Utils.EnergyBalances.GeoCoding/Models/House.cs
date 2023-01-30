using CsvHelper.Configuration.Attributes;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Models;

public class House
{
    [Name("houseguid")]
    public string Id { get; set; }

    [Name("formalname_city")]  
    public string City { get; set; }

    [Name("formalname_street")]
    public string Street { get; set; }

    [Name("house_number")]
    public string HouseNumber { get; set; }

    public string Address => $"{City} {Street} {HouseNumber}";
}