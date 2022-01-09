using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Albums;

public class CreateAlbumDto
{
    [Required]
    [StringLength(AlbumConsts.MaxNameLength)]
    public virtual string Name { get; set; }
    
    [StringLength(AlbumConsts.MaxCoverImageUrlLength)]
    public virtual string CoverImageUrl { get; set; }
    
    [StringLength(AlbumConsts.MaxDescriptionLength)]
    public virtual string Description { get; set; }
}