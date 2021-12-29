using Volo.Abp.BlobStoring;
using Volo.Abp.Modularity;

namespace Dotnet9.Abp.BlobStoring.TencentCloud
{
    [DependsOn(typeof(AbpBlobStoringModule))]
    public class AbpBlobStoringTencentCloudModule : AbpModule
    {
    }
}