namespace ParkNet_Fabio.Pinheiro.App.Data.Repositories;

public class TransactionsRepository
{
    private ApplicationDbContext _ctx;

    public TransactionsRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Transactions>> GetAllByUserAsync(string currentUserId)
    {
        return await (from transactions in _ctx.Transactions                     
                      join user in _ctx.Users on transactions.UserId equals user.Id
                      where user.Id == currentUserId
                      select new Transactions
                      {
                          Id = transactions.Id,
                          UserId = transactions.UserId,
                          DateTime = transactions.DateTime,
                          Value = transactions.Value,
                      }).ToListAsync();
    }

    public async Task<Transactions> AddAsync(Transactions transaction)
    {
        _ctx.Transactions.Add(transaction);
        await _ctx.SaveChangesAsync();

        return transaction;
    }

}
