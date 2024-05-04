namespace ParkNet_Fabio.Pinheiro.App.Pages.Vehicles;

[Authorize (Roles = "User")]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly VehicleRepository _vehicleRepository;

    public EditModel(ApplicationDbContext context, VehicleRepository vehicleRepository)
    {
        _context = context;
        _vehicleRepository = vehicleRepository;
    }

    [BindProperty]
    public Vehicle Vehicle { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var vehicle = await _vehicleRepository.GetByIdAsync(id.Value, currentUserId);
        if (vehicle == null)
        {
            return NotFound();
        }
        Vehicle = vehicle;
        ViewData["TypeId"] = new SelectList(await _vehicleRepository.GetAllTypesAsync(), "Id", "Type");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var vehicleIsFromUser = await _vehicleRepository.IsFromUserAsync(Vehicle, currentUserId);

        if (!vehicleIsFromUser)
        {
            return NotFound();
        }

        await _vehicleRepository.UpdateAsync(Vehicle);

        return RedirectToPage("./Index");
    }
}
