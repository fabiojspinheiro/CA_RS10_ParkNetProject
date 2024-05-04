namespace ParkNet_Fabio.Pinheiro.App.Pages.Tariffs.Tickets;

[Authorize (Roles = "Admin")]
public class DeleteModel : PageModel
{
    private readonly TariffTicketsRepository _tariffTicketsRepository;

    public DeleteModel(TariffTicketsRepository tariffTicketsRepository)
    {
        _tariffTicketsRepository = tariffTicketsRepository;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tariffticket = await _tariffTicketsRepository.GetByIdAsync(id.Value);
        if (tariffticket != null)
        {
            TariffTicket = tariffticket;
            await _tariffTicketsRepository.DeleteAsync(tariffticket);
        }

        return RedirectToPage("./Index");
    }
}
