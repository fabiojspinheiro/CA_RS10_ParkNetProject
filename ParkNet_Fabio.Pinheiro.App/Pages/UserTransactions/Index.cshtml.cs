namespace ParkNet_Fabio.Pinheiro.App.Pages.UserTransactions;

[Authorize(Roles = "User")]
public class IndexModel : PageModel
{
    private readonly TransactionsRepository _transactionsRepository;
    private readonly TransactionsServices _transactionsServices;

    public IndexModel(TransactionsRepository transactionsRepository, TransactionsServices transactionsServices)
    {
        _transactionsRepository = transactionsRepository;
        _transactionsServices = transactionsServices;
    }

    public IList<Transactions> Transactions { get;set; } = default!;
    public double Balance { get; set; }

    public async Task OnGetAsync()
    {
        var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Transactions = await _transactionsRepository.GetAllByUserAsync(currentUser);

        Balance = await _transactionsServices.GetBalanceAsync(currentUser);
    }
}
