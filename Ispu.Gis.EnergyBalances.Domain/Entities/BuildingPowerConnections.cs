namespace Ispu.Gis.EnergyBalances.Domain.Entities;

/// <summary>
/// Способы подключения здания к различным вариантам энергообеспечения
/// </summary>
public class BuildingPowerConnections
{
    private readonly Building _building;
    private readonly Standard _standard = new();

    public BuildingPowerConnections(Building building) =>
        (_building) = (building);

    public double GasCookingConsumption => GetGasCookingConsumption();
    public double GasWaterHeatingConsumptionWithoutBoiler => GetGasWaterHeatingConsumptionWithoutBoiler();
    public double GasHeatingConsumption => GetGasHeatingConsumption();
    public double ElectricityCooking => GetElectricityCooking();
    public double ElectricityWaterHeating => GetElectricityWaterHeating();
    public double CentralHeating => GetCentralHeating();
    public double CentralWater => GetCentralWater();
    
    public double GasCookingConsumptionFuel => GetGasCookingConsumption() * 1.154 / 1000;
    public double GasWaterHeatingConsumptionWithoutBoilerFuel => GetGasWaterHeatingConsumptionWithoutBoiler() * 1.154 / 1000;
    public double GasHeatingConsumptionFuel => GetGasHeatingConsumption() * 1.154 / 1000;
    public double ElectricityCookingFuel => GetElectricityCooking() * 0.123 / 1000;
    public double ElectricityWaterHeatingFuel => GetElectricityWaterHeating() * 0.123 / 1000;
    public double CentralHeatingFuel => GetCentralHeating() * 1.154 / 1000;
    public double CentralWaterFuel => GetCentralWater() * 1.154 / 1000;


    public double S1 => GasCookingConsumptionFuel + CentralHeatingFuel + CentralWaterFuel;
    public double S2 => GasWaterHeatingConsumptionWithoutBoilerFuel + ElectricityCookingFuel + CentralWaterFuel + CentralHeatingFuel;
    public double S3 => GasCookingConsumptionFuel + CentralHeatingFuel + GasWaterHeatingConsumptionWithoutBoilerFuel;
    public double S4 => GasCookingConsumptionFuel + GasWaterHeatingConsumptionWithoutBoilerFuel + GasHeatingConsumptionFuel;
    public double S5 => GasWaterHeatingConsumptionWithoutBoilerFuel + GasHeatingConsumptionFuel + ElectricityCookingFuel;
    public double S6 => GasWaterHeatingConsumptionWithoutBoilerFuel + ElectricityCookingFuel + CentralHeatingFuel;
    public double S7 => ElectricityCookingFuel + ElectricityWaterHeatingFuel + CentralHeatingFuel;
    public double S8 => GasHeatingConsumptionFuel + ElectricityCookingFuel + ElectricityWaterHeatingFuel;
    public double S9 => GasCookingConsumptionFuel + GasHeatingConsumptionFuel + ElectricityWaterHeatingFuel;

    

    /// <summary>
    /// Получение потребеление газа на пищеприготовление
    /// </summary>
    /// <returns></returns>
    private double GetGasCookingConsumption()
    {
        return _standard.GasCookingConsumption / (_standard.GasCombustionHeat * 12) * _standard.GasCookingCoefficient *
               _building.ResidentsCount;
    }

    private double GetGasWaterHeatingConsumptionWithoutBoiler()
    {
        return _standard.GasWaterHeatingConsumptionWithoutBoiler / (_standard.GasCombustionHeat * 12) *
               _standard.GasWaterHeatingConsumptionWithoutBoilerCoefficient *
               _building.ResidentsCount;
    }

    private double GetGasWaterHeatingConsumptionWithBoiler()
    {
        return _standard.GasWaterHeatingConsumptionWithBoiler / (_standard.GasCombustionHeat * 12) *
               _standard.GasWaterHeatingConsumptionWithBoilerCoefficient *
               _building.ResidentsCount;
    }

    private double GetGasHeatingConsumption()
    {
        return ((3.32 / (21 - _standard.ColdTemperature) +
                 (0.097 * 0.8 * (0.28 * 1 * 0.35 * 0.8 * _building.V * 353) / _building.AOk)) *
                (_standard.DDay * _building.AOk) -
                (0.864 * _standard.ZHeating * _building.AOk - 0.223 * _standard.IMean * _building.AF)) /
               (_standard.GasCombustionHeat * 12 * 0.8);
    }

    private double GetElectricityCooking()
    {
        return 600 * _building.ResidentsCount / 12;
    }

    private double GetElectricityWaterHeating() => 3627.609;

    private double GetCentralHeating()
    {
        return _standard.HH * _building.LivingSquare;
    }

    private double GetCentralWater()
    {
        return _standard.HW * _building.ResidentsCount * 0.0677;
    }
}