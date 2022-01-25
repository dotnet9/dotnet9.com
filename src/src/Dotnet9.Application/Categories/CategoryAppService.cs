using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Categories;

public class CategoryAppService : Dotnet9AppService, ICategoryAppService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryManager _categoryManager;
    private readonly IMemoryCache _memoryCache;

    public CategoryAppService(ICategoryRepository categoryRepository, CategoryManager categoryManager,
        IMemoryCache memoryCache)
    {
        _categoryRepository = categoryRepository;
        _categoryManager = categoryManager;
        _memoryCache = memoryCache;
    }

    public async Task<CategoryDto> GetAsync(Guid id)
    {
        var category = await _categoryRepository.GetAsync(id);
        return ObjectMapper.Map<Category, CategoryDto>(category);
    }

    public async Task<List<CategoryDto>> GetListAsync()
    {
        return await _memoryCache.GetOrCreateAsync("AllCategory", async e =>
        {
            e.SetOptions(new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });

            var categories = await _categoryRepository.GetListAsync(0, int.MaxValue, nameof(Category.Name));

            return ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories);
        });
    }

    public async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Category.Name);
        }

        var categories = await _categoryRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting,
            input.Filter);

        var totalCount = input.Filter == null
            ? await _categoryRepository.CountAsync()
            : await _categoryRepository.CountAsync(category => category.Name.Contains(input.Filter));

        return new PagedResultDto<CategoryDto>(totalCount,
            ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories));
    }

    [Authorize(Dotnet9Permissions.Categories.Create)]
    public async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
    {
        var category =
            await _categoryManager.CreateAsync(input.ParentId, input.Name, input.CoverImageUrl, input.Description);

        await _categoryRepository.InsertAsync(category);

        return ObjectMapper.Map<Category, CategoryDto>(category);
    }

    [Authorize(Dotnet9Permissions.Categories.Edit)]
    public async Task UpdateAsync(Guid id, UpdateCategoryDto input)
    {
        var category = await _categoryRepository.GetAsync(id);

        if (category.Name != input.Name)
        {
            await _categoryManager.ChangeNameAsync(category, input.Name);
        }

        category.CoverImageUrl = input.CoverImageUrl;
        category.Description = input.Description;

        await _categoryRepository.UpdateAsync(category);
    }

    [Authorize(Dotnet9Permissions.Categories.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _categoryRepository.DeleteAsync(id);
    }
}