namespace ParkNet_Fabio.Pinheiro.App.Pages.Tariffs.Tickets
{
    [Authorize (Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly TariffTicketsRepository _tariffTicketsRepository;

        public IndexModel(TariffTicketsRepository tariffTicketsRepository)
        {
            _tariffTicketsRepository = tariffTicketsRepository;
        }

        public IList<ViewTariffTicket> TariffTicket { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TariffTicket = await _tariffTicketsRepository.GetAllAsync();
        }
    }
}
