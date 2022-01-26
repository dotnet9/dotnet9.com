using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Categories;

public class CategoryCount : FullAuditedAggregateRoot<Guid>
{
    private CategoryCount()
    {
    }


    public CategoryCount(
        Guid id,
        Guid? parentId,
        [NotNull] string name,
        string description,
        int blogPostCount) : base(id)
    {
        ParentId = parentId;
        Name = name;
        Description = description;
        BlogPostCount = blogPostCount;
    }

    public Guid? ParentId { get; set; }

    [NotNull] public string Name { get; set; }

    public string CoverImageUrl { get; set; }

    public string Description { get; set; }

    public int BlogPostCount { get; set; }
}