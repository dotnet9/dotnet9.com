namespace Dotnet9.Service.Domain.Aggregates.Tags;

public class TagManager : IScopedDependency
{
    private readonly ITagRepository _tagRepository;

    public TagManager(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Tag> CreateAsync(Guid? id, string name)
    {
        var isNew = id == null;
        Tag? oldTag = null;
        if (isNew)
        {
            id = Guid.NewGuid();
        }
        else
        {
            oldTag = await _tagRepository.FindByIdAsync(id!.Value);
            if (oldTag == null)
            {
                throw new Exception($"不存在的标签: {id}");
            }
        }

        Check.NotNullOrWhiteSpace(name, nameof(name), TagConsts.MaxNameLength, TagConsts.MinNameLength);


        var existTag = await _tagRepository.FindByNameAsync(name);
        if ((isNew && existTag != null) ||
            (isNew == false && existTag != null && existTag.Id != oldTag!.Id))
        {
            throw new Exception($"存在同名的标签: {name}");
        }

        if (isNew)
        {
            oldTag = new Tag(id.Value, name);
        }
        else
        {
            oldTag!.ChangeName(name);
        }

        return oldTag;
    }

    public Tag CreateForSeed(string name)
    {
        return new Tag(Guid.NewGuid(), name);
    }
}