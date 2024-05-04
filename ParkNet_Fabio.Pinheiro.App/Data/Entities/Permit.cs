namespace ParkNet_Fabio.Pinheiro.App.Data.Entities;

public class Permit
{
    public int Id { get; set; }

    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }

    public int ParkingSpaceId { get; set; }
    public ParkingSpace ParkingSpace { get; set; }

    public int TariffPermitId { get; set; }
    public TariffPermit TariffPermit { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public double Value { get; set; }
}
