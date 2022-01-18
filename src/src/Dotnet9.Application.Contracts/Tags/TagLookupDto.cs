using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Tags;

public class TagLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }
}