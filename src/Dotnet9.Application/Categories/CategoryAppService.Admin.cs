using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Repositories;

namespace Dotnet9.Application.Categories;

public partial class CategoryAppService
{
    public async Task<List<CategoryDto>> AdminListAsync()
    {
        var categories = await _categoryRepository.SelectAsync(x => x.Id > 0, x => x.Id, SortDirectionKind.Ascending);

        return _mapper.Map<List<Category>, List<CategoryDto>>(categories);
    }
}