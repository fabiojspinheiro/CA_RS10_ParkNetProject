namespace ParkNet_Fabio.Pinheiro.App.Pages.Tariffs.Permits;

public class IndexModel : PageModel
{
    private readonly TariffPermitsRepository _tariffPermitsRepository;

    public IndexModel(TariffPermitsRepository tariffPermitsRepository)
    {
        _tariffPermitsRepository = tariffPermitsRepository;
    }

    public IList<ViewTariffPermit> TariffPermit { get;set; } = default!;

    public async Task OnGetAsync()
    {
        TariffPermit = await _tariffPermitsRepository.GetAllAsync();
    }
}
