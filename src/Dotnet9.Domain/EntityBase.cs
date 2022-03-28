using System.ComponentModel.DataAnnotations.Schema;
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
    [NotMapped] public User? CreateUser { get; set; }
    public DateTime? CreateDate { get; set; }


    public int? UpdateUserId { get; set; }

    [NotMapped] public User? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool IsDeleted { get; set; }
    public int? DeleteUserId { get; set; }

    [NotMapped] public User? DeleteUser { get; set; }

    public DateTime? DeleteDate { get; set; }
}