using Dotnet9.DomainCommons.Models;

namespace IdentityService.Domain.Entities;

public class User : IdentityUser<Guid>, IHasCreationTime, IHasDeletionTime, ISoftDelete
{
    public User(string userName) : base(userName)
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public DateTime? CreationTime { get; init; }

    public DateTime? DeletionTime { get; private set; }

    public bool IsDeleted { get; private set; }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletionTime = DateTime.Now;
    }
}