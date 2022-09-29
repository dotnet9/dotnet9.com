namespace Dotnet9.DomainCommons.Models;

public interface IHasModificationTime
{
    DateTime? LastModificationTime { get; }
}