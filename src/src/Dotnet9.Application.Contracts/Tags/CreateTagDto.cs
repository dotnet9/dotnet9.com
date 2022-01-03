using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Tags;

public class CreateTagDto
{
    [Required]
    [StringLength(TagConsts.MaxNameLength)]
    public virtual string Name { get; set; }

    [Required]
    [StringLength(TagConsts.MaxDescriptionLength)]
    public virtual string Description { get; set; }
}