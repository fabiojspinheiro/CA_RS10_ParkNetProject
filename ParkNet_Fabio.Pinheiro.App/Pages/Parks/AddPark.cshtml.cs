namespace ParkNet_Fabio.Pinheiro.App.Pages;

[Authorize (Roles = "Admin")]
public class AddParkModel : PageModel
{
    private readonly ReadLayout _readLayout;
    private readonly ParkRepository _parkRepository;

    public AddParkModel(ReadLayout readLayout, ParkRepository parkRepository) 
    {
        _readLayout = readLayout;
        _parkRepository = parkRepository;
    }

    [BindProperty]
    public Park Park { get; set; } = default!;

    [BindProperty]
    public string Layout { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Save Park on Database
        await _parkRepository.AddParkAsync(Park);

        // Save Floor on Database
        var floors = _readLayout.ReadFloor(Layout, Park);

        foreach (var floor in floors)
        {
            await _parkRepository.AddFloorAsync(floor);
        }

        // Save Parking Spaces on Database
        foreach (var space in _readLayout.ReadParkingSpace(floors))
        {
            await _parkRepository.AddParkingSpaceAsync(space);
        }

        return RedirectToPage("./ShowParks");
    }
}
