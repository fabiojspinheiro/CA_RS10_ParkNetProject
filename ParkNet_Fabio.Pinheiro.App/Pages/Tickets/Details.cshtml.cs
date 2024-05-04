namespace ParkNet_Fabio.Pinheiro.App.Pages.Tickets;

[Authorize (Roles = "User")]
public class DetailsModel : PageModel
{
    private readonly TicketRepository _ticketRepository;
    private readonly TicketServices _ticketServices;
    private readonly TransactionsRepository _transactionsRepository;
    private readonly VehicleRepository _vehicleRepository;

    public DetailsModel(TicketRepository ticketRepository,
        TicketServices ticketServices,
        TransactionsRepository transactionsRepository,
        VehicleRepository vehicleRepository)
    {
        _ticketRepository = ticketRepository;
        _ticketServices = ticketServices;
        _transactionsRepository = transactionsRepository;
        _vehicleRepository = vehicleRepository;
    }

    public Ticket Ticket { get; set; } = default!;
    public ViewTicket ViewTicket { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticket = await _ticketRepository.GetByIdAsync(id.Value);

        if (ticket == null && ticket.Value != 0)
        {
            return NotFound();
        }
        else
        {
            // Update ticket
            ticket.End = DateTime.UtcNow;
            ticket.TariffTicketId = await _ticketServices.GetTariffByTimeAndVehicle(ticket.Start, ticket.End, ticket.VehicleId);
            ticket.Value = _ticketServices.GetTicketValue(ticket.TariffTicketId);
            Ticket = ticket;
            await _ticketRepository.UpdateAsync(ticket);

            //Create new transaction
            Transactions transaction = new Transactions()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                DateTime = DateTime.UtcNow,
                Value = -Ticket.Value
            };

            await _transactionsRepository.AddAsync(transaction);
            ViewTicket = _ticketRepository.GetViewTicket(ticket);
        }
        return Page();
    }
}
