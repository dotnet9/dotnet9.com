namespace Dotnet9.Caller.Services.Donations;

public class DonationService : HttpClientCallerBase
{
    protected override string Prefix { get; set; } = "/api/donations";
    protected override string BaseAddress { get; set; }

    public DonationService(IOptions<ServiceCallerOptions> option)
    {
        BaseAddress = option.Value.BaseAddress;
    }

    public async Task<DonationDto?> GetAllAsync()
    {
        return await Caller.GetAsync<DonationDto>("");
    }
}