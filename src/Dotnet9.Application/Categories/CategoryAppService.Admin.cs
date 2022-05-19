using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Domain.Categories;

namespace Dotnet9.Application.Categories;

public partial class CategoryAppService
{
    public async Task<List<CategoryDto>> AdminListAsync()
    {
        var categories = await _categoryRepository.SelectAsync();

        return _mapper.Map<List<Category>, List<CategoryDto>>(categories);
    }
}