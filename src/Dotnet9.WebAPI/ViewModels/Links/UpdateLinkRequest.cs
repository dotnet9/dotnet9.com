namespace Dotnet9.WebAPI.ViewModels.Links;

public record UpdateLinkRequest
{
    public int SequenceNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LinkKind Kind { get; set; }
}