using Dotnet9.Tags;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9;

public class Dotnet9DataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly ITagRepository _tagRepository;
    private readonly TagManager _tagManager;

    public Dotnet9DataSeederContributor(
        ITagRepository tagRepository,
        TagManager tagManager)
    {
        _tagRepository = tagRepository;
        _tagManager = tagManager;
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
    }
}