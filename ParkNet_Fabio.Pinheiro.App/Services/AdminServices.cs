namespace ParkNet_Fabio.Pinheiro.App.Services;

public class AdminServices
{
    private readonly PermitRepository _permitRepository;
    private readonly TicketRepository _ticketRepository;

    public AdminServices(PermitRepository permitRepository, TicketRepository ticketRepository) 
    {
        _permitRepository = permitRepository;
        _ticketRepository = ticketRepository;
    }

    // List of all parking spaces sales
    public async Task<IList<ViewProfit>> GetProfitsAsync()
    {
        var sales = new List<ViewProfit>();

        foreach(var permit in await _permitRepository.GetAllProfitPermits()) 
        {
            sales.Add(permit);
        }

        foreach(var ticket in await _ticketRepository.GetAllProfitTickets())
        {
            sales.Add(ticket);
        }

        return sales;
    }

    // List of all parking spaces sales filtered by date
    public async Task<IList<ViewProfit>> GetProfitsAsync(DateTime start, DateTime end)
    {
        var sales = new List<ViewProfit>();

        foreach (var permit in await _permitRepository.GetAllProfitPermits(start, end))
        {
            sales.Add(permit);
        }

        foreach (var ticket in await _ticketRepository.GetAllProfitTickets(start, end))
        {
            sales.Add(ticket);
        }

        return sales;
    }
}
