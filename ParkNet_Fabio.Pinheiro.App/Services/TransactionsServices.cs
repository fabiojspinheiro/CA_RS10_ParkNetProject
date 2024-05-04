namespace ParkNet_Fabio.Pinheiro.App.Services;

public class TransactionsServices
{

    private readonly TransactionsRepository _transactionsRepository;

    public TransactionsServices(TransactionsRepository transactionsRepository) 
    {
        _transactionsRepository = transactionsRepository;
    }

    public async Task <double> GetBalanceAsync(string currentUser)
    {
        var transactions = await _transactionsRepository.GetAllByUserAsync(currentUser);

        double balance = 0;
        foreach (var transaction in transactions)
        {
            balance += transaction.Value;
        }

        return balance;
    }
}
