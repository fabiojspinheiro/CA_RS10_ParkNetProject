namespace ParkNet_Fabio.Pinheiro.App.Pages.Parks;

[Authorize (Roles = "Admin")]
public class EditParkModel : PageModel
{
    private readonly ParkRepository _parkRepository;

    public EditParkModel(ParkRepository parkRepository)
    {
        _parkRepository = parkRepository;
    }

    [BindProperty]
    public Park Park { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var park =  await _parkRepository.GetParkByIdAsync(id.Value);
        if (park == null)
        {
            return NotFound();
        }
        Park = park;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _parkRepository.UpdateAsync(Park);

        return RedirectToPage("./ShowParks");
    }
}
