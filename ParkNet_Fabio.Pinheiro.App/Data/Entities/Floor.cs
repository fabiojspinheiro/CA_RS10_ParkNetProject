namespace ParkNet_Fabio.Pinheiro.App.Data.Entities;

public class Floor
{
    public int Id { get; set; }

    public int ParkId { get; set; }
    public Park Park { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<string> RowOfSpaces { get; set; } = new List<string>();
}
