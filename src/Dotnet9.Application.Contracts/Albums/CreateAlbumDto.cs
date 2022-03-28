using System.ComponentModel.DataAnnotations;
using Dotnet9.Domain.Shared.Albums;

namespace Dotnet9.Application.Contracts.Albums;

public class CreateAlbumDto
{
    [Required]
    [StringLength(AlbumConsts.MaxNameLength)]
    public virtual string Name { get; set; } = null!;

    [StringLength(AlbumConsts.MaxCoverLength)]
    public virtual string? CoverImageUrl { get; set; }

    [StringLength(AlbumConsts.MaxDescriptionLength)]
    public virtual string? Description { get; set; }
}