namespace ParkNet_Fabio.Pinheiro.App.Pages.Admin;

[Authorize (Roles = "Admin")]
public class AllCustomersModel : PageModel
{
    private readonly CustomerRepository _customerRepository;
    private readonly TransactionsServices _transactionsServices;

    public AllCustomersModel(CustomerRepository customerRepository,
        TransactionsServices transactionsServices)
    {
        _customerRepository = customerRepository;
        _transactionsServices = transactionsServices;
    }

    public IList<ViewCustomerBalance> CustomerBalance { get; set; } = default!;

    public async Task OnGetAsync()
    {
        CustomerBalance = await _customerRepository.GetCustomersAndBalanceAsync();
        foreach (var customer in CustomerBalance)
        {
            customer.Balance = await _transactionsServices.GetBalanceAsync(customer.Id);
        }
    }
}
