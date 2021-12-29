using System;
using Volo.Abp.BlobStoring;

namespace Dotnet9.Abp.BlobStoring.TencentCloud
{
    public static class TencentCloudBlobContainerConfigurationExtensions
    {
        public static TencentCloudBlobProviderConfiguration GetTencentCloudConfiguration(
            this BlobContainerConfiguration containerConfiguration)
        {
            return new TencentCloudBlobProviderConfiguration(containerConfiguration);
        }

        public static BlobContainerConfiguration UseTencentCloud(
            this BlobContainerConfiguration containerConfiguration,
            Action<TencentCloudBlobProviderConfiguration> tencentCloudConfigureAction)
        {
            containerConfiguration.ProviderType = typeof(TencentCloudBlobProvider);
            containerConfiguration.NamingNormalizers.TryAdd<TencentCloudBlobNamingNormalizer>();
            
            tencentCloudConfigureAction(new TencentCloudBlobProviderConfiguration(containerConfiguration));

            return containerConfiguration;
        }
    }
}