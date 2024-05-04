namespace ParkNet_Fabio.Pinheiro.App.Data.Entities;

public class Customer
{
    public int Id { get; set; }
    public string UserId { get; set; }              
    public IdentityUser User { get; set; }

    public string DLN { get; set; }     // — Driver License Number

    public DateOnly DLNExpDate { get; set; }    // - Driver License Expiration Date

    public string CreditCard { get; set; }

    public DateOnly CreditCardExpDate { get; set; }
}
