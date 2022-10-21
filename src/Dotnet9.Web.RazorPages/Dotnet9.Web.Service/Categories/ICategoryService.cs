namespace Dotnet9.Web.Service.Categories;

public interface ICategoryService
{
    Task<List<CategoryBrief>> CategoriesAsync();
    Task<List<CategoryBriefForMenu>?> CategoriesForMenuAsync();
}