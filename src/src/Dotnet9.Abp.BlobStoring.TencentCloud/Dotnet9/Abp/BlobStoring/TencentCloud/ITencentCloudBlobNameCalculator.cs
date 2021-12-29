using Volo.Abp.BlobStoring;

namespace Dotnet9.Abp.BlobStoring.TencentCloud
{
    public interface ITencentCloudBlobNameCalculator
    {
        string Calculate(BlobProviderArgs args);
    }
}