namespace ParkNet_Fabio.Pinheiro.App.Pages.Vehicles;

[Authorize]
public class IndexModel : PageModel
{
    private readonly VehicleRepository _vehicleRepository;

    public IndexModel(VehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public IList<ViewVehicle> Vehicle { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Vehicle = await _vehicleRepository.GetAllByUserAsync(currentUserId);
        
    }
}
