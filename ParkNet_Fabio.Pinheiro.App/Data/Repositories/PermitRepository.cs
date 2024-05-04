namespace ParkNet_Fabio.Pinheiro.App.Data.Repositories;

public class PermitRepository
{
    private ApplicationDbContext _ctx;

    public PermitRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Permit> AddAsync(Permit permit)
    {
        _ctx.Permit.Add(permit);
        await _ctx.SaveChangesAsync();

        return permit;
    }

    public async Task<List<Permit>> GetAllAsync() => await _ctx.Permit.ToListAsync();

    public async Task<List<ViewPermit>> GetAllByUserAsync(string currentUserId)
    {
        return await (from permit in _ctx.Permit
                      join vehicle in _ctx.Vehicle on permit.VehicleId equals vehicle.Id
                      join parkingSpace in _ctx.ParkingSpace on permit.ParkingSpaceId equals parkingSpace.Id
                      join floor in _ctx.Floor on parkingSpace.FloorId equals floor.Id
                      join park in _ctx.Park on floor.ParkId equals park.Id
                      join tariff in _ctx.TariffPermits on permit.TariffPermitId equals tariff.Id
                      join user in _ctx.Users on vehicle.UserId equals user.Id
                      where user.Id == currentUserId
                      select new ViewPermit
                      {
                          Id = permit.Id,
                          LicensePlate = vehicle.LicensePlate,
                          Park = park.Name,
                          Period = tariff.Period,
                          Start = permit.Start,
                          End = permit.End,
                          Value = permit.Value
                      }).ToListAsync();
    }

    public async Task<IList<ViewProfit>> GetAllProfitPermits()
    {
        return await (from user in _ctx.Users
                      join vehicle in _ctx.Vehicle on user.Id equals vehicle.UserId
                      join permit in _ctx.Permit on vehicle.Id equals permit.VehicleId
                      select new ViewProfit
                      {
                          ServiceType = "Permit",
                          UserName = user.UserName,
                          StartDate = permit.Start,
                          EndDate = permit.End,
                          Value = permit.Value
                      }).ToListAsync();
    }

    public async Task<IList<ViewProfit>> GetAllProfitPermits(DateTime start, DateTime end)
    {
        return await (from user in _ctx.Users
                      join vehicle in _ctx.Vehicle on user.Id equals vehicle.UserId
                      join permit in _ctx.Permit on vehicle.Id equals permit.VehicleId
                      where permit.Start >= start && permit.Start <= end
                      select new ViewProfit
                      {
                          ServiceType = "Permit",
                          UserName = user.UserName,
                          StartDate = permit.Start,
                          EndDate = permit.End,
                          Value = permit.Value
                      }).ToListAsync();
    }
}
