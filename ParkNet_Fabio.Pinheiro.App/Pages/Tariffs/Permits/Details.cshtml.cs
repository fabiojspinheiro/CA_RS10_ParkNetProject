namespace ParkNet_Fabio.Pinheiro.App.Pages.Tariffs.Permits;

[Authorize (Roles = "Admin")]
public class DetailsModel : PageModel
{
    private readonly TariffPermitsRepository _tariffPermitsRepository;

    public DetailsModel(TariffPermitsRepository tariffPermitsRepository)
    {
        _tariffPermitsRepository = tariffPermitsRepository;
    }

    public TariffPermit TariffPermit { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tariffpermit = await _tariffPermitsRepository.GetByIdAsync(id.Value);
        if (tariffpermit == null)
        {
            return NotFound();
        }
        else
        {
            TariffPermit = tariffpermit;
        }
        return Page();
    }
}
