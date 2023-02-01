using CsvHelper.Configuration.Attributes;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Models;

public class House
{
    /// <summary>
    /// Идентификатор дома
    /// </summary>
    [Name("id")]
    public int Id { get; set; }

    /// <summary>
    /// Город
    /// </summary>
    [Name("formalname_city")]  
    public string City { get; set; }
    
    /// <summary>
    /// Улица
    /// </summary>
    [Name("shortname_street")]
    public string ShortNameStreet { get; set; }

    /// <summary>
    /// Улица
    /// </summary>
    [Name("formalname_street")]
    public string Street { get; set; }

    /// <summary>
    /// Номер дома
    /// </summary>
    [Name("house_number")]
    public string HouseNumber { get; set; }

    /// <summary>
    /// Полнгый адрес
    /// </summary>
    public string Address => $"{City} {Street} {ShortNameStreet} {HouseNumber}";
}