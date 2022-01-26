using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dotnet9.Categories;

public interface ICategoryAppService : IApplicationService
{
    Task<CategoryDto> GetAsync(Guid id);

    Task<List<CategoryCountDto>> GetListCountAsync();

    Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input);

    Task<CategoryDto> CreateAsync(CreateCategoryDto input);

    Task UpdateAsync(Guid id, UpdateCategoryDto input);

    Task DeleteAsync(Guid id);
}