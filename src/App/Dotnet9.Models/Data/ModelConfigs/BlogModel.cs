using Dotnet9.Models.Data.Blogs;

namespace Dotnet9.Models.Data.ModelConfigs;

public static class BlogModel
{
    public static void PostConfig(this ModelBuilder builder)
    {
        //Posts

        builder.Entity<Posts>().HasQueryFilter(a => a.IsDelete == false);

        builder.Entity<PostTagRelation>().HasQueryFilter(a => a.Post.IsDelete == false);
        builder.Entity<PostCateRelation>().HasQueryFilter(a => a.Post.IsDelete == false);
        builder.Entity<PostVisitRecord>().HasQueryFilter(a => a.Post.IsDelete == false);
        builder.Entity<PostComments>().HasQueryFilter(a => a.Post.IsDelete == false);

        builder.Entity<Posts>().Property(a => a.Title).IsRequired().HasMaxLength(150);
        builder.Entity<Posts>().Property(a => a.Slug).IsRequired().HasMaxLength(256);
        builder.Entity<Posts>().Property(a => a.ShortId).IsRequired().HasMaxLength(10);
        builder.Entity<Posts>().Property(a => a.Content).IsRequired().HasColumnType("text");
        builder.Entity<Posts>().Property(a => a.Snippet).IsRequired().HasMaxLength(200);
        builder.Entity<Posts>().HasMany(a => a.TagRelations).WithOne(a => a.Post)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Posts>().HasMany(a => a.CateRelations).WithOne(a => a.Post)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Posts>().HasMany(a => a.PostComments).WithOne(a => a.Post);

        builder.Entity<Posts>().HasMany(a => a.VisitRecords).WithOne(a => a.Post)
            .HasForeignKey(a => a.PostId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PostVisitRecord>().Property(a => a.IP).HasMaxLength(50);
        builder.Entity<PostVisitRecord>().Property(a => a.UA).HasMaxLength(500);
        builder.Entity<PostVisitRecord>().Property(a => a.UId).HasMaxLength(32);
        //PostTags
        builder.Entity<PostTags>().Property(a => a.TagName).HasMaxLength(32).IsRequired();
        builder.Entity<PostTags>().HasMany(a => a.TagRelation)
            .WithOne(a => a.PostTags);

        builder.Entity<PostTagRelation>()
            .HasQueryFilter(a => a.Post.IsDelete == false)
            .HasOne(a => a.PostTags);
        builder.Entity<PostTagRelation>().HasOne(a => a.PostTags);

        //PostCate

        builder.Entity<PostCates>().Property(a => a.CateName).HasMaxLength(20).IsRequired();

        builder.Entity<PostCates>().HasMany(a => a.PostCateRelations).WithOne(a => a.PostCate);

        builder.Entity<PostCateRelation>().HasOne(a => a.Post)
            .WithMany(a => a.CateRelations).OnDelete(DeleteBehavior.NoAction);
        builder.Entity<PostCateRelation>().HasOne(a => a.Post).WithMany(a => a.CateRelations)
            .OnDelete(DeleteBehavior.NoAction);


        builder.Entity<SysConfig>().Property(a => a.Key).HasMaxLength(50);
        builder.Entity<SysConfig>().Property(a => a.Value).HasMaxLength(2000);
        builder.Entity<SysConfig>().Property(a => a.Description).HasMaxLength(200);
        builder.Entity<SysConfig>().HasIndex(a => a.Key).IsUnique();
        builder.Entity<SysConfig>().Property(a => a.GroupName).HasMaxLength(50);
        builder.Entity<AccountLoginRecord>().Property(a => a.Ip).HasMaxLength(50);
        builder.Entity<AccountLoginRecord>().Property(a => a.UA).HasMaxLength(500);
        builder.Entity<SysResource>();
    }
}