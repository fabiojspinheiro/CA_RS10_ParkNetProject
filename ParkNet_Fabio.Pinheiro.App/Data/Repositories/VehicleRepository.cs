namespace ParkNet_Fabio.Pinheiro.App.Data.Repositories;

public class VehicleRepository
{
    private ApplicationDbContext _ctx;

    public VehicleRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<ViewVehicle>> GetAllByUserAsync(string currentUserId)
    {       
        return await (from vehicle in _ctx.Vehicle
               join type in _ctx.VehicleTypes on vehicle.TypeId equals type.Id
               join user in _ctx.Users on vehicle.UserId equals user.Id
               where user.Id == currentUserId
               select new ViewVehicle
               {
                   Id = vehicle.Id,
                   LicensePlate = vehicle.LicensePlate,
                   VehicleType = type.Type,
               }).ToListAsync();
    }

    public async Task<Vehicle> AddAsync(Vehicle vehicle)
    {
        _ctx.Vehicle.Add(vehicle);
        await _ctx.SaveChangesAsync();

        return vehicle;
    }

    public async Task<Vehicle> GetByIdAsync(int id)
        => await _ctx.Vehicle.FirstOrDefaultAsync(v => v.Id == id);

    public async Task<Vehicle> GetByIdAsync(int id, string currentUserId) 
        => await _ctx.Vehicle.FirstOrDefaultAsync(v => v.Id == id && v.UserId == currentUserId);

    public async Task DeleteAsync(int id, string currentUserId)
    {
        var vehicle = await GetByIdAsync(id, currentUserId);
        if (vehicle != null)
        {
            _ctx.Vehicle.Remove(vehicle);
            await _ctx.SaveChangesAsync();
        }
        return;
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        _ctx.Attach(vehicle).State = EntityState.Modified;
        await _ctx.SaveChangesAsync();
        return;
    }

    public async Task<List<VehicleType>> GetAllTypesAsync() => await _ctx.VehicleTypes.ToListAsync();

    public async Task<bool> IsFromUserAsync(Vehicle vehicle, string currentUserId) 
        => await _ctx.Vehicle.AnyAsync(v => v.Id == vehicle.Id && v.UserId == currentUserId);
}
