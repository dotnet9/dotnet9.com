namespace Dotnet9.DomainCommons.Models;

public interface IHasDeletionTime
{
    DateTime? DeletionTime { get; }
}