using Dotnet9.Models.Data.Blogs;
using Dotnet9.Models.Data.ModelConfigs;
using Dotnet9Tools.Auth;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dotnet9.Models.Data;

public class Dotnet9DbContext : BaseDbContext<Accounts>
{
    public Dotnet9DbContext(DbContextOptions<Dotnet9DbContext> options) : base(options)
    {
        StartChangeListen();
    }

    public DbSet<Posts> Posts { get; set; }

    public DbSet<PostTags> PostTags { get; set; }

    public DbSet<PostTagRelation> PostTagRelations { get; set; }

    public DbSet<PostCates> PostCates { get; set; }

    public DbSet<PostCateRelation> PostCateRelations { get; set; }

    public DbSet<PostComments> PostComments { get; set; }

    public DbSet<FriendLink> FriendLinks { get; set; }


    /// <summary>
    ///     系统配置
    /// </summary>
    public DbSet<SysConfig> SysConfig { get; set; }

    /// <summary>
    ///     资源文件
    /// </summary>
    public DbSet<SysResource> SysResources { get; set; }

    private void StartChangeListen()
    {
        ChangeTracker.StateChanged += UpdateTimestamps;
        ChangeTracker.Tracked += UpdateTimestamps;
    }

    private static void UpdateTimestamps(object? sender, EntityEntryEventArgs e)
    {
        object entity = e.Entry.Entity;
        switch (e.Entry.State)
        {
            case EntityState.Detached:
                break;
            case EntityState.Unchanged:
                break;
            case EntityState.Deleted:
                break;
            case EntityState.Modified:
                if (entity.GetType().GetProperty("UpdateTime") != null)
                {
                    Type type = entity.GetType();
                    type.GetProperty("UpdateTime")?.SetValue(type, DateTime.Now);
                }

                break;
            case EntityState.Added:
                // if (entity.GetType().GetProperty("CreateTime") != null)
                // {
                //     var type = entity.GetType();
                //     type.GetProperty("CreateTime")?.SetValue(type, DateTime.Now);
                // }

                break;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigAccount();
        modelBuilder.PostConfig();
    }
}