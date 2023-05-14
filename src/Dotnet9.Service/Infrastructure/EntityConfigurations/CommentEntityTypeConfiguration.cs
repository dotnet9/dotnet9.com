namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comment");
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Url).IsRequired().HasMaxLength(CommentConsts.MaxUrlLength);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(CommentConsts.MaxUserNameLength);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(CommentConsts.MaxEmailLength);
        builder.Property(x => x.Content).HasMaxLength(CommentConsts.MaxContentLength);
        builder.Property(x => x.ParentId);
        builder.Property(x => x.Visible);
    }
}