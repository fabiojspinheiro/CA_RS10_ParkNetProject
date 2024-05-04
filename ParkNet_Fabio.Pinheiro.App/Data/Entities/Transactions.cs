namespace ParkNet_Fabio.Pinheiro.App.Data.Entities;

public class Transactions
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public IdentityUser User { get; set; }

    public DateTime DateTime { get; set; }

    public double Value { get; set; }
}
