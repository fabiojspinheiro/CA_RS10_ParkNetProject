namespace ParkNet_Fabio.Pinheiro.App.Services;

public class TicketServices
{
    private readonly TicketRepository _ticketRepository;
    private readonly TariffTicketsRepository _tariffTicketsRepository;
    private readonly VehicleRepository _vehicleRepository;

    public TicketServices(TicketRepository ticketRepository,
        TariffTicketsRepository tariffTicketsRepository,
        VehicleRepository vehicleRepository) 
    {
        _ticketRepository = ticketRepository;
        _tariffTicketsRepository = tariffTicketsRepository;
        _vehicleRepository = vehicleRepository;
    }

    public async Task<int> GetTariffByTime(DateTime start, DateTime end)
    {
        int tariffId = 0;
        TimeSpan period = end.Subtract(start);
        List<TariffTicket> tariffTickets = await _tariffTicketsRepository.GetAllTariffsAsync();

        foreach (var tariff in tariffTickets)
        {
            if (period < tariff.End - tariff.Start)
            {
                tariffId = tariff.Id;
                break;
            }
        }

        return tariffId;
    }

    public async Task<int> GetTariffByTimeAndVehicle(DateTime start, DateTime end, int vehicleId)
    {
        int tariffId = default;
        TimeSpan period = end.Subtract(start);
        Vehicle vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
        List<TariffTicket> tariffTickets = await _tariffTicketsRepository.GetAllTariffsAsync();

        foreach (var tariff in tariffTickets)
        {
            if(vehicle.TypeId == tariff.TypeId)
            {
                if(period < tariff.End - tariff.Start)
                {
                    tariffId = tariff.Id; 
                    break;
                }
            }
        }
        return tariffId;
    }

    public double GetTicketValue(int tariffId)
    {
        return _tariffTicketsRepository.GetTariffTicketById(tariffId).Value;
    }

    public async Task<bool> ValidTicketAsync(string currentUser)
    {
        var result = false;
        var tickets = await _ticketRepository.GetAllByUserAsync(currentUser);

        foreach (var ticket in tickets)
        {
            if (ticket.End == DateTime.Parse("0001-01-01 00:00:00.0000000"))
            {
                result = true;
                break;
            }
            else
            {
                result = false;
            }
        }
        return result;
    }

}
