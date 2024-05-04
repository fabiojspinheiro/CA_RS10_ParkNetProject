namespace ParkNet_Fabio.Pinheiro.App.Data.Entities;

public class Vehicle
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public IdentityUser User { get; set; }

    [Required]
    public string LicensePlate { get; set; }

    public int TypeId { get; set;}
    public VehicleType Type { get; set;}
}
