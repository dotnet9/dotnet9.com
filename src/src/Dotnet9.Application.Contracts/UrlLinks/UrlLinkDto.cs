using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.UrlLinks;

public class UrlLinkDto : EntityDto<Guid>
{
    public string Name { get; set; }
    
    public string Url { get; set; }
    
    public string Description { get; set; }

    public int Index { get; set; }
}