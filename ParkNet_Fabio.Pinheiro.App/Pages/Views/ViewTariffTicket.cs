namespace ParkNet_Fabio.Pinheiro.App.Pages.Views;

public class ViewTariffTicket
{
    public int Id { get; set; }
    public string Type { get; set; }
    public TimeOnly Start { get; set; }
    [Required]
    public TimeOnly End { get; set; }
    [Required]
    public double Value { get; set; }
}
