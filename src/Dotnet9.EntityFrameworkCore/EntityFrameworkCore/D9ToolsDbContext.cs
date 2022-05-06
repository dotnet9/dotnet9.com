using Dotnet9.Domain;
using Dotnet9.Domain.Abouts;
using Dotnet9.Domain.ActionLogs;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Donations;
using Dotnet9.Domain.Privacies;
using Dotnet9.Domain.Shared.Abouts;
using Dotnet9.Domain.Shared.ActionLogs;
using Dotnet9.Domain.Shared.Albums;
using Dotnet9.Domain.Shared.Blogs;
using Dotnet9.Domain.Shared.Categories;
using Dotnet9.Domain.Shared.Donations;
using Dotnet9.Domain.Shared.Privacies;
using Dotnet9.Domain.Shared.Tags;
using Dotnet9.Domain.Shared.Timelines;
using Dotnet9.Domain.Shared.UrlLinks;
using Dotnet9.Domain.Shared.Users;
using Dotnet9.Domain.Tags;
using Dotnet9.Domain.Timelines;
using Dotnet9.Domain.UrlLinks;
using Dotnet9.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

public class Dotnet9DbContext : DbContext
{
    public Dotnet9DbContext(DbContextOptions<Dotnet9DbContext> options) : base(options)
    {
    }

    public DbSet<BlogPost>? BlogPosts { get; set; }
    public DbSet<Album>? Albums { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Tag>? Tags { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<UrlLink>? UrlLinks { get; set; }
    public DbSet<About>? Abouts { get; set; }
    public DbSet<Donation>? Donations { get; set; }
    public DbSet<Timeline>? Timelines { get; set; }
    public DbSet<Privacy>? Privacies { get; set; }
    public DbSet<ActionLog>? ActionLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Album>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Albums", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(AlbumConsts.MaxNameLength);
            b.Property(x => x.Slug).IsRequired().HasMaxLength(AlbumConsts.MaxSlugLength);
            b.Property(x => x.Cover).IsRequired().HasMaxLength(AlbumConsts.MaxCoverLength);
            b.Property(x => x.Description).HasMaxLength(AlbumConsts.MaxDescriptionLength);
            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.Slug);
        });

        modelBuilder.Entity<Category>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Categories", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(CategoryConsts.MaxNameLength);
            b.Property(x => x.Slug).IsRequired().HasMaxLength(CategoryConsts.MaxSlugLength);
            b.Property(x => x.Cover).IsRequired().HasMaxLength(CategoryConsts.MaxCoverLength);
            b.Property(x => x.Description).HasMaxLength(CategoryConsts.MaxDescriptionLength);
            b.Property(x => x.ParentId);
            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.Slug);
        });

        modelBuilder.Entity<Tag>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Tags", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength);
            b.HasIndex(x => x.Name);
        });

        modelBuilder.Entity<User>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Users", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(UserConsts.MaxNameLength);
            b.Property(x => x.Account).IsRequired().HasMaxLength(UserConsts.MaxAccountLength);
            b.Property(x => x.Password).IsRequired().HasMaxLength(UserConsts.MaxPasswordLength);
            b.Property(x => x.Email).IsRequired().HasMaxLength(UserConsts.MaxEmailLength);
            b.Property(x => x.Email).IsRequired().HasMaxLength(UserConsts.MaxEmailLength);
            b.Property(x => x.Role);
            b.Property(x => x.LastLoginDate);
            b.Property(x => x.LockedDate);
            b.Property(x => x.LoginFailCount);
            b.HasIndex(x => x.Disable);
        });

        modelBuilder.Entity<BlogPost>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPosts", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Title).IsRequired().HasMaxLength(BlogPostConsts.MaxTitleLength);
            b.Property(x => x.Slug).IsRequired().HasMaxLength(BlogPostConsts.MaxSlugLength);
            b.Property(x => x.BriefDescription).HasMaxLength(BlogPostConsts.MaxBriefDescriptionLength);
            b.Property(x => x.InBanner);
            b.Property(x => x.Content).IsRequired().HasMaxLength(BlogPostConsts.MaxContentLength);
            b.Property(x => x.Cover).HasMaxLength(BlogPostConsts.MaxCoverLength);
            b.Property(x => x.CopyrightType);
            b.Property(x => x.Original).HasMaxLength(BlogPostConsts.MaxOriginalLength);
            b.Property(x => x.OriginalTitle).HasMaxLength(BlogPostConsts.MaxOriginalTitleLength);
            b.Property(x => x.OriginalLink).HasMaxLength(BlogPostConsts.MaxOriginalLinkLength);
            b.HasIndex(x => x.Title);
            b.HasIndex(x => x.Slug);
            b.HasMany(x => x.Albums).WithOne().HasForeignKey(x => x.BlogPostId).IsRequired();
            b.HasMany(x => x.Categories).WithOne().HasForeignKey(x => x.BlogPostId).IsRequired();
            b.HasMany(x => x.Tags).WithOne().HasForeignKey(x => x.BlogPostId).IsRequired();
        });

        modelBuilder.Entity<BlogPostAlbum>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPostAlbums", Dotnet9Consts.DbSchema);
            b.HasKey(x => new {x.BlogPostId, x.AlbumId});
            b.HasOne<BlogPost>().WithMany(x => x.Albums).HasForeignKey(x => x.BlogPostId).IsRequired();
            b.HasOne<Album>().WithMany().HasForeignKey(x => x.AlbumId).IsRequired();
            b.HasIndex(x => new {x.BlogPostId, x.AlbumId});
        });

        modelBuilder.Entity<BlogPostCategory>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPostCategories", Dotnet9Consts.DbSchema);
            b.HasKey(x => new {x.BlogPostId, x.CategoryId});
            b.HasOne<BlogPost>().WithMany(x => x.Categories).HasForeignKey(x => x.BlogPostId).IsRequired();
            b.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId).IsRequired();
            b.HasIndex(x => new {x.BlogPostId, x.CategoryId});
        });

        modelBuilder.Entity<BlogPostTag>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPostTags", Dotnet9Consts.DbSchema);
            b.HasKey(x => new {x.BlogPostId, x.TagId});
            b.HasOne<BlogPost>().WithMany(x => x.Tags).HasForeignKey(x => x.BlogPostId).IsRequired();
            b.HasOne<Tag>().WithMany().HasForeignKey(x => x.TagId).IsRequired();
            b.HasIndex(x => new {x.BlogPostId, x.TagId});
        });

        modelBuilder.Entity<UrlLink>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}UrlLinks", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(UrlLinkConsts.MaxNameLength);
            b.Property(x => x.Url).IsRequired().HasMaxLength(UrlLinkConsts.MaxUrlLength);
            b.Property(x => x.Description).HasMaxLength(UrlLinkConsts.MaxDescriptionLength);
            b.Property(x => x.Kind);
            b.Property(x => x.Index).IsRequired();
            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.Url);
        });

        modelBuilder.Entity<About>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Abouts", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Content).IsRequired().HasMaxLength(AboutConsts.MaxContentLength);
        });

        modelBuilder.Entity<Donation>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Donations", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Content).IsRequired().HasMaxLength(DonationConsts.MaxContentLength);
        });

        modelBuilder.Entity<Timeline>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Timelines", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            _ = b.Property(x => x.Time).IsRequired();
            b.Property(x => x.Title).IsRequired().HasMaxLength(TimelineConsts.MaxTitleLength);
            b.Property(x => x.Content).IsRequired().HasMaxLength(TimelineConsts.MaxContentLength);
        });

        modelBuilder.Entity<Privacy>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Privacies", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Content).IsRequired().HasMaxLength(PrivacyConsts.MaxContentLength);
        });

        modelBuilder.Entity<ActionLog>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}ActionLogs", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.UId).HasMaxLength(ActionLogConsts.MaxUIdLength);
            b.Property(x => x.UA).HasMaxLength(ActionLogConsts.MaxUALength);
            b.Property(x => x.OS).HasMaxLength(ActionLogConsts.MaxOSLength);
            b.Property(x => x.Browser).HasMaxLength(ActionLogConsts.MaxBrowserLength);
            b.Property(x => x.Referer).HasMaxLength(ActionLogConsts.MaxRefererLength);
            b.Property(x => x.AccessName).HasMaxLength(ActionLogConsts.MaxAccessName);
            b.Property(x => x.Original).HasMaxLength(ActionLogConsts.MaxOriginalLength);
            b.Property(x => x.IP).HasMaxLength(ActionLogConsts.MaxIPLength);
            b.Property(x => x.Url).HasMaxLength(ActionLogConsts.MaxUrlLength);
            b.Property(x => x.Controller).HasMaxLength(ActionLogConsts.MaxControllerLength);
            b.Property(x => x.Action).HasMaxLength(ActionLogConsts.MaxActionLength);
            b.Property(x => x.Method).HasMaxLength(ActionLogConsts.MaxMethodLength);
            b.Property(x => x.Arguments).HasMaxLength(ActionLogConsts.MaxArgumentsLength);
            b.Property(x => x.Duration);
        });
    }
}