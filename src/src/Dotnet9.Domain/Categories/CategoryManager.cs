using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dotnet9.Albums;
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

    public async Task<Category> CreateAsync([NotNull] string name, string coverImageUrl, string description)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingAlbum = await _categoryRepository.FindByNameAsync(name);
        if (existingAlbum != null)
        {
            throw new CategoryAlreadyExistsException(name);
        }

        return new Category(GuidGenerator.Create(), name, coverImageUrl, description);
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