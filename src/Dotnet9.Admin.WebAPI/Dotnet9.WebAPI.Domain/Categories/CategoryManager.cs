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
        bool isNew = id == null;
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

        if (parentId is not null)
        {
            Category? existCategory = await _categoryRepository.FindByIdAsync(parentId.Value);
            if (existCategory == null)
            {
                throw new Exception($"不存在的父级分类: {parentId}");
            }
        }

        if (isNew)
        {
            oldCategory = new Category(id.Value, sequenceNumber, name, slug, cover, description, visible, parentId);
        }
        else
        {
            oldCategory!.ChangeSequenceNumber(sequenceNumber);
            oldCategory.ChangeSlug(slug);
            oldCategory.ChangeCover(cover);
            oldCategory.ChangeDescription(description);
            oldCategory.ChangeVisible(visible);
            oldCategory.ChangeParentId(parentId);
        }

        await ChangeNameAsync(isNew, oldCategory, name);
        await ChangeSlugAsync(isNew, oldCategory, slug);

        return oldCategory;
    }

    public Category CreateForSeed(string name, string slug, string cover, Guid? parentId)
    {
        return new Category(Guid.NewGuid(), 1, name, slug, cover, null, true, parentId);
    }

    public async Task ChangeNameAsync(bool isNew, Category category, string newName)
    {
        Check.NotNull(category, nameof(category));
        Check.NotNullOrWhiteSpace(newName, nameof(newName), CategoryConsts.MaxNameLength, CategoryConsts.MinNameLength);

        Category? existCategory = await _categoryRepository.FindByNameAsync(newName);
        if ((isNew && existCategory != null) ||
            (isNew == false && existCategory != null && existCategory.Id != category!.Id))
        {
            throw new Exception($"存在同名的分类: {newName}");
        }

        category.ChangeName(newName);
    }

    public async Task ChangeSlugAsync(bool isNew, Category category, string newSlug)
    {
        Check.NotNull(category, nameof(category));
        Check.NotNullOrWhiteSpace(newSlug, nameof(newSlug), CategoryConsts.MaxSlugLength, CategoryConsts.MinSlugLength);

        Category? existCategory = await _categoryRepository.FindBySlugAsync(newSlug);
        if ((isNew && existCategory != null) ||
            (isNew == false && existCategory != null && existCategory.Id != category!.Id))
        {
            throw new Exception($"存在相同别名的分类: {newSlug}");
        }

        category.ChangeSlug(newSlug);
    }
}