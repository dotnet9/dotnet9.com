using Dotnet9.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Dotnet9.Blazor
{
    public abstract class Dotnet9ComponentBase : AbpComponentBase
    {
        protected Dotnet9ComponentBase()
        {
            LocalizationResource = typeof(Dotnet9Resource);
        }
    }
}
