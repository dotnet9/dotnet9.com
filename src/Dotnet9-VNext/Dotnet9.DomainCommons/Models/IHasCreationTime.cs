namespace Dotnet9.DomainCommons.Models;

public interface IHasCreationTime
{
    DateTime? CreationTime { get; }
}