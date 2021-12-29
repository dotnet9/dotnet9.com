using System;
using COSXML;
using COSXML.Auth;

namespace Dotnet9.Abp.BlobStoring.TencentCloud.Infrastructure
{
    public class CosServerWrapObject
    {
        public CosXmlServer CosXmlServer { get; protected set; }

        public CosServerWrapObject(TencentCloudBlobProviderConfiguration configuration)
        {
            CosXmlServer = new CosXmlServer(
                BuildConfig(configuration),
                GetCredentialProvider(configuration));
        }

        private CosXmlConfig BuildConfig(TencentCloudBlobProviderConfiguration configuration)
        {
            return new CosXmlConfig.Builder()
                .SetConnectionLimit(TimeSpan.FromSeconds(configuration.ConnectionTimeout).TotalMilliseconds.To<int>())
                .SetReadWriteTimeoutMs(TimeSpan.FromSeconds(configuration.ReadWriteTimeout).TotalMilliseconds.To<int>())
                .IsHttps(true)
                .SetAppid(configuration.AppId)
                .SetRegion(configuration.Region)
                .SetDebugLog(true)
                .Build();
        }

        private QCloudCredentialProvider GetCredentialProvider(TencentCloudBlobProviderConfiguration configuration)
        {
            return new DefaultQCloudCredentialProvider(
                configuration.SecretId,
                configuration.SecretKey,
                configuration.KeyDurationSecond);
        }
    }
}