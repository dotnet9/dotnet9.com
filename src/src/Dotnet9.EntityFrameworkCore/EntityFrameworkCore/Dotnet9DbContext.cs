using Dotnet9.Abouts;
using Dotnet9.Albums;
using Dotnet9.Blogs;
using Dotnet9.Categories;
using Dotnet9.Comments;
using Dotnet9.Contacts;
using Dotnet9.Privacies;
using Dotnet9.Ratings;
using Dotnet9.Tags;
using Dotnet9.UrlLinks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ConnectionStringName("Default")]
    public class Dotnet9DbContext :
        AbpDbContext<Dotnet9DbContext>,
        IIdentityDbContext,
        ITenantManagementDbContext
    {
        /* Add DbSet properties for your Aggregate Roots / Entities here. */
        public DbSet<About> Abouts { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Privacy> Privacies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UrlLink> UruLinks { get; set; }

        #region Entities from the modules

        /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityDbContext and ITenantManagementDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */

        //Identity
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }

        // Tenant Management
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

        #endregion

        public Dotnet9DbContext(DbContextOptions<Dotnet9DbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside here */

            builder.Entity<About>(b =>
            {
                b.ToTable($"{Dotnet9Consts.DbTablePrefix}Abouts", Dotnet9Consts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Details).IsRequired().HasMaxLength(AboutConsts.MaxDetailsLength);
            });

            builder.Entity<Album>(b =>
            {
                b.ToTable($"{Dotnet9Consts.DbTablePrefix}Albums", Dotnet9Consts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(AlbumConsts.MaxNameLength);
                b.Property(x => x.CoverImageUrl).HasMaxLength(AlbumConsts.MaxCoverImageUrlLength);
                b.Property(x => x.Description).HasMaxLength(AlbumConsts.MaxDescriptionLength);
                b.HasIndex(x => x.Name);
            });

            builder.Entity<BlogPost>(b =>
            {
                b.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPosts", Dotnet9Consts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Title).IsRequired().HasMaxLength(BlogPostConsts.MaxTitleLength);
                b.Property(x => x.Slug).IsRequired().HasMaxLength(BlogPostConsts.MaxSlugLength);
                b.Property(x => x.ShortDescription).HasMaxLength(BlogPostConsts.MaxShortDescriptionLength);
                b.Property(x => x.Content).IsRequired().HasMaxLength(BlogPostConsts.MaxContentLength);
                b.Property(x => x.CoverImageUrl).HasMaxLength(BlogPostConsts.MaxCoverImageUrlLength);
                b.Property(x => x.BlogCopyrightType);
                b.Property(x => x.Original).HasMaxLength(BlogPostConsts.MaxOriginalLength);
                b.Property(x => x.OriginalTitle).HasMaxLength(BlogPostConsts.MaxOriginalTitleLength);
                b.Property(x => x.OriginalLink).HasMaxLength(BlogPostConsts.MaxOriginalLinkLength);
                b.HasIndex(x => x.Title);
                b.HasIndex(x => x.Slug);
            });

            builder.Entity<Category>(b =>
            {
                b.ToTable($"{Dotnet9Consts.DbTablePrefix}Categories", Dotnet9Consts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(CategoryConsts.MaxNameLength);
                b.Property(x => x.CoverImageUrl).HasMaxLength(CategoryConsts.MaxCoverImageUrlLength);
                b.Property(x => x.Description).HasMaxLength(CategoryConsts.MaxDescriptionLength);
                b.HasIndex(x => x.Name);
            });

            builder.Entity<Comment>(b =>
            {
                b.ToTable($"{Dotnet9Consts.DbTablePrefix}Comments", Dotnet9Consts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Text).IsRequired().HasMaxLength(CommentConsts.MaxTextLength);
                b.Property(x => x.RepliedCommentId);
                b.HasIndex(x => x.Text);
            });

            builder.Entity<Contact>(b =>
            {
                b.ToTable($"{Dotnet9Consts.DbTablePrefix}Contacts", Dotnet9Consts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(ContactConsts.MaxNameLength);
                b.Property(x => x.Email).IsRequired().HasMaxLength(ContactConsts.MaxEmailLength);
                b.Property(x => x.Subject).IsRequired().HasMaxLength(ContactConsts.MaxSubjectLength);
                b.Property(x => x.Message).IsRequired().HasMaxLength(ContactConsts.MaxMessageLength);
                b.HasIndex(x => x.Name);
                b.HasIndex(x => x.Email);
            });

            builder.Entity<Privacy>(b =>
            {
                b.ToTable($"{Dotnet9Consts.DbTablePrefix}Privacies", Dotnet9Consts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Details).IsRequired().HasMaxLength(PrivacyConsts.MaxDetailsLength);
            });

            builder.Entity<Rating>(b =>
            {
                b.ToTable($"{Dotnet9Consts.DbTablePrefix}Ratings", Dotnet9Consts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.StarCount);
            });

            builder.Entity<Tag>(b =>
            {
                b.ToTable($"{Dotnet9Consts.DbTablePrefix}Tags", Dotnet9Consts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength);
                b.Property(x => x.Description).IsRequired().HasMaxLength(TagConsts.MaxDescriptionLength);
                b.HasIndex(x => x.Name);
            });

            builder.Entity<UrlLink>(b =>
            {
                b.ToTable($"{Dotnet9Consts.DbTablePrefix}UrlLinks", Dotnet9Consts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(UrlLinkConsts.MaxNameLength);
                b.Property(x => x.Url).HasMaxLength(UrlLinkConsts.MaxUrlLength);
                b.Property(x => x.Description).HasMaxLength(UrlLinkConsts.MaxDescriptionLength);
                b.Property(x => x.Index);
                b.HasIndex(x => x.Name);
                b.HasIndex(x => x.Url);
            });
        }
    }
}