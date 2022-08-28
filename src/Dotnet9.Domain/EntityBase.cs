using Dotnet9.Domain.Users;

namespace Dotnet9.Domain;

public class EntityBase
{
    protected EntityBase()
    {
    }

    protected EntityBase(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    public int? CreateUserId { get; set; }
    [NotMapped] public CustomUser? CreateUser { get; set; }
    public DateTimeOffset? CreateDate { get; set; }


    public int? UpdateUserId { get; set; }

    [NotMapped] public CustomUser? UpdateUser { get; set; }

    public DateTimeOffset? UpdateDate { get; set; }

    public bool IsDeleted { get; set; }
    public int? DeleteUserId { get; set; }

    [NotMapped] public CustomUser? DeleteUser { get; set; }

    public DateTimeOffset? DeleteDate { get; set; }
}