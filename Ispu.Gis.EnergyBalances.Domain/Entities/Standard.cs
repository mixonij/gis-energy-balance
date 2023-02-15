using System.Text.Json;

namespace Ispu.Gis.EnergyBalances.Domain.Entities;

public class Standard
{
    public double GasCombustionHeat { get; set; } = 7900;

    public double GasCookingConsumption { get; set; } = 12000;
    
    public double GasCookingCoefficient { get; set; } = 1.26;

    public double GasWaterHeatingConsumptionWithoutBoiler { get; set; } = 14300;

    public double GasWaterHeatingConsumptionWithoutBoilerCoefficient { get; set; } = 1.3;

    public double GasWaterHeatingConsumptionWithBoiler { get; set; } = 11000;

    public double GasWaterHeatingConsumptionWithBoilerCoefficient { get; set; } = 1.28;

    public double ColdTemperature { get; set; } = -30;

    public double DDay { get; set; } = 5672;

    public double ZHeating { get; set; } = 219;

    public double IMean { get; set; } = 1292;

    public double HH { get; set; } = 0.01494;

    public double HW { get; set; } = 3.95;
}