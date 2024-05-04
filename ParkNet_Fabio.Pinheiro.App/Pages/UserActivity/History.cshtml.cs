namespace ParkNet_Fabio.Pinheiro.App.Pages.UserActivity;

[Authorize (Roles = "User")]
public class HistoryModel : PageModel
{
    private readonly PermitRepository _permitRepository;
    private readonly TicketRepository _ticketRepository;

    public HistoryModel(PermitRepository permitRepository,
        TicketRepository ticketRepository)
    {
        _permitRepository = permitRepository;
        _ticketRepository = ticketRepository;
    }

    public IList<ViewPermit> Permit { get; set; } = default!;
    public IList<ViewTicket> Ticket { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Permit = await _permitRepository.GetAllByUserAsync(currentUserId);
        Ticket = await _ticketRepository.GetAllByUserAsync(currentUserId);
    }
}
