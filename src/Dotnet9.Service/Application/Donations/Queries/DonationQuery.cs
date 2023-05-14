namespace Dotnet9.Service.Application.Donations.Queries;

public record DonationQuery : Query<DonationDto?>
{
    public override DonationDto? Result { get; set; }
}