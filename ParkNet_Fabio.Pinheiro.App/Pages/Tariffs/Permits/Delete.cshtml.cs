namespace ParkNet_Fabio.Pinheiro.App.Pages.Tariffs.Permits;

[Authorize(Roles = "Admin")]
public class DeleteModel : PageModel
{
    private readonly TariffPermitsRepository _tariffPermitsRepository;

    public DeleteModel(TariffPermitsRepository tariffPermitsRepository)
    {
        _tariffPermitsRepository = tariffPermitsRepository;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tariffpermit = await _tariffPermitsRepository.GetByIdAsync(id.Value);
        if (tariffpermit != null)
        {
            TariffPermit = tariffpermit;
            await _tariffPermitsRepository.DeleteAsync(TariffPermit);
        }

        return RedirectToPage("./Index");
    }
}
