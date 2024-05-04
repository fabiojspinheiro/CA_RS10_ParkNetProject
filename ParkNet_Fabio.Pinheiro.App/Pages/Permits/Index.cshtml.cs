namespace ParkNet_Fabio.Pinheiro.App.Pages.Permits;

[Authorize]
public class IndexModel : PageModel
{
    private readonly PermitRepository _permitRepository;

    public IndexModel(PermitRepository permitRepository)
    {
        _permitRepository = permitRepository;
    }

    public IList<ViewPermit> Permit { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Permit = await _permitRepository.GetAllByUserAsync(currentUserId);
    }
}
