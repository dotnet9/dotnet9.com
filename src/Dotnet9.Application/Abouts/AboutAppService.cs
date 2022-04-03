using AutoMapper;
using Dotnet9.Application.Contracts.Abouts;
using Dotnet9.Domain.Abouts;

namespace Dotnet9.Application.Abouts;

public class AboutAppService : IAboutAppService
{
    private readonly IAboutRepository _aboutRepository;
    private readonly IMapper _mapper;

    public AboutAppService(IAboutRepository aboutRepository, IMapper mapper)
    {
        _aboutRepository = aboutRepository;
        _mapper = mapper;
    }

    public async Task<AboutDto?> GetAsync()
    {
        var about = await _aboutRepository.GetAsync(x => x.Id > 0);
        return about == null ? null : _mapper.Map<About, AboutDto>(about);
    }

    public async Task<bool> UpdateAsync(AboutDto aboutDto)
    {
        var about = await _aboutRepository.GetAsync(x => x.Id > 0);
        if (about == null)
        {
            about = _mapper.Map<AboutDto, About>(aboutDto);
            await _aboutRepository.InsertAsync(about);
        }
        else
        {
            about.Content = aboutDto.Content;
            await _aboutRepository.UpdateAsync(about);
        }

        return true;
    }
}