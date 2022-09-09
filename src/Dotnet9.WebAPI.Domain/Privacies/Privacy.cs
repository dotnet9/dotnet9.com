namespace Dotnet9.WebAPI.Domain.Privacies;

public record Privacy : AggregateRootEntity
{
    private Privacy()
    {
    }

    internal Privacy(Guid id, string content)
    {
        Id = id;
        ChangeContent(content);
    }

    public string? Content { get; private set; }

    public Privacy ChangeContent(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), PrivacyConsts.MaxContentLength,
            PrivacyConsts.MinContentLength);
        return this;
    }
}