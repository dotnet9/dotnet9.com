using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Tags;

public class UpdateTagDto
{
    [Required]
    [StringLength(TagConsts.MaxNameLength)]
    public virtual string Name { get; set; }
    
    [StringLength(TagConsts.MaxDescriptionLength)]
    public virtual string Description { get; set; }
}