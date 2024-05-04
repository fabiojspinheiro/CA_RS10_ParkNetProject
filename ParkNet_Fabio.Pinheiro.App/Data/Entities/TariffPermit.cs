namespace ParkNet_Fabio.Pinheiro.App.Data.Entities;

public class TariffPermit
{
    public int Id { get; set; }

    public int TypeId { get; set; }
    public VehicleType Type { get; set; }

    [Required]
    public string Period { get; set; }

    [Required]
    public double Value { get; set; }
}
