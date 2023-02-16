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

    public double GasCookingConsumption => Math.Round(GetGasCookingConsumption(), 4);
    public double GasWaterHeatingConsumptionWithoutBoiler => Math.Round(GetGasWaterHeatingConsumptionWithoutBoiler(), 4);
    public double GasHeatingConsumption => Math.Round(GetGasHeatingConsumption(), 4);
    public double ElectricityCooking => Math.Round(GetElectricityCooking(), 4);
    public double ElectricityWaterHeating => Math.Round(GetElectricityWaterHeating(), 4);
    public double CentralHeating => Math.Round(GetCentralHeating(), 4);
    public double CentralWater => Math.Round(GetCentralWater(), 4);
    
    public double GasCookingConsumptionFuel => Math.Round(GetGasCookingConsumption() * 1.154 / 1000, 4);
    public double GasWaterHeatingConsumptionWithoutBoilerFuel => Math.Round(GetGasWaterHeatingConsumptionWithoutBoiler() * 1.154 / 1000, 4);
    public double GasHeatingConsumptionFuel => Math.Round(GetGasHeatingConsumption() * 1.154 / 1000, 4);
    public double ElectricityCookingFuel => Math.Round(GetElectricityCooking() * 0.123 / 1000, 4);
    public double ElectricityWaterHeatingFuel => Math.Round(GetElectricityWaterHeating() * 0.123 / 1000, 4);
    public double CentralHeatingFuel => Math.Round(GetCentralHeating() * 1.154 / 1000, 4);
    public double CentralWaterFuel => Math.Round(GetCentralWater() * 1.154 / 1000, 4);


    public double S1 => Math.Round(GasCookingConsumptionFuel + CentralHeatingFuel + CentralWaterFuel, 4);
    public double S2 => Math.Round(GasWaterHeatingConsumptionWithoutBoilerFuel + ElectricityCookingFuel + CentralWaterFuel + CentralHeatingFuel, 4);
    public double S3 => Math.Round(GasCookingConsumptionFuel + CentralHeatingFuel + GasWaterHeatingConsumptionWithoutBoilerFuel, 4);
    public double S4 => Math.Round(GasCookingConsumptionFuel + GasWaterHeatingConsumptionWithoutBoilerFuel + GasHeatingConsumptionFuel, 4);
    public double S5 => Math.Round(GasWaterHeatingConsumptionWithoutBoilerFuel + GasHeatingConsumptionFuel + ElectricityCookingFuel, 4);
    public double S6 => Math.Round(GasWaterHeatingConsumptionWithoutBoilerFuel + ElectricityCookingFuel + CentralHeatingFuel, 4);
    public double S7 => Math.Round(ElectricityCookingFuel + ElectricityWaterHeatingFuel + CentralHeatingFuel, 4);
    public double S8 => Math.Round(GasHeatingConsumptionFuel + ElectricityCookingFuel + ElectricityWaterHeatingFuel, 4);
    public double S9 => Math.Round(GasCookingConsumptionFuel + GasHeatingConsumptionFuel + ElectricityWaterHeatingFuel, 4);

    

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
                 0.097 * 0.8 * (0.28 * 1 * 0.35 * 0.8 * _building.V * 353) / (100*_building.AOk)) *
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