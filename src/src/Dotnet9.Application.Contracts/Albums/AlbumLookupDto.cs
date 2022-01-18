using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Albums;

public class AlbumLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
}