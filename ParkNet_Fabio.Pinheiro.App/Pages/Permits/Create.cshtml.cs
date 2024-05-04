namespace ParkNet_Fabio.Pinheiro.App.Pages.Permits;

[Authorize (Roles = "User")]
public class CreateModel : PageModel
{
    private readonly ParkRepository _parkRepository;
    private readonly PermitServices _permitCalculations;
    private readonly PermitRepository _permitRepository;
    private readonly TransactionsRepository _transactionsRepository;
    private readonly VehicleRepository _vehicleRepository;
    private readonly TransactionsServices _transactionsServices;
    private readonly ValidationsServices _validationsServices;
    private readonly TariffPermitsRepository _tariffPermitsRepository;

    public CreateModel(ParkRepository parkRepository, 
                        PermitServices permitCalculations,
                        PermitRepository permitRepository,
                        TransactionsRepository transactionsRepository,
                        VehicleRepository vehicleRepository,
                        TransactionsServices transactionsServices,
                        ValidationsServices validationsServices,
                        TariffPermitsRepository tariffPermitsRepository)
    {
        _parkRepository = parkRepository;
        _permitCalculations = permitCalculations;
        _permitRepository = permitRepository;
        _transactionsRepository = transactionsRepository;
        _vehicleRepository = vehicleRepository;
        _transactionsServices = transactionsServices;
        _validationsServices = validationsServices;
        _tariffPermitsRepository = tariffPermitsRepository;
    }

    public List<SelectListItem> Period { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

        ViewData["ParkingSpaceId"] = 
            new SelectList(await _parkRepository.GetAllSpacesByParkIdAsync(id.Value), "Id", "Name");

        Period = new List<SelectListItem>
        {
            new SelectListItem {Text = "Monthly", Value = "monthly"},
            new SelectListItem {Text = "Quarterly", Value = "quarterly"},
            new SelectListItem {Text = "Half-Yearly", Value = "half-yearly"},
            new SelectListItem {Text = "Annually", Value = "annually"}
        };

        ViewData["VehicleId"] = 
            new SelectList(await _vehicleRepository.GetAllByUserAsync(currentUser), "Id", "LicensePlate");

        return Page();
    }

    [BindProperty]
    public Permit Permit { get; set; } = default!;

    [BindProperty]
    public string SelectedPeriod { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Update permit by user choices
        Permit.Start = DateTime.UtcNow;
        var tariffPermit = await _tariffPermitsRepository.GetTariffPermitAsync(SelectedPeriod, Permit.VehicleId);
        Permit.TariffPermitId = tariffPermit.Id;
        Permit.End = _permitCalculations.GetEndDate(Permit.Start, tariffPermit.Period);
        Permit.Value = tariffPermit.Value;

        // Validation conditions
        var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var currentAmount = await _transactionsServices.GetBalanceAsync(currentUser);
        bool isCompatible = await _validationsServices.IsVehicleCompatible(Permit.VehicleId, Permit.ParkingSpaceId);
        bool isAvailable = await _validationsServices.IsSpaceAvailableAsync(Permit.ParkingSpaceId);
        bool isCustomerValid = await _validationsServices.IsIdAndCreditCardValidAsync(currentUser);

        // Save if is valid
        if (Permit.Value <= currentAmount && isCompatible && isAvailable && isCustomerValid)
        {          
            Transactions transaction = new Transactions()   //Create new transaction
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                DateTime = DateTime.UtcNow,
                Value = -Permit.Value
            };

            await _transactionsRepository.AddAsync(transaction);
            await _permitRepository.AddAsync(Permit);
        }
        else
        {
            return NotFound();
        }

        return RedirectToPage("./Index");
    }
}
