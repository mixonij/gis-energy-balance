using OsmSharp.Complete;
using OsmSharp.Streams;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Loaders;

/// <summary>
/// Загрузчик данных OSM
/// </summary>
public class PbfLoader: IDataLoader<List<ICompleteOsmGeo>>
{
    /// <summary>
    /// Загрузка данных OSM
    /// </summary>
    /// <param name="path">Путь до источника данных</param>
    /// <returns>Данные OSM</returns>
    public static List<ICompleteOsmGeo> LoadData(string path)
    {
        using var fileStream = File.OpenRead(path);
        var source = new PBFOsmStreamSource(fileStream);
        
        return source.ToComplete().ToList();
    }
}