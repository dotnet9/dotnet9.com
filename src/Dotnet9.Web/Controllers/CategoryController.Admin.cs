using Dotnet9.Application.Contracts.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public partial class CategoryController
{
    [HttpGet("api/category/list")]
    public async Task<List<CategoryDto>> List()
    {
        return await _categoryAppService.AdminListAsync();
    }
}