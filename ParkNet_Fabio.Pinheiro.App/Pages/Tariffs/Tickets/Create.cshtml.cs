namespace ParkNet_Fabio.Pinheiro.App.Pages.Tariffs.Tickets;

[Authorize (Roles = "Admin")]
public class CreateModel : PageModel
{
    private readonly TariffTicketsRepository _tariffTicketsRepository;
    private readonly VehicleRepository _vehicleRepository;

    public CreateModel(TariffTicketsRepository tariffTicketsRepository,
        VehicleRepository vehicleRepository)
    {
        _tariffTicketsRepository = tariffTicketsRepository;
        _vehicleRepository = vehicleRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        ViewData["TypeId"] = new SelectList(await _vehicleRepository.GetAllTypesAsync(), "Id", "Type");
        return Page();
    }

    [BindProperty]
    public TariffTicket TariffTicket { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _tariffTicketsRepository.AddAsync(TariffTicket);

        return RedirectToPage("./Index");
    }
}
