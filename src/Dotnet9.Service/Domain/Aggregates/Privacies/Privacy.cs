﻿namespace Dotnet9.Service.Domain.Aggregates.Privacies;

public class Privacy : FullAggregateRoot<Guid, int>
{
    private Privacy()
    {
    }

    internal Privacy(Guid id, string content)
    {
        Id = id;
        ChangeContent(content);
    }

    public string Content { get; private set; } = null!;

    public Privacy ChangeContent(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), PrivacyConsts.MaxContentLength,
            PrivacyConsts.MinContentLength);
        return this;
    }
}