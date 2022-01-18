using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Categories;

public class CategoryLookupDto : EntityDto<Guid>
{
    public Guid? ParentId { get; set; }

    public string Name { get; set; }
}