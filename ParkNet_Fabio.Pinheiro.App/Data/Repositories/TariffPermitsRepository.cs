namespace ParkNet_Fabio.Pinheiro.App.Data.Repositories;

public class TariffPermitsRepository
{
    private readonly ApplicationDbContext _ctx;
    private readonly VehicleRepository _vehicleRepository;

    public TariffPermitsRepository(ApplicationDbContext ctx,
        VehicleRepository vehicleRepository)
    {
        _ctx = ctx;
        _vehicleRepository = vehicleRepository;
    }

    public async Task<TariffPermit> AddAsync(TariffPermit tariffPermit)
    {
        _ctx.TariffPermits.Add(tariffPermit);
        await _ctx.SaveChangesAsync();

        return tariffPermit;
    }

    public async Task DeleteAsync(TariffPermit tariffPermit)
    {
        _ctx.TariffPermits.Remove(tariffPermit);
        await _ctx.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(TariffPermit tariffPermit)
    {
        _ctx.Attach(tariffPermit).State = EntityState.Modified;
        await _ctx.SaveChangesAsync();
        return;
    }

    public async Task<IList<ViewTariffPermit>> GetAllAsync()
    {
        return await(from tariffPermit in _ctx.TariffPermits
                     join vehicleType in _ctx.VehicleTypes on tariffPermit.TypeId equals vehicleType.Id
                     select new ViewTariffPermit
                     {
                         Id = tariffPermit.Id,
                         Type = vehicleType.Type,
                         Period = tariffPermit.Period,
                         Value = tariffPermit.Value
                     }).ToListAsync();
    }

    public async Task<TariffPermit> GetByIdAsync(int id) => await _ctx.TariffPermits.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<TariffPermit> GetTariffPermitAsync(string period, int vehicleId)
    {
        // Get vehicle to validate type
        Vehicle vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);

        return _ctx.TariffPermits.FirstOrDefault(t => t.Period == period && t.TypeId == vehicle.TypeId);
    }

}
