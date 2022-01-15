using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Categories;

public class UpdateCategoryDto
{
    public Guid? ParentId { get; set; }

    [Required]
    [StringLength(CategoryConsts.MaxNameLength)]
    public virtual string Name { get; set; }
    
    [StringLength(CategoryConsts.MaxCoverImageUrlLength)]
    public virtual string CoverImageUrl { get; set; }
    
    [StringLength(CategoryConsts.MaxDescriptionLength)]
    public virtual string Description { get; set; }
}