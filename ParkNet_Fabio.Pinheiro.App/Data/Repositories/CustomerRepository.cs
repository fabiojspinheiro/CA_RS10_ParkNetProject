namespace ParkNet_Fabio.Pinheiro.App.Data.Repositories;

public class CustomerRepository
{
    private ApplicationDbContext _ctx;

    public CustomerRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Customer> AddAsync(Customer customer)
    {
        _ctx.Customers.Add(customer);
        await _ctx.SaveChangesAsync();

        return customer;
    }

    public async Task UpdateAsync(Customer customer)
    {
        _ctx.Attach(customer).State = EntityState.Modified;
        await _ctx.SaveChangesAsync();
        return;
    }

    public async Task<Customer> GetCustomerByIdAsync(string id) => await _ctx.Customers.FirstOrDefaultAsync(c => c.UserId == id);

    public async Task<List<ViewCustomerBalance>> GetCustomersAndBalanceAsync()
    {
        return await (from user in _ctx.Users
                      join customer in _ctx.Customers on user.Id equals customer.UserId
                      select new ViewCustomerBalance
                      {
                          Id = user.Id,
                          UserName = user.UserName,
                          PhoneNumber = user.PhoneNumber,
                          Email = user.Email
                      }).ToListAsync();
    }
}
