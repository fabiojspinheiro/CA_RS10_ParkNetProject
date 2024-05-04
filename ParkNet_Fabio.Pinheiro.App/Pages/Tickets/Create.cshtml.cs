namespace ParkNet_Fabio.Pinheiro.App.Pages.Tickets;

[Authorize(Roles = "User")]
public class CreateModel : PageModel
{
    private readonly ParkRepository _parkRepository;
    private readonly TicketRepository _ticketRepository;
    private readonly VehicleRepository _vehicleRepository;
    private readonly TransactionsServices _transactionsServices;
    private readonly ValidationsServices _validationsServices;
    private readonly TariffTicketsRepository _tariffTicketsRepository;

    public CreateModel(ParkRepository parkRepository,
        TicketRepository ticketRepository,
        VehicleRepository vehicleRepository,
        TransactionsServices transactionsServices,
        ValidationsServices validationsServices,
        TariffTicketsRepository tariffTicketsRepository)
    {

        _parkRepository = parkRepository;
        _ticketRepository = ticketRepository;
        _vehicleRepository = vehicleRepository;
        _transactionsServices = transactionsServices;
        _validationsServices = validationsServices;
        _tariffTicketsRepository = tariffTicketsRepository;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var currentAmount = await _transactionsServices.GetBalanceAsync(currentUser);
        bool isCustomerValid = await _validationsServices.IsIdAndCreditCardValidAsync(currentUser);

        if (currentAmount > 0 && isCustomerValid)   //Check if user have money and Id card and credit card valid
        {
            ViewData["ParkingSpaceId"] =
            new SelectList(await _parkRepository.GetAllSpacesByParkIdAsync(id.Value), "Id", "Name");
            ViewData["TariffTicketId"] =
                new SelectList(await _tariffTicketsRepository.GetAllTariffsAsync(), "Id", "Id");
            ViewData["VehicleId"] =
                    new SelectList(await _vehicleRepository.GetAllByUserAsync(currentUser), "Id", "LicensePlate");
            return Page();
        }
        else
        {
            return NotFound();
        }
    }

    [BindProperty]
    public Ticket Ticket { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var currentAmount = await _transactionsServices.GetBalanceAsync(currentUser);
        bool isCustomerValid = await _validationsServices.IsIdAndCreditCardValidAsync(currentUser);

        if (!ModelState.IsValid && currentAmount > 0 && isCustomerValid)
        {
            return Page();
        }

        try
        {
            bool isCompatible = await _validationsServices.IsVehicleCompatible(Ticket.VehicleId, Ticket.ParkingSpaceId);
            bool isAvailable = await _validationsServices.IsSpaceAvailableAsync(Ticket.ParkingSpaceId);

            // Check if vehicle is compatible, if parking space is available, if custumer cards are valid and current amount > 0
            if (isCompatible && isAvailable && isCustomerValid && currentAmount > 0)
            {

                Ticket.Start = DateTime.Now;
                Ticket.TariffTicketId = _tariffTicketsRepository.GetFirstTariffTicket().Id;

                await _ticketRepository.AddAsync(Ticket);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception) 
        {
            return NotFound();
        }
        
        return RedirectToPage("./Index");
    }
}
