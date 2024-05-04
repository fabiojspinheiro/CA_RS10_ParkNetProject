namespace ParkNet_Fabio.Pinheiro.App.Tests;

public class TariffTicketsTests : IDisposable
{
    private readonly ApplicationDbContext _ctx;
    private readonly TariffTicketsRepository _tariffTicketsRepository;

    public TariffTicketsTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Data Source=.\\;Initial Catalog=ParkNet_Fabio.Pinheiro_Tests;Integrated Security=True;MultipleActiveResultSets=true;Connection Timeout=360;TrustServerCertificate=True")
        .Options;

        _ctx = new ApplicationDbContext(options);
        _ctx.Database.EnsureDeleted();
        _ctx.Database.Migrate();

        _tariffTicketsRepository = new TariffTicketsRepository(_ctx);
    }


    [Fact]
    public async Task Check_TicketValue_ResultOK()
    {
        // Arrange
        _ctx.VehicleTypes.Add(new VehicleType { Type = "Car" });
        await _ctx.SaveChangesAsync();

        _ctx.TariffTickets.Add(new TariffTicket
        {
            TypeId = 1,
            Start = TimeOnly.Parse("00:00"),
            End = TimeOnly.Parse("00:15"),
            Value = 1
        });
        await _ctx.SaveChangesAsync();

        // Act
        var tariff = _tariffTicketsRepository.GetTariffTicketById(1);

        //Assert
        tariff.Value.Should().Be(1);
    }

    [Fact]
    public async Task Check_TicketValue_ResultNull()
    {
        // Arrange
        _ctx.VehicleTypes.Add(new VehicleType { Type = "Car" });
        await _ctx.SaveChangesAsync();

        _ctx.TariffTickets.Add(new TariffTicket
        {
            TypeId = 1,
            Start = TimeOnly.Parse("00:00"),
            End = TimeOnly.Parse("00:15"),
            Value = 1
        });
        await _ctx.SaveChangesAsync();

        // Act  
        var tariff = _tariffTicketsRepository.GetTariffTicketById(2);       // There is no ID 2

        //Assert
        tariff.Should().BeNull();
    }

    public void Dispose()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Dispose();
    }
}
