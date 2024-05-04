namespace ParkNet_Fabio.Pinheiro.App.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }
    public DbSet<Vehicle> Vehicle { get; set; }
    public DbSet<Park> Park { get; set; }
    public DbSet<Floor> Floor { get; set; }
    public DbSet<ParkingSpace> ParkingSpace { get; set; }
    public DbSet<Transactions> Transactions { get; set; }
    public DbSet<TariffPermit> TariffPermits { get; set; }
    public DbSet<TariffTicket> TariffTickets { get; set; }
    public DbSet<Permit> Permit { get; set; }
    public DbSet<Ticket> Ticket { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
