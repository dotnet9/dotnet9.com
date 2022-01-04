using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dotnet9.Abouts;

public interface IAboutAppService : IApplicationService
{
    Task<AboutDto> GetAsync();

    Task UpdateAsync(UpdateAboutDto input);
}