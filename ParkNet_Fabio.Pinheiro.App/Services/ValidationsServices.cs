namespace ParkNet_Fabio.Pinheiro.App.Services;

public class ValidationsServices
{
    private readonly VehicleRepository _vehicleRepository;
    private readonly ParkRepository _parkRepository;
    private readonly PermitRepository _permitRepository;
    private readonly TicketRepository _ticketRepository;
    private readonly CustomerRepository _customerRepository;

    public ValidationsServices(VehicleRepository vehicleRepository,
        ParkRepository parkRepository,
        PermitRepository permitRepository,
        TicketRepository ticketRepository,
        CustomerRepository customerRepository) 
    {
        _vehicleRepository = vehicleRepository;
        _parkRepository = parkRepository;
        _ticketRepository = ticketRepository;
        _customerRepository = customerRepository;
        _permitRepository = permitRepository;
    }

    public async Task<bool> IsVehicleCompatible(int vehicleId, int spaceId)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
        var parkingSpace = await _parkRepository.GetSpaceByIdAsync(spaceId);

        return vehicle.TypeId == parkingSpace.TypeId;
    }

    public async Task<bool> IsSpaceAvailableAsync(int spaceId)
    {
        var permits = await _permitRepository.GetAllAsync();
        var tickets = await _ticketRepository.GetAllAsync();
        bool validPermit = false;
        bool validTickets = false;

        foreach(var permit in permits)
        {
            if(permit.ParkingSpaceId == spaceId && permit.End > DateTime.UtcNow)
            {
                validPermit = true;
                break;
            }
        }

        foreach(var ticket in tickets)
        {
            if(ticket.ParkingSpaceId == spaceId && ticket.Value == 0)
            {
                validTickets = true;
                break;
            }
        }

        if(validPermit == true || validTickets == true)
            return false;
        else
            return true;
    }

    public async Task<bool> IsIdAndCreditCardValidAsync(string currentUser)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(currentUser);
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

        if (customer.DLNExpDate < currentDate || customer.CreditCardExpDate < currentDate)
            return false;
        else
            return true;
    }
}
