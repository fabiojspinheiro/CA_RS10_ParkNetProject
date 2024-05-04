namespace ParkNet_Fabio.Pinheiro.App.Pages.Tariffs.Permits;

[Authorize (Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly TariffPermitsRepository _tariffPermitsRepository;
    private readonly VehicleRepository _vehicleRepository;

    public EditModel(TariffPermitsRepository tariffPermitsRepository,
        VehicleRepository vehicleRepository)
    {
        _tariffPermitsRepository = tariffPermitsRepository;
        _vehicleRepository = vehicleRepository;
    }

    [BindProperty]
    public TariffPermit TariffPermit { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tariffpermit =  await _tariffPermitsRepository.GetByIdAsync(id.Value);
        if (tariffpermit == null)
        {
            return NotFound();
        }
        TariffPermit = tariffpermit;
        ViewData["TypeId"] = new SelectList(await _vehicleRepository.GetAllTypesAsync(), "Id", "Type");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _tariffPermitsRepository.UpdateAsync(TariffPermit);

        return RedirectToPage("./Index");
    }
}
