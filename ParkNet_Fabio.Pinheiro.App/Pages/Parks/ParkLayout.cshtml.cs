namespace ParkNet_Fabio.Pinheiro.App.Pages.Parks;

[Authorize]
public class ParkLayoutModel : PageModel
{
    private readonly ParkRepository _parkRepository;

    public ParkLayoutModel(ParkRepository parkRepository)
    {
        _parkRepository = parkRepository;
    }

    [BindProperty]
    public Park Park { get; set; } = default!;
    public List<ViewFloor> Floors { get; set; }
    public List<ViewParkingSpace> Spaces { get; set; }


    public async Task<IActionResult> OnGetAsync(int? id)
    {
        var park = await _parkRepository.GetParkByIdAsync(id.Value);

        if (park == null)
        {
            return NotFound();
        }

        Park = park;
        Floors = await _parkRepository.GetAllFloorsByParkIdAsync(Park.Id);
        Spaces = await _parkRepository.GetAllSpacesByParkIdAsync(Park.Id);

        return Page();
    }
}
