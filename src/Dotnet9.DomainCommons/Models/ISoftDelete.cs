namespace Dotnet9.DomainCommons.Models;

public interface ISoftDelete
{
    bool IsDeleted { get; }
    void SoftDelete();
}