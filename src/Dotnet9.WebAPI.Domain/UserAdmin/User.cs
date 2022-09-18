namespace Dotnet9.WebAPI.Domain.UserAdmin;

public class User : IdentityUser<Guid>, IHasCreationTime, IHasDeletionTime, ISoftDelete
{
    public User(string userName) : base(userName)
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public DateTime CreationTime { get; internal set; }
    public DateTime? DeletionTime { get; private set; }

    public bool IsDeleted { get; private set; }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletionTime = DateTime.Now;
    }
}