using Dotnet9.Tags;
using System;
using System.Threading.Tasks;
using Dotnet9.Abouts;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9;

public class Dotnet9DataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly ITagRepository _tagRepository;
    private readonly TagManager _tagManager;
    private readonly IAboutRepository _aboutRepository;

    public Dotnet9DataSeederContributor(
        ITagRepository tagRepository,
        TagManager tagManager,
        IAboutRepository aboutRepository)
    {
        _tagRepository = tagRepository;
        _tagManager = tagManager;
        _aboutRepository = aboutRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _tagRepository.GetCountAsync() <= 0)
        {
            await _tagRepository.InsertAsync(
                await _tagManager.CreateAsync("C#", "Language")
            );
            await _tagRepository.InsertAsync(
                await _tagManager.CreateAsync("C++", "Language")
            );
        }

        if (await _aboutRepository.GetCountAsync() <= 0)
        {
            await _aboutRepository.InsertAsync(new About { Details = "This is the content of the \"about\" page, which is actually saved as markdown text;" });
        }
    }
}