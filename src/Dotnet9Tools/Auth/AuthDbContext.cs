using System.Reflection;
using Dotnet9Tools.Auth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dotnet9Tools.Auth;

public class BaseDbContext<User> : DbContext where User : BaseAccount
{
    public BaseDbContext(DbContextOptions option) : base(option)
    {
    }

    public virtual DbSet<User> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region 批量设置CreateTime

        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            PropertyInfo[] properties = entityType.ClrType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "CreateTime" && property.PropertyType == typeof(DateTime))
                {
                    modelBuilder.Entity(entityType.ClrType).Property(property.Name).HasDefaultValueSql("now()");
                }
            }
        }

        #endregion
    }
}