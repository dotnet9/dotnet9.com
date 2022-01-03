using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Dotnet9.Tags;

public class TagManager : DomainService
{
    private readonly ITagRepository _tagRepository;

    public TagManager(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Tag> CreateAsync([NotNull] string name, string description)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingTag = await _tagRepository.FindByNameAsync(name);
        if (existingTag != null)
        {
            throw new TagAlreadyExistsException(name);
        }

        return new Tag(GuidGenerator.Create(), name, description);
    }

    public async Task ChangeNameAsync([NotNull] Tag tag, [NotNull] string newName)
    {
        Check.NotNull(tag, nameof(tag));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingTag = await _tagRepository.FindByNameAsync(newName);
        if (existingTag != null && existingTag.Id != tag.Id)
        {
            throw new TagAlreadyExistsException(newName);
        }

        tag.ChangeName(newName);
    }
}