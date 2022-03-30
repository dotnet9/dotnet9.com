using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;

namespace Dotnet9.Application.Categories;

public class CategoryAppService : ICategoryAppService
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly CategoryManager _categoryManager;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryAppService(ICategoryRepository categoryRepository, CategoryManager categoryManager,
        IBlogPostRepository blogPostRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _categoryManager = categoryManager;
        _blogPostRepository = blogPostRepository;
        _mapper = mapper;
    }

    public async Task<CategoryDto?> GetCategoryAsync(string slug)
    {
        var category = await _categoryRepository.GetAsync(x => x.Slug == slug);
        return category == null ? null : _mapper.Map<Category, CategoryDto>(category);
    }

    public async Task<List<CategoryCountDto>> GetListCountAsync()
    {
        var categories = await _categoryRepository.GetListCountAsync();

        return _mapper.Map<List<CategoryCount>, List<CategoryCountDto>>(categories);
    }

    public async Task<List<CategoryCountDto>> ListAllAsync()
    {
        var categories = await _categoryRepository.SelectAsync();
        return _mapper.Map<List<Category>, List<CategoryCountDto>>(categories);
    }

    public async Task<List<BlogPostWithDetailsDto>?> GetBlogPostListAsync(string categorySlug)
    {
        var blogPostWithDetailsLists = await _blogPostRepository.GetBlogPostListByCategorySlugAsync(categorySlug);
        return blogPostWithDetailsLists == null
            ? null
            : _mapper.Map<List<BlogPostWithDetails>, List<BlogPostWithDetailsDto>>(blogPostWithDetailsLists);
    }
}