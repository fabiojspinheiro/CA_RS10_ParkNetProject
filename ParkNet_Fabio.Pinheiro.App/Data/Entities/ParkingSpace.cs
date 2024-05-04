namespace ParkNet_Fabio.Pinheiro.App.Data.Entities;

public class ParkingSpace
{
    public int Id { get; set; }

    public int FloorId { get; set; }
    public Floor Floor { get; set; }

    public int TypeId { get; set; }
    public VehicleType Type { get; set; }

    public string Name { get; set; }
}
