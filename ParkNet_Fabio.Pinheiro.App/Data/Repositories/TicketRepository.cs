namespace ParkNet_Fabio.Pinheiro.App.Data.Repositories;

public partial class TicketRepository
{
    private ApplicationDbContext _ctx;

    public TicketRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Ticket> AddAsync(Ticket ticket)
    {
        _ctx.Ticket.Add(ticket);
        await _ctx.SaveChangesAsync();

        return ticket;
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        _ctx.Attach(ticket).State = EntityState.Modified;
        await _ctx.SaveChangesAsync();
        return;
    }

    public async Task<List<Ticket>> GetAllAsync() => await _ctx.Ticket.ToListAsync();

    public async Task<Ticket> GetByIdAsync(int id) => await _ctx.Ticket.FirstOrDefaultAsync(b => b.Id == id);

    public async Task<List<ViewTicket>> GetAllByUserAsync(string currentUserId)
    {
        return await (from ticket in _ctx.Ticket
                      join vehicle in _ctx.Vehicle on ticket.VehicleId equals vehicle.Id
                      join parkingSpace in _ctx.ParkingSpace on ticket.ParkingSpaceId equals parkingSpace.Id
                      join floor in _ctx.Floor on parkingSpace.FloorId equals floor.Id
                      join park in _ctx.Park on floor.ParkId equals park.Id
                      join tariff in _ctx.TariffTickets on ticket.TariffTicketId equals tariff.Id
                      join user in _ctx.Users on vehicle.UserId equals user.Id
                      where user.Id == currentUserId
                      select new ViewTicket
                      {
                          Id = ticket.Id,
                          LicensePlate = vehicle.LicensePlate,
                          Park = park.Name,
                          ParkingSpace = parkingSpace.Name,
                          Start = ticket.Start,
                          End = ticket.End,
                          Value = ticket.Value
                      }).ToListAsync();
    }

    public async Task<List<ViewTicket>> GetOpenTicketsByUserAsync(string currentUserId)
    {
        return await (from ticket in _ctx.Ticket
                      join vehicle in _ctx.Vehicle on ticket.VehicleId equals vehicle.Id
                      join parkingSpace in _ctx.ParkingSpace on ticket.ParkingSpaceId equals parkingSpace.Id
                      join floor in _ctx.Floor on parkingSpace.FloorId equals floor.Id
                      join park in _ctx.Park on floor.ParkId equals park.Id
                      join tariff in _ctx.TariffTickets on ticket.TariffTicketId equals tariff.Id
                      join user in _ctx.Users on vehicle.UserId equals user.Id
                      where user.Id == currentUserId && ticket.Value == 0
                      select new ViewTicket
                      {
                          Id = ticket.Id,
                          LicensePlate = vehicle.LicensePlate,
                          Park = park.Name,
                          ParkingSpace = parkingSpace.Name,
                          Start = ticket.Start,
                          End = ticket.End,
                          Value = ticket.Value
                      }).ToListAsync();
    }

    public ViewTicket GetViewTicket(Ticket t)
    {
        return (from ticket in _ctx.Ticket
                join vehicle in _ctx.Vehicle on ticket.VehicleId equals vehicle.Id
                join parkingSpace in _ctx.ParkingSpace on ticket.ParkingSpaceId equals parkingSpace.Id
                join floor in _ctx.Floor on parkingSpace.FloorId equals floor.Id
                join park in _ctx.Park on floor.ParkId equals park.Id
                join tariff in _ctx.TariffTickets on ticket.TariffTicketId equals tariff.Id
                join user in _ctx.Users on vehicle.UserId equals user.Id
                where ticket.Id == t.Id
                select new ViewTicket
                {
                    Id = ticket.Id,
                    LicensePlate = vehicle.LicensePlate,
                    Park = park.Name,
                    ParkingSpace = parkingSpace.Name,
                    Start = ticket.Start,
                    End = ticket.End,
                    Value = ticket.Value

                }).FirstOrDefault();
    }

    public async Task<IList<ViewProfit>> GetAllProfitTickets()
    {
        return await (from user in _ctx.Users
                      join vehicle in _ctx.Vehicle on user.Id equals vehicle.UserId
                      join ticket in _ctx.Ticket on vehicle.Id equals ticket.VehicleId
                      select new ViewProfit
                      {
                          ServiceType = "Ticket",
                          UserName = user.UserName,
                          StartDate = ticket.Start,
                          EndDate = ticket.End,
                          Value = ticket.Value
                      }).ToListAsync();
    }

    public async Task<IList<ViewProfit>> GetAllProfitTickets(DateTime start, DateTime end)
    {
        return await (from user in _ctx.Users
                      join vehicle in _ctx.Vehicle on user.Id equals vehicle.UserId
                      join ticket in _ctx.Ticket on vehicle.Id equals ticket.VehicleId
                      where ticket.Start >= start && ticket.Start <= end
                      select new ViewProfit
                      {
                          ServiceType = "Ticket",
                          UserName = user.UserName,
                          StartDate = ticket.Start,
                          EndDate = ticket.End,
                          Value = ticket.Value
                      }).ToListAsync();
    }

}
