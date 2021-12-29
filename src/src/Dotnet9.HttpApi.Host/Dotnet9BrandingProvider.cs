using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Dotnet9
{
    [Dependency(ReplaceServices = true)]
    public class Dotnet9BrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Dotnet9";
    }
}
