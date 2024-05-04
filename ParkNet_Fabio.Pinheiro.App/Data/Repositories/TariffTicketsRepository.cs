namespace ParkNet_Fabio.Pinheiro.App.Data.Repositories;

public class TariffTicketsRepository
{
    private readonly ApplicationDbContext _ctx;


    public TariffTicketsRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<TariffTicket> AddAsync(TariffTicket tariffTicket)
    {
        _ctx.TariffTickets.Add(tariffTicket);
        await _ctx.SaveChangesAsync();

        return tariffTicket;
    }

    public async Task DeleteAsync(TariffTicket tariffTicket)
    {
        _ctx.TariffTickets.Remove(tariffTicket);
        await _ctx.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(TariffTicket tariffTicket)
    {
        _ctx.Attach(tariffTicket).State = EntityState.Modified;
        await _ctx.SaveChangesAsync();
        return;
    }

    public async Task<IList<ViewTariffTicket>> GetAllAsync()
    {
        return await (from tariffTicket in _ctx.TariffTickets
                      join vehicleType in _ctx.VehicleTypes on tariffTicket.TypeId equals vehicleType.Id
                      select new ViewTariffTicket
                      {
                          Id = tariffTicket.Id,
                          Type = vehicleType.Type,
                          Start = tariffTicket.Start,
                          End = tariffTicket.End,
                          Value = tariffTicket.Value
                      }).ToListAsync();
    } 

    public async Task<TariffTicket> GetByIdAsync(int id) => await _ctx.TariffTickets.FirstOrDefaultAsync(t => t.Id == id);

    public TariffTicket GetTariffTicketById(int id) => _ctx.TariffTickets.FirstOrDefault(t => t.Id == id);

    public TariffTicket GetFirstTariffTicket() => _ctx.TariffTickets.FirstOrDefault();

    public async Task<List<TariffTicket>> GetAllTariffsAsync() => await _ctx.TariffTickets.ToListAsync();

}
