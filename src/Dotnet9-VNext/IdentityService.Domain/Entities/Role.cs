namespace IdentityService.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public Role()
    {
        Id = Guid.NewGuid();
    }
}