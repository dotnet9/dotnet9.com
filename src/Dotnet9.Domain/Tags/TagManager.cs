using Dotnet9.Core;

namespace Dotnet9.Domain.Tags;

public class TagManager
{
    private readonly ITagRepository _tagRepository;

    public TagManager(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Tag> CreateAsync(int? id, string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existTag = await _tagRepository.FindByNameAsync(name);
        if (existTag != null)
        {
            throw new Exception($"存在同名的标签: {name}");
        }

        if (id != null)
        {
            return new Tag(id.Value, name);
        }

        var maxIdOfTag = await _tagRepository.GetMaxIdAsync();
        id = maxIdOfTag + 1;
        return new Tag(id.Value, name);
    }

    public async Task ChangeNameAsync(Tag tag, string newName)
    {
        Check.NotNull(tag, nameof(tag));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existTag = await _tagRepository.FindByNameAsync(newName);
        if (existTag != null && existTag.Id != tag.Id)
        {
            throw new Exception("存在同名的标签");
        }

        tag.ChangeName(newName);
    }
}