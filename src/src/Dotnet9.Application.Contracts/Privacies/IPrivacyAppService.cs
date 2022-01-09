using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dotnet9.Privacies;

public interface IPrivacyAppService : IApplicationService
{
    Task<PrivacyDto> GetAsync();

    Task UpdateAsync(UpdatePrivacyDto input);
}