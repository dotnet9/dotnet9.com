using AutoMapper;
using Dotnet9.Application.Contracts.Privacies;
using Dotnet9.Domain.Privacies;

namespace Dotnet9.Application.Privacies;

public class PrivacyAppService : IPrivacyAppService
{
    private readonly IPrivacyRepository _privacyRepository;
    private readonly IMapper _mapper;

    public PrivacyAppService(IPrivacyRepository privacyRepository, IMapper mapper)
    {
        _privacyRepository = privacyRepository;
        _mapper = mapper;
    }

    public async Task<PrivacyDto?> GetAsync()
    {
        var privacy = await _privacyRepository.GetAsync(x => x.Id > 0);
        return privacy == null ? null : _mapper.Map<Privacy, PrivacyDto>(privacy);
    }

    public async Task<bool> UpdateAsync(PrivacyDto privacyDto)
    {
        var privacy = await _privacyRepository.GetAsync(x => x.Id > 0);
        if (privacy == null)
        {
            privacy = _mapper.Map<PrivacyDto, Privacy>(privacyDto);
            await _privacyRepository.InsertAsync(privacy);
        }
        else
        {
            privacy.Content = privacyDto.Content;
            await _privacyRepository.UpdateAsync(privacy);
        }

        return true;
    }
}