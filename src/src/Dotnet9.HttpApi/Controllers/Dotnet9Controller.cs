using Dotnet9.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dotnet9.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class Dotnet9Controller : AbpControllerBase
    {
        protected Dotnet9Controller()
        {
            LocalizationResource = typeof(Dotnet9Resource);
        }
    }
}