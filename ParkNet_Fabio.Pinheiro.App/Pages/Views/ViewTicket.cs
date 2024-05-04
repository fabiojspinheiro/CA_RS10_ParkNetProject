namespace ParkNet_Fabio.Pinheiro.App.Data.Repositories;

public class ViewTicket
{
    public int Id { get; set; }
    public string LicensePlate { get; set; }
    public string Park { get; set; }
    public string ParkingSpace { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double Value { get; set; }
}

