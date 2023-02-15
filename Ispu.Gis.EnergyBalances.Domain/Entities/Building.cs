namespace Ispu.Gis.EnergyBalances.Domain.Entities;

public class Building
{
    public double LivingSquare { get; set; }
    
    public int ResidentsCount { get; set; }

    public double AOk { get; set; } = 1723;

    public double AF { get; set; } = 251;

    public double V => LivingSquare * 2.5;
}