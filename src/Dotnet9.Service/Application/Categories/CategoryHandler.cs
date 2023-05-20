namespace Dotnet9.Service.Application.Categories;

public class CategoryHandler
{
    private readonly ICategoryRepository _repository;

    public CategoryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(CategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAllBriefAsync();

        query.Result = new PaginatedListBase<CategoryBrief>()
        {
            Result = categories!
        };
    }
}