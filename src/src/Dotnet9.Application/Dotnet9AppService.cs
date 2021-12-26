using System;
using System.Collections.Generic;
using System.Text;
using Dotnet9.Localization;
using Volo.Abp.Application.Services;

namespace Dotnet9
{
    /* Inherit your application services from this class.
     */
    public abstract class Dotnet9AppService : ApplicationService
    {
        protected Dotnet9AppService()
        {
            LocalizationResource = typeof(Dotnet9Resource);
        }
    }
}
