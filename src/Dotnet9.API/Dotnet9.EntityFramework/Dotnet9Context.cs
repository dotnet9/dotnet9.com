using Dotnet9.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFramework;

public class Dotnet9Context : DbContext
{
    public Dotnet9Context(DbContextOptions<Dotnet9Context> options) : base(options)
    {
    }

    public DbSet<UserInfo>? UserInfos { get; set; }

    public DbSet<BlogPost>? BlogPosts { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<BlogPostComment> BlogPostComments { get; set; }

    public DbSet<QuestionComment> QuestionComments { get; set; }

    public DbSet<Advertisement> Advertisements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userInfoConfig = modelBuilder.Entity<UserInfo>();
        userInfoConfig.Property(x => x.UserName).HasMaxLength(64);
        userInfoConfig.Property(x => x.Account).HasMaxLength(64);
        userInfoConfig.Property(x => x.Password).HasMaxLength(64);
        userInfoConfig.Property(x => x.Phone).HasMaxLength(16);
        userInfoConfig.Property(x => x.Email).HasMaxLength(64);
        userInfoConfig.Property(x => x.Introduction).HasMaxLength(512);
        userInfoConfig.Property(x => x.HeadPortrait).HasMaxLength(1024);
        userInfoConfig.Property(x => x.CreateTime).HasColumnType("datetime");

        var blogPostConfig = modelBuilder.Entity<BlogPost>();
        blogPostConfig.Property(x => x.Title).HasMaxLength(128);
        blogPostConfig.Property(x => x.Slug).HasMaxLength(128);
        blogPostConfig.Property(x => x.CoverImageUrl).HasMaxLength(128);
        blogPostConfig.Property(x => x.Original).HasMaxLength(128);
        blogPostConfig.Property(x => x.OriginalTitle).HasMaxLength(128);
        blogPostConfig.Property(x => x.OriginalLink).HasMaxLength(128);
        blogPostConfig.Property(x => x.Categories).HasMaxLength(128);
        blogPostConfig.Property(x => x.Albums).HasMaxLength(128);
        blogPostConfig.Property(x => x.Tags).HasMaxLength(128);
        blogPostConfig.Property(x => x.Content).HasMaxLength(1024 * 10);
        blogPostConfig.HasMany(x => x.UserInfoBlogPosts).WithOne().HasForeignKey(p => p.BlogPostId)
            .OnDelete(DeleteBehavior.Cascade);
        blogPostConfig.HasMany(x => x.BlogPostComments).WithOne(p=>p.BlogPost).HasForeignKey(p => p.BlogPostId)
            .OnDelete(DeleteBehavior.Cascade);
        blogPostConfig.HasOne(x => x.CreateUser).WithMany().HasForeignKey(p => p.CreateUserId)
            .OnDelete(DeleteBehavior.Restrict);
        blogPostConfig.HasOne(x => x.UpdateUser).WithMany().HasForeignKey(p => p.UpdateUserId)
            .OnDelete(DeleteBehavior.Restrict);
        blogPostConfig.Property(x => x.CreateTime).HasColumnType("datetime");
        blogPostConfig.Property(x => x.UpdateTime).HasColumnType("datetime");

        var blogPostCommentConfig = modelBuilder.Entity<BlogPostComment>();
        blogPostCommentConfig.Property(p => p.Content).HasMaxLength(512);
        blogPostCommentConfig.Property(p => p.CreateTime).HasColumnType("datetime");
        blogPostCommentConfig.Property(p => p.UpdateTime).HasColumnType("datetime");
        blogPostCommentConfig.HasOne(p => p.CreateUser).WithMany().HasForeignKey(p => p.CreateUserId).OnDelete(DeleteBehavior.Restrict);
        blogPostCommentConfig.HasOne(p => p.UpdateUser).WithMany().HasForeignKey(p => p.UpdateUserId).OnDelete(DeleteBehavior.Restrict);

        var questionConfig = modelBuilder.Entity<Question>();
        questionConfig.Property(x => x.Title).HasMaxLength(128);
        questionConfig.Property(x => x.Content).HasMaxLength(1024 * 10);
        questionConfig.Property(x => x.Categories).HasMaxLength(128);
        questionConfig.Property(x => x.Tags).HasMaxLength(128);
        questionConfig.HasMany(x => x.QuestionComments).WithOne(p=>p.Question).HasForeignKey(p => p.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        questionConfig.HasOne(x => x.CreateUser).WithMany().HasForeignKey(p => p.CreateUserId)
            .OnDelete(DeleteBehavior.Restrict);
        questionConfig.HasOne(x => x.UpdateUser).WithMany().HasForeignKey(p => p.UpdateUserId)
            .OnDelete(DeleteBehavior.Restrict);
        questionConfig.Property(x => x.CreateTime).HasColumnType("datetime");
        questionConfig.Property(x => x.UpdateTime).HasColumnType("datetime");

        var questionCommentConfig = modelBuilder.Entity<QuestionComment>();
        questionCommentConfig.Property(x => x.Content).HasMaxLength(512);
        questionCommentConfig.HasOne(x => x.CreateUser).WithMany().HasForeignKey(p => p.CreateUserId)
            .OnDelete(DeleteBehavior.Restrict);
        questionCommentConfig.HasOne(x => x.UpdateUser).WithMany().HasForeignKey(p => p.UpdateUserId)
            .OnDelete(DeleteBehavior.Restrict);
        questionCommentConfig.Property(x => x.CreateTime).HasColumnType("datetime");
        questionCommentConfig.Property(x => x.UpdateTime).HasColumnType("datetime");

        var advertisementConfig = modelBuilder.Entity<Advertisement>();
        advertisementConfig.Property(x => x.ImageUrl).HasMaxLength(128);
        advertisementConfig.Property(x => x.Url).HasMaxLength(128);
        advertisementConfig.HasOne(x => x.CreateUser).WithMany().HasForeignKey(p => p.CreateUserId)
            .OnDelete(DeleteBehavior.Restrict);
        advertisementConfig.HasOne(x => x.UpdateUser).WithMany().HasForeignKey(p => p.UpdateUserId)
            .OnDelete(DeleteBehavior.Restrict);
        advertisementConfig.Property(x => x.CreateTime).HasColumnType("datetime");
        advertisementConfig.Property(x => x.UpdateTime).HasColumnType("datetime");
    }
}