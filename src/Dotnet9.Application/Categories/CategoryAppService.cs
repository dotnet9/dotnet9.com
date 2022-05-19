using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Core;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Repositories;

namespace Dotnet9.Application.Categories;

public partial class CategoryAppService : ICategoryAppService
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryAppService(ICategoryRepository categoryRepository, IBlogPostRepository blogPostRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _blogPostRepository = blogPostRepository;
        _mapper = mapper;
    }

    public async Task<CategoryViewModel?> GetCategoryAsync(string? slug)
    {
        if (slug.IsNullOrWhiteSpace())
        {
            var categoryCounts = await _categoryRepository.GetListCountAsync(x => x.Id > 0);

            return new CategoryViewModel
                {Categories = _mapper.Map<List<CategoryCount>, List<CategoryCountDto>>(categoryCounts)};
        }

        var category = await _categoryRepository.GetAsync(x => x.Slug == slug);
        if (category == null) return null;

        var children = await _categoryRepository.GetListCountAsync(x => x.ParentId == category.Id);
        if (children.Any())
        {
            return new CategoryViewModel
                {Categories = _mapper.Map<List<CategoryCount>, List<CategoryCountDto>>(children)};
        }

        var vm = new CategoryViewModel {Name = category.Name};
        var blogPosts =
            await _blogPostRepository.SelectBlogPostBriefAsync(x =>
                    x.Categories != null && x.Categories.Any(d => d.CategoryId == category.Id), x => x.CreateDate,
                SortDirectionKind.Descending);
        if (!blogPosts.IsNullOrEmpty())
            vm.BlogPosts = _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(blogPosts!);

        return vm;
    }

    public async Task<List<CategoryCountDto>> GetListCountAsync()
    {
        var categories = await _categoryRepository.GetListCountAsync(x => x.Id > 0);
        return _mapper.Map<List<CategoryCount>, List<CategoryCountDto>>(categories);
    }
}