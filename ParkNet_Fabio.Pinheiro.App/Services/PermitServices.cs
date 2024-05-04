namespace ParkNet_Fabio.Pinheiro.App.Services;

public class PermitServices
{
    private readonly PermitRepository _permitRepository;

    public PermitServices(PermitRepository permitRepository) 
    {
        _permitRepository = permitRepository;
    }

    public DateTime GetEndDate(DateTime startDate, string period)
    {
        DateTime endDate = default;
        switch (period)
        {
            case "monthly":
                endDate = startDate.AddMonths(1);
                break;
            case "quarterly":
                endDate = startDate.AddMonths(3);
                break;
            case "half-yearly":
                endDate = startDate.AddMonths(6);
                break;
            case "annually":
                endDate = startDate.AddYears(1);
                break;
        }

        return endDate;        
    }

    public async Task<bool> ValidPermitAsync(string currentUser)
    {
        var result = false;
        var permits = await _permitRepository.GetAllByUserAsync(currentUser);

        foreach (var permit in permits)
        {
            if(permit.End <  DateTime.UtcNow)
            {
                result = false; 
            }
            else if(permit.End > DateTime.UtcNow)
            {
                result = true;
                break;
            }
        }

        return result;
    }
}
