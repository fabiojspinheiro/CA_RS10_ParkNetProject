namespace ParkNet_Fabio.Pinheiro.App.Data.Repositories;

public class ParkRepository
{
    private ApplicationDbContext _ctx;

    public ParkRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Park> AddParkAsync(Park park)
    {
        _ctx.Park.Add(park);
        await _ctx.SaveChangesAsync();

        return park;
    }

    public async Task UpdateAsync(Park park)
    {
        _ctx.Attach(park).State = EntityState.Modified;
        await _ctx.SaveChangesAsync();
        return;
    }

    public async Task<Floor> AddFloorAsync(Floor floor)
    {
        _ctx.Floor.Add(floor);
        await _ctx.SaveChangesAsync();

        return floor;
    }

    public async Task<ParkingSpace> AddParkingSpaceAsync(ParkingSpace space)
    {
        _ctx.ParkingSpace.Add(space);
        await _ctx.SaveChangesAsync();

        return space;
    }

    public async Task<List<Park>> GetAllParksAsync() => await _ctx.Park.ToListAsync();

    public async Task<Park> GetParkByIdAsync(int id) => await _ctx.Park.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<ParkingSpace> GetSpaceByIdAsync(int id) => await _ctx.ParkingSpace.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<ViewFloor>> GetAllFloorsByParkIdAsync(int id)
    {
        return await (from floor in _ctx.Floor
                      join park in _ctx.Park on floor.ParkId equals park.Id
                      where park.Id == id
                      select new ViewFloor
                      {
                          Id = floor.Id,
                          Spaces = floor.RowOfSpaces
                      }).ToListAsync();
    }

    public async Task<List<ViewParkingSpace>> GetAllSpacesByParkIdAsync(int parkId)
    {
        return await (from space in _ctx.ParkingSpace
                      join floor in _ctx.Floor on space.FloorId equals floor.Id
                      join type in _ctx.VehicleTypes on space.TypeId equals type.Id
                      where floor.ParkId == parkId
                      select new ViewParkingSpace
                      {
                          Id = space.Id,
                          FloorId = floor.Id,
                          Name = space.Name,
                          Type = type.Type
                      }).ToListAsync();
    }
}
