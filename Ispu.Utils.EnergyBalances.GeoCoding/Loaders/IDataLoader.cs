namespace Ispu.Utils.EnergyBalances.GeoCoding.Loaders;

/// <summary>
/// Загрузчик данных
/// </summary>
/// <typeparam name="T">Тип возвращаемых данных</typeparam>
public interface IDataLoader<out T> where T : class
{
    /// <summary>
    /// Загрузка данных
    /// </summary>
    /// <param name="path">Путь до источника</param>
    /// <returns>Данные</returns>
    static abstract T LoadData(string path);
}