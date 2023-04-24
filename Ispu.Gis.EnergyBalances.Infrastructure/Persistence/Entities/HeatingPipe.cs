using NetTopologySuite.Geometries;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

/// <summary>
/// Труба тепловая
/// </summary>
public class HeatingPipe
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Геометрия
    /// </summary>
    public MultiLineString Geometry { get; set; }

    /// <summary>
    /// Диаметр подающей трубы
    /// </summary>
    public decimal DPod { get; set; }

    /// <summary>
    /// Диаметр отводящей трубы
    /// </summary>
    public decimal DObr { get; set; }
    
    /// <summary>
    /// Идентификатор тепловой станции
    /// </summary>
    public int? HeatingStationId { get; set; }
    
    /// <summary>
    /// Тепловая станция
    /// </summary>
    public HeatingStation? HeatingStation { get; set; }
}