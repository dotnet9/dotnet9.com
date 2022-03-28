using AutoMapper;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Domain.Categories;

namespace Dotnet9.Application.Categories;

public class CategoryAppService : ICategoryAppService
{
    private readonly CategoryManager _categoryManager;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryAppService(ICategoryRepository categoryRepository, CategoryManager categoryManager, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _categoryManager = categoryManager;
        _mapper = mapper;
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
}