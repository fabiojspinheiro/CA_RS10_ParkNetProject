namespace ParkNet_Fabio.Pinheiro.App.Pages.Vehicles;

[Authorize (Roles = "User")]
public class DeleteModel : PageModel
{
    private readonly VehicleRepository _vehicleRepository;

    public DeleteModel(VehicleRepository vehicleRepository)
    {
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
        else
        {
            Vehicle = vehicle;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _vehicleRepository.DeleteAsync(id.Value, currentUserId);

        return RedirectToPage("./Index");
    }
}
