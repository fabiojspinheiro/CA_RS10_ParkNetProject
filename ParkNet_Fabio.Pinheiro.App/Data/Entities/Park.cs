namespace ParkNet_Fabio.Pinheiro.App.Data.Entities;

public class Park
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }
}
