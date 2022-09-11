namespace Dotnet9.WebAPI.Domain.Categories;

public class CategoryManager
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryManager(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> CreateAsync(Guid? id, int sequenceNumber, string name, string slug, string cover,
        string? description, bool visible,
        Guid? parentId)
    {
        var isNew = id == null;
        Category? oldCategory = null;
        if (isNew)
        {
            id = Guid.NewGuid();
        }
        else
        {
            oldCategory = await _categoryRepository.FindByIdAsync(id!.Value);
            if (oldCategory == null)
            {
                throw new Exception($"不存在的分类: {id}");
            }
        }

        Check.NotNullOrWhiteSpace(name, nameof(name), CategoryConsts.MaxNameLength, CategoryConsts.MinNameLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug), CategoryConsts.MaxSlugLength, CategoryConsts.MinSlugLength);
        Check.NotNullOrWhiteSpace(cover, nameof(cover), CategoryConsts.MaxCoverLength, CategoryConsts.MinCoverLength);

        var existCategory = await _categoryRepository.FindByNameAsync(name);
        if ((isNew && existCategory != null) ||
            (isNew == false && existCategory != null && existCategory.Id != oldCategory!.Id))
        {
            throw new Exception($"存在同名的分类: {name}");
        }

        existCategory = await _categoryRepository.FindBySlugAsync(slug);
        if ((isNew && existCategory != null) ||
            (isNew == false && existCategory != null && existCategory.Id != oldCategory!.Id))
        {
            throw new Exception($"存在相同别名的分类: {slug}");
        }

        if (parentId is not null)
        {
            existCategory = await _categoryRepository.FindByIdAsync(parentId.Value);
            if (existCategory == null)
            {
                throw new Exception($"不存在的父级分类: {parentId}");
            }
        }

        if (isNew)
        {
            return new Category(id.Value, sequenceNumber, name, slug, cover, description, visible, parentId);
        }

        oldCategory!.ChangeSequenceNumber(sequenceNumber);
        oldCategory.ChangeName(name);
        oldCategory.ChangeSlug(slug);
        oldCategory.ChangeCover(cover);
        oldCategory.ChangeDescription(description);
        oldCategory.ChangeVisible(visible);
        oldCategory.ChangeParentId(parentId);
        return oldCategory;
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