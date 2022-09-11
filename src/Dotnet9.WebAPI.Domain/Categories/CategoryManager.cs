namespace Dotnet9.WebAPI.Domain.Categories;

public class CategoryManager
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> CreateAsync(Guid id, UpdateCategoryRequest request)
    {
        var existCategory = await _categoryRepository.FindByIdAsync(id);
        if (existCategory == null)
        {
            throw new Exception($"不存在的分类: {id}");
        }

        return await CreateAsync(id, request.SequenceNumber, request.Name, request.Slug, request.Cover,
            request.Description, request.Visible, request.ParentId);
    }

    public async Task<Category> CreateAsync(AddCategoryRequest request)
    {
        var id = Guid.NewGuid();
        return await CreateAsync(id, request.SequenceNumber, request.Name, request.Slug, request.Cover,
            request.Description, request.Visible, request.ParentId);
    }

    public async Task<Category> CreateAsync(Guid id, int sequenceNumber, string name, string slug, string cover,
        string? description, bool visible,
        Guid? parentId)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name), CategoryConsts.MaxNameLength, CategoryConsts.MinNameLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug), CategoryConsts.MaxSlugLength, CategoryConsts.MinSlugLength);
        Check.NotNullOrWhiteSpace(cover, nameof(cover), CategoryConsts.MaxCoverLength, CategoryConsts.MinCoverLength);

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

        if (parentId is null)
        {
            return new Category(id, sequenceNumber, name, slug, cover, description, visible, parentId);
        }

        existCategory = await _categoryRepository.FindByIdAsync(parentId.Value);
        if (existCategory == null)
        {
            throw new Exception($"不存在的父级分类: {parentId}");
        }

        return new Category(id, sequenceNumber, name, slug, cover, description, visible, parentId);
    }

    public async Task ChangeNameAsync(Category category, string newName)
    {
        Check.NotNull(category, nameof(category));
        Check.NotNullOrWhiteSpace(newName, nameof(newName), CategoryConsts.MaxNameLength, CategoryConsts.MinNameLength);

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
        Check.NotNullOrWhiteSpace(newSlug, nameof(newSlug), CategoryConsts.MaxSlugLength, CategoryConsts.MinSlugLength);

        var existCategory = await _categoryRepository.FindBySlugAsync(newSlug);
        if (existCategory != null && existCategory.Id != category.Id)
        {
            throw new Exception("存在相同别名的分类");
        }

        category.ChangeSlug(newSlug);
    }
}