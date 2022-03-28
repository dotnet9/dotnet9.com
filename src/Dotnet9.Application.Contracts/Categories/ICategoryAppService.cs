namespace Dotnet9.Application.Contracts.Categories;

public interface ICategoryAppService
{
    Task<List<CategoryCountDto>> GetListCountAsync();
    Task<List<CategoryCountDto>> ListAllAsync();
}