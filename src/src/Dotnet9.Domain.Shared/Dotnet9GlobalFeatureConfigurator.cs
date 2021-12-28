using Volo.Abp.GlobalFeatures;
using Volo.Abp.Threading;

namespace Dotnet9
{
    public static class Dotnet9GlobalFeatureConfigurator
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
	            GlobalFeatureManager.Instance.Modules.CmsKit(cmsKit =>
	            {
		            cmsKit.EnableAll();
	            });

            });
        }
    }
}
