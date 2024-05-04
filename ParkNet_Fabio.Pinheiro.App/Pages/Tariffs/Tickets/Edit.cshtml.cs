namespace ParkNet_Fabio.Pinheiro.App.Pages.Tariffs.Tickets;

[Authorize (Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly TariffTicketsRepository _tariffTicketsRepository;
    private readonly VehicleRepository _vehicleRepository;

    public EditModel(TariffTicketsRepository tariffTicketsRepository,
        VehicleRepository vehicleRepository)
    {
        _tariffTicketsRepository = tariffTicketsRepository;
        _vehicleRepository = vehicleRepository;
    }

    [BindProperty]
    public TariffTicket TariffTicket { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tariffticket =  await _tariffTicketsRepository.GetByIdAsync(id.Value);
        if (tariffticket == null)
        {
            return NotFound();
        }
        TariffTicket = tariffticket;
        ViewData["TypeId"] = new SelectList(await _vehicleRepository.GetAllTypesAsync(), "Id", "Type");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _tariffTicketsRepository.UpdateAsync(TariffTicket);

        return RedirectToPage("./Index");
    }
}
