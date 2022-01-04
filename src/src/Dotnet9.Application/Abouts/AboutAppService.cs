using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Dotnet9.Abouts;

public class AboutAppService : Dotnet9AppService, IAboutAppService
{
    private readonly IAboutRepository _aboutRepository;

    public AboutAppService(IAboutRepository aboutRepository)
    {
        _aboutRepository = aboutRepository;
    }

    public async Task<AboutDto> GetAsync()
    {
        var about = await _aboutRepository.GetAsync();
        return ObjectMapper.Map<About, AboutDto>(about);
    }

    [Authorize(Dotnet9Permissions.Abouts.Edit)]
    public async Task UpdateAsync(UpdateAboutDto input)
    {
        var about = await _aboutRepository.GetAsync();

        if (about.Details != input.Details)
        {
            about.Details = input.Details;
            await _aboutRepository.UpdateAsync(about, autoSave: true);
        }
    }
}