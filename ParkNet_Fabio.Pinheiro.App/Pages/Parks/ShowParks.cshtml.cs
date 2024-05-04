namespace ParkNet_Fabio.Pinheiro.App.Pages;

[Authorize]
public class ShowParkModel : PageModel
{
    private readonly ParkRepository _parkRepository;

    public ShowParkModel(ParkRepository parkRepository)
    {
        _parkRepository = parkRepository;
    }

    public List<Park> Parks { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Parks = await _parkRepository.GetAllParksAsync();
    }
}
