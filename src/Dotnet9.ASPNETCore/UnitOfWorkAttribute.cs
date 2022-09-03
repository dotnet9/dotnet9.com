namespace Dotnet9.ASPNETCore;

[AttributeUsage(AttributeTargets.Class
                | AttributeTargets.Method)]
public class UnitOfWorkAttribute : Attribute
{
    public UnitOfWorkAttribute(params Type[] dbContextTypes)
    {
        DbContextTypes = dbContextTypes;
        foreach (var type in dbContextTypes)
        {
            if (!typeof(DbContext).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{type} must inherit from DbContext");
            }
        }
    }

    public Type[] DbContextTypes { get; init; }
}