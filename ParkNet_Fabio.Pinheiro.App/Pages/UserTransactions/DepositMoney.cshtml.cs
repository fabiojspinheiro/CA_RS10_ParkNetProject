namespace ParkNet_Fabio.Pinheiro.App.Pages.UserTransactions;

[Authorize (Roles = "User")]
public class DepositMoneyModel : PageModel
{ 
    private readonly TransactionsRepository _transactionsRepository;
    private readonly ValidationsServices _validationsServices;

    public DepositMoneyModel(TransactionsRepository transactionsRepository,
        ValidationsServices validationsServices)
    {
        _transactionsRepository = transactionsRepository;
        _validationsServices = validationsServices;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        bool isCustomerValid = await _validationsServices.IsIdAndCreditCardValidAsync(currentUser);
        
        if(isCustomerValid)
            return Page();
        else
            return NotFound();
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
        bool isCustomerValid = await _validationsServices.IsIdAndCreditCardValidAsync(currentUser);

        if (Transactions.Value < 0)
        {
            return NotFound();
        }
        else if (isCustomerValid)
        {
            Transactions.UserId = currentUser;
            Transactions.DateTime = DateTime.UtcNow;
            await _transactionsRepository.AddAsync(Transactions);
            return RedirectToPage("./Index");
        }
        else
        {
            return NotFound();
        }
    }
}
