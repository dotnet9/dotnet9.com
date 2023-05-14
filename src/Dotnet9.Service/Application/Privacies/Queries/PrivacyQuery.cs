namespace Dotnet9.Service.Application.Privacies.Queries;

public record PrivacyQuery : Query<PrivacyDto?>
{
    public override PrivacyDto? Result { get; set; }
}