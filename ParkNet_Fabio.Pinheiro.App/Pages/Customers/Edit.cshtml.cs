namespace ParkNet_Fabio.Pinheiro.App.Pages.Customers;

[Authorize (Roles = "User")]
public class EditModel : PageModel
{
    private readonly CustomerRepository _customerRepository;

    public EditModel(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [BindProperty]
    public Customer Customer { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _customerRepository.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        Customer = customer;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _customerRepository.UpdateAsync(Customer);

        return RedirectToPage("/Index");
    }
}
