namespace Dotnet9.DomainCommons.Models;

public interface IEntity
{
    public Guid Id { get; }
    object[] GetKeys();
}