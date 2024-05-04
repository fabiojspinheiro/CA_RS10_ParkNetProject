namespace ParkNet_Fabio.Pinheiro.App.Pages.Tickets;

[Authorize (Roles = "User")]
public class IndexModel : PageModel
{
    private readonly TicketRepository _ticketRepository;

    public IndexModel(TicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public IList<ViewTicket> Ticket { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Ticket = await _ticketRepository.GetOpenTicketsByUserAsync(currentUserId);
    }
}
