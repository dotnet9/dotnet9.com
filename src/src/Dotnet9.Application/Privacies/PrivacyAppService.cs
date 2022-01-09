using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Privacies;

public class PrivacyAppService : Dotnet9AppService, IPrivacyAppService
{
    private readonly IRepository<Privacy, Guid> _privacyRepository;

    public PrivacyAppService(IRepository<Privacy, Guid> privacyRepository)
    {
        _privacyRepository = privacyRepository;
    }


    public async Task<PrivacyDto> GetAsync()
    {
        var privacy = await _privacyRepository.GetAsync(x => x.IsDeleted == false);
        return ObjectMapper.Map<Privacy, PrivacyDto>(privacy);
    }

    [Authorize(Dotnet9Permissions.Privacies.Edit)]
    public async Task UpdateAsync(UpdatePrivacyDto input)
    {
        var privacy = await _privacyRepository.GetAsync(x => x.IsDeleted == false);

        if (privacy.Details != input.Details)
        {
            privacy.Details = input.Details;
            await _privacyRepository.UpdateAsync(privacy, autoSave: true);
        }
    }
}