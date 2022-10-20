namespace Dotnet9.Web.Service.Categories;

public interface ICategoryService
{
    Task<List<CategoryBrief>> GetCategoriesAsync();
    Task<List<CategoryBriefForMenu>?> GetCategoriesForMenuAsync();
}