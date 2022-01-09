using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Albums;

public class AlbumDto : EntityDto<Guid>
{
    public string Name { get; set; }

    public string CoverImageUrl { get; set; }

    public string Description { get; set; }
}