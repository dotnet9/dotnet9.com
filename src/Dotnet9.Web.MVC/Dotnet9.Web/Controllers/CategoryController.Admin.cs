using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Domain.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public partial class CategoryController
{
    [HttpGet("api/category/list")]
    public async Task<List<CategoryDto>> List()
    {
        return await _categoryAppService.AdminListAsync();
    }

    [HttpDelete("api/category/delete")]
    public async Task Delete(int id)
    {
        await _categoryRepository.DeleteAsync(x => x.Id == id);
    }

    [HttpPost("api/category/addOrUpdate")]
    public async Task AddOrUpdate([FromBody] AddOrUpdateCategoryDto request)
    {
        var categoryFromDb = await _categoryRepository.GetAsync(x => x.Id == request.Id);
        if (categoryFromDb == null)
        {
            var categoryForDb = _mapper.Map<AddOrUpdateCategoryDto, Category>(request);
            categoryForDb.Id = await _categoryRepository.GetMaxIdAsync() + 1;
            categoryForDb.CreateDate = DateTimeOffset.Now;
            await _categoryRepository.InsertAsync(categoryForDb);
        }
        else
        {
            _mapper.Map(request, categoryFromDb, typeof(AddOrUpdateCategoryDto), typeof(Category));
            categoryFromDb.UpdateDate = DateTimeOffset.Now;
            await _categoryRepository.UpdateAsync(categoryFromDb);
        }
    }
}