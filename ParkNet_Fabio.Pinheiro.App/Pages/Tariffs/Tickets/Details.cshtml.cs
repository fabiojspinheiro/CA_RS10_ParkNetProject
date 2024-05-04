namespace ParkNet_Fabio.Pinheiro.App.Pages.Tariffs.Tickets;

[Authorize (Roles = "Admin")]
public class DetailsModel : PageModel
{
    private readonly TariffTicketsRepository _tariffTicketsRepository;

    public DetailsModel(TariffTicketsRepository tariffTicketsRepository)
    {
        _tariffTicketsRepository = tariffTicketsRepository;
    }

    public TariffTicket TariffTicket { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tariffticket = await _tariffTicketsRepository.GetByIdAsync(id.Value);
        if (tariffticket == null)
        {
            return NotFound();
        }
        else
        {
            TariffTicket = tariffticket;
        }
        return Page();
    }
}
