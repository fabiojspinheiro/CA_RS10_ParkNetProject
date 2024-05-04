namespace ParkNet_Fabio.Pinheiro.App.Pages.Vehicles;

[Authorize(Roles = "User")]
public class CreateModel : PageModel
{
    private readonly VehicleRepository _vehicleRepository;

    public CreateModel(VehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        ViewData["TypeId"] = new SelectList(await _vehicleRepository.GetAllTypesAsync(), "Id", "Type");
        return Page();
    }

    [BindProperty]
    public Vehicle Vehicle { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Vehicle.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _vehicleRepository.AddAsync(Vehicle);

        return RedirectToPage("./Index");
    }
}
