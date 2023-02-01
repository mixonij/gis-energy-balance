using System.Text.Json.Serialization;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Models;

public class GeoJsonBuildingResponse
{
    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("licence")] public string Licence { get; set; }

    [JsonPropertyName("features")] public Feature[] Features { get; set; }
}

public partial class Feature
{
    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("properties")] public Properties Properties { get; set; }

    [JsonPropertyName("bbox")] public double[] Bbox { get; set; }

    [JsonPropertyName("geometry")] public Geometry Geometry { get; set; }
}

public partial class Geometry
{
    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("coordinates")] public double[] Coordinates { get; set; }
}

public partial class Properties
{
    [JsonPropertyName("place_id")] public long PlaceId { get; set; }

    [JsonPropertyName("osm_type")] public string OsmType { get; set; }

    [JsonPropertyName("osm_id")] public long OsmId { get; set; }

    [JsonPropertyName("display_name")] public string DisplayName { get; set; }

    [JsonPropertyName("place_rank")] public long PlaceRank { get; set; }

    [JsonPropertyName("category")] public string Category { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("importance")] public double Importance { get; set; }

    [JsonPropertyName("address")] public Address Address { get; set; }
}

public partial class Address
{
    [JsonPropertyName("house_number")] public string HouseNumber { get; set; }

    [JsonPropertyName("road")] public string Road { get; set; }

    [JsonPropertyName("suburb")] public string Suburb { get; set; }

    [JsonPropertyName("town")] public string Town { get; set; }

    [JsonPropertyName("county")] public string County { get; set; }

    [JsonPropertyName("state")] public string State { get; set; }

    [JsonPropertyName("ISO3166-2-lvl4")] public string Iso31662Lvl4 { get; set; }

    [JsonPropertyName("region")] public string Region { get; set; }

    [JsonPropertyName("postcode")] public string Postcode { get; set; }

    [JsonPropertyName("country")] public string Country { get; set; }

    [JsonPropertyName("country_code")] public string CountryCode { get; set; }
}