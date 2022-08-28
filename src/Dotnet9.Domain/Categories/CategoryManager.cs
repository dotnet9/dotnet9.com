using Dotnet9.Core;

namespace Dotnet9.Domain.Categories;

public class CategoryManager
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> CreateAsync(int? id, string name, string slug, string cover, string? description,
        int parentId)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.NotNullOrWhiteSpace(cover, nameof(cover));

        var existCategory = await _categoryRepository.FindByNameAsync(name);
        if (existCategory != null)
        {
            throw new Exception($"存在同名的分类: {name}");
        }

        existCategory = await _categoryRepository.FindBySlugAsync(slug);
        if (existCategory != null)
        {
            throw new Exception($"存在相同别名的分类: {slug}");
        }

        if (id != null)
        {
            return new Category(id.Value, name, slug, cover, description, parentId);
        }

        var maxIdOfCategory = await _categoryRepository.GetMaxIdAsync();
        id = maxIdOfCategory + 1;

        return new Category(id.Value, name, slug, cover, description, parentId);
    }

    public async Task ChangeNameAsync(Category category, string newName)
    {
        Check.NotNull(category, nameof(category));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existCategory = await _categoryRepository.FindByNameAsync(newName);
        if (existCategory != null && existCategory.Id != category.Id)
        {
            throw new Exception("存在同名的分类");
        }

        category.ChangeName(newName);
    }

    public async Task ChangeSlugAsync(Category category, string newSlug)
    {
        Check.NotNull(category, nameof(category));
        Check.NotNullOrWhiteSpace(newSlug, nameof(newSlug));

        var existCategory = await _categoryRepository.FindBySlugAsync(newSlug);
        if (existCategory != null && existCategory.Id != category.Id)
        {
            throw new Exception("存在相同别名的分类");
        }

        category.ChangeSlug(newSlug);
    }
}