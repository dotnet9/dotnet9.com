namespace Dotnet9.Service.Application.Abouts.Queries;

public record AboutQuery : Query<AboutDto?>
{
    public override AboutDto? Result { get; set; }
}