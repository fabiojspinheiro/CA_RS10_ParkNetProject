using ParkNet_Fabio.Pinheiro.App.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddScoped<VehicleRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<ParkRepository>();
builder.Services.AddScoped<TransactionsRepository>();
builder.Services.AddScoped<PermitRepository>();
builder.Services.AddScoped<TicketRepository>();
builder.Services.AddScoped<TariffPermitsRepository>();
builder.Services.AddScoped<TariffTicketsRepository>();
builder.Services.AddScoped<ReadLayout>();
builder.Services.AddScoped<PermitServices>();
builder.Services.AddScoped<TransactionsServices>();
builder.Services.AddScoped<TicketServices>();
builder.Services.AddScoped<AdminServices>();
builder.Services.AddScoped<ValidationsServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Check or create roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = ["Admin", "User"];

    foreach (var role in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var email = builder.Configuration["AdminEmail"];
    var password = builder.Configuration["AdminPassword"];

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser() { UserName = email, Email = email, EmailConfirmed = true};

        await userManager.CreateAsync(user, password);


        await userManager.AddToRoleAsync(user, "Admin");
    }
}

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!ctx.VehicleTypes.Any())
    {
        ctx.VehicleTypes.Add(new VehicleType { Type = "Car"});
        ctx.VehicleTypes.Add(new VehicleType { Type = "Moto"});

        await ctx.SaveChangesAsync();
    }
}

app.Run();
