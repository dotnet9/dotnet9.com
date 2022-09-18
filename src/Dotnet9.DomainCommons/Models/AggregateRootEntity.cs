namespace Dotnet9.DomainCommons.Models;

public record AggregateRootEntity : BaseEntity, IAggregateRoot, ISoftDelete, IHasCreationTime, IHasDeletionTime,
    IHasModificationTime
{
    public DateTime CreationTime { get; internal set; } = DateTime.Now;

    public DateTime? DeletionTime { get; private set; }

    public DateTime? LastModificationTime { get; private set; }
    public bool IsDeleted { get; private set; }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletionTime = DateTime.Now;
    }

    public void NotifyModified()
    {
        LastModificationTime = DateTime.Now;
    }

    public override object[] GetKeys()
    {
        return new object[] { Id };
    }
}