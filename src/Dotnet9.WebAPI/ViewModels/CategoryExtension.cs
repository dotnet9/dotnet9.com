namespace Dotnet9.WebAPI.ViewModels;

public static class CategoryExtension
{
    public static async Task<Dictionary<Guid, string>?> GetCategoryIdAndNames(this Dotnet9DbContext dbContext,
        IMemoryCacheHelper cacheHelper)
    {
        async Task<Dictionary<Guid, string>?> GetIdAndNamesFromDb()
        {
            return await dbContext.Categories!.ToDictionaryAsync(category => category.Id, category => category.Name);
        }

        return await cacheHelper.GetOrCreateAsync("CategoryIDAndNames", async e => await GetIdAndNamesFromDb());
    }
}