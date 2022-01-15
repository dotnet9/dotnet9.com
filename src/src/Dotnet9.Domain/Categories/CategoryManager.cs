using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Dotnet9.Categories;

public class CategoryManager : DomainService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> CreateAsync(Guid? parentId, [NotNull] string name, string coverImageUrl,
        string description)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingAlbum = await _categoryRepository.FindByNameAsync(name);
        if (existingAlbum != null)
        {
            throw new CategoryAlreadyExistsException(name);
        }

        return new Category(GuidGenerator.Create(), parentId, name, coverImageUrl, description);
    }

    public async Task ChangeNameAsync([NotNull] Category category, [NotNull] string newName)
    {
        Check.NotNull(category, nameof(category));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingCategory = await _categoryRepository.FindByNameAsync(newName);
        if (existingCategory != null && existingCategory.Id != category.Id)
        {
            throw new CategoryAlreadyExistsException(newName);
        }

        category.ChangeName(newName);
    }
}