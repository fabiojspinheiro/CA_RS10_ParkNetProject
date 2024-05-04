namespace ParkNet_Fabio.Pinheiro.App.Pages.Admin;

[Authorize (Roles = "Admin")]
public class ProfitModel : PageModel
{
    private readonly AdminServices _adminServices;

    public ProfitModel(AdminServices adminServices)
    {
        _adminServices = adminServices;
    }

    [BindProperty]
    public DateTime Start { get; set; }
    [BindProperty]
    public DateTime End { get; set; }

    public IList<ViewProfit> Data { get; set; } = default!;
    public double ProfitValue {  get; set; }

    public async Task OnGetAsync()
    {
        Data = await _adminServices.GetProfitsAsync();
        Data = Data.OrderBy(d => d.StartDate).ToList();

        foreach (var d in Data)
        {
            ProfitValue += d.Value;
        }   
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Data = await _adminServices.GetProfitsAsync(Start, End);
        Data = Data.OrderBy(d => d.StartDate).ToList();

        foreach (var d in Data)
        {
            ProfitValue += d.Value;
        }

        return Page();
    }
}
