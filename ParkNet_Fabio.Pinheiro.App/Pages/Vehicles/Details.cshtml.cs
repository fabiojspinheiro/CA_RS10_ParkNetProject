namespace ParkNet_Fabio.Pinheiro.App.Pages.Vehicles;

[Authorize (Roles = "User")]
public class DetailsModel : PageModel
{
    private readonly VehicleRepository _vehicleRepository;

    public DetailsModel(VehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

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
}
