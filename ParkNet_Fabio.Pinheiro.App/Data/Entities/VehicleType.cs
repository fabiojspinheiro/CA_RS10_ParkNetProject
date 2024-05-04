namespace ParkNet_Fabio.Pinheiro.App.Data.Entities;

public class VehicleType
{
    public int Id { get; set; }

    [Required]
    public string Type { get; set; }
}
