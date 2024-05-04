namespace ParkNet_Fabio.Pinheiro.App.Pages.UserTransactions;

[Authorize(Roles = "User")]
public class WithdrawMoneyModel : PageModel
{ 
    private readonly TransactionsRepository _transactionsRepository;
    private readonly TransactionsServices _transactionsServices;
    private readonly PermitServices _permitServices;
    private readonly TicketServices _ticketServices;
    private readonly ValidationsServices _validationsServices;

    public WithdrawMoneyModel(TransactionsRepository transactionsRepository,
        TransactionsServices transactionsServices,
        PermitServices permitServices,
        TicketServices ticketServices,
        ValidationsServices validationsServices)
    {
        _transactionsRepository = transactionsRepository;
        _transactionsServices = transactionsServices;
        _permitServices = permitServices;
        _ticketServices = ticketServices;
        _validationsServices = validationsServices;
    }

    public async Task <IActionResult> OnGetAsync()
    {

        var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var transactions = await _transactionsRepository.GetAllByUserAsync(currentUser);

        bool validPermit = await _permitServices.ValidPermitAsync(currentUser);
        bool validTicket = await _ticketServices.ValidTicketAsync(currentUser);
        bool isCustomerValid = await _validationsServices.IsIdAndCreditCardValidAsync(currentUser);

        // Check if user have vehicle on park and cards valid
        if (transactions == null || validPermit == true || validTicket == true || isCustomerValid == false)
        {
            return NotFound();
        }
        return Page();
    }

    [BindProperty]
    public Transactions Transactions { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var _currentAmount = await _transactionsServices.GetBalanceAsync(currentUser);

        bool validPermit = await _permitServices.ValidPermitAsync(currentUser);
        bool validTicket = await _ticketServices.ValidTicketAsync(currentUser);
        bool isCustomerValid = await _validationsServices.IsIdAndCreditCardValidAsync(currentUser);

        // Check withdraw conditions
        if (Transactions.Value < 0 || Transactions.Value > _currentAmount || 
            validPermit == true || validTicket == true || isCustomerValid == false)
        {
            return NotFound();
        }

        Transactions.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Transactions.DateTime = DateTime.UtcNow;
        Transactions.Value = - Transactions.Value;  // Negative value
        await _transactionsRepository.AddAsync(Transactions);

        return RedirectToPage("./Index");
    }
}
