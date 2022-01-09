using Dotnet9.Tags;
using System;
using System.Threading.Tasks;
using Dotnet9.Abouts;
using Dotnet9.Albums;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9;

public class Dotnet9DataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly ITagRepository _tagRepository;
    private readonly TagManager _tagManager;
    private readonly IAlbumRepository _albumRepository;
    private readonly AlbumManager _albumManager;
    private readonly IAboutRepository _aboutRepository;

    public Dotnet9DataSeederContributor(
        ITagRepository tagRepository,
        TagManager tagManager,
        IAlbumRepository albumRepository,
        AlbumManager albumManager,
        IAboutRepository aboutRepository)
    {
        _tagRepository = tagRepository;
        _tagManager = tagManager;
        _albumRepository = albumRepository;
        _albumManager = albumManager;
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

        if (await _albumRepository.GetCountAsync() <= 0)
        {
            await _albumRepository.InsertAsync(
                await _albumManager.CreateAsync("WPF", "https://img1.dotnet9.com/album_wpf.png",
                    "WPF open source project")
            );
            await _albumRepository.InsertAsync(
                await _albumManager.CreateAsync("Winform", "https://img1.dotnet9.com/album_winform.png",
                    "Winform open source project")
            );
        }

        if (await _aboutRepository.GetCountAsync() <= 0)
        {
            await _aboutRepository.InsertAsync(new About
                { Details = "This is the content of the \"about\" page, which is actually saved as markdown text;" });
        }
    }
}