using System;
using System.Diagnostics.CodeAnalysis;
using Dotnet9.Albums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Contacts;

public class Contact : FullAuditedAggregateRoot<Guid>
{
    private Contact()
    {
    }


    internal Contact(
        Guid id,
        [NotNull] string name,
        [NotNull] string email,
        [NotNull] string subject,
        [NotNull] string message) : base(id)
    {
        SetName(name);
        SetEmail(email);
        SetSubject(subject);
        SetMessage(message);
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), ContactConsts.MaxNameLength);
    }

    private void SetEmail([NotNull] string email)
    {
        Email = Check.NotNullOrWhiteSpace(email, nameof(email), ContactConsts.MaxEmailLength);
    }

    private void SetSubject([NotNull] string subject)
    {
        Subject = Check.NotNullOrWhiteSpace(subject, nameof(subject), ContactConsts.MaxSubjectLength);
    }

    private void SetMessage([NotNull] string message)
    {
        Message = Check.NotNullOrWhiteSpace(message, nameof(message), ContactConsts.MaxMessageLength);
    }

    [NotNull] public string Name { get; set; }

    [NotNull] public string Email { get; set; }

    [NotNull] public string Subject { get; set; }

    [NotNull] public string Message { get; set; }
}