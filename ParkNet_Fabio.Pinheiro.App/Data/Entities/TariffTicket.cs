namespace ParkNet_Fabio.Pinheiro.App.Data.Entities;

public class TariffTicket
{
    public int Id { get; set; }

    public int TypeId { get; set; }
    public VehicleType Type { get; set; }

    [Required]
    public TimeOnly Start { get; set; }

    [Required]
    public TimeOnly End { get; set; }

    [Required]
    public double Value { get; set; }
}
