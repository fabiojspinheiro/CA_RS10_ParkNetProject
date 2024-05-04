namespace ParkNet_Fabio.Pinheiro.App.Pages.Tariffs.Permits;

[Authorize (Roles = "Admin")]
public class CreateModel : PageModel
{
    private readonly TariffPermitsRepository _tariffPermitsRepository;
    private readonly VehicleRepository _vehicleRepository;

    public CreateModel(TariffPermitsRepository tariffPermitsRepository,
        VehicleRepository vehicleRepository)
    {
        _tariffPermitsRepository = tariffPermitsRepository;
        _vehicleRepository = vehicleRepository;
    }
    
    public List<SelectListItem> Period {  get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        ViewData["TypeId"] = new SelectList(await _vehicleRepository.GetAllTypesAsync(), "Id", "Type");

        Period = new List<SelectListItem>
        {
            new SelectListItem {Text = "Monthly", Value = "monthly"},
            new SelectListItem {Text = "Quarterly", Value = "quarterly"},
            new SelectListItem {Text = "Half-Yearly", Value = "half-yearly"},
            new SelectListItem {Text = "Annually", Value = "annually"}
        };

        return Page();
    }

    [BindProperty]
    public TariffPermit TariffPermit { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _tariffPermitsRepository.AddAsync(TariffPermit);

        return RedirectToPage("./Index");
    }
}
