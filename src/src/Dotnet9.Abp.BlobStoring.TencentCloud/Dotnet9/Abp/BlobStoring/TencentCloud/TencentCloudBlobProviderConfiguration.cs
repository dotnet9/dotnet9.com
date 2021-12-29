using Volo.Abp.BlobStoring;

namespace Dotnet9.Abp.BlobStoring.TencentCloud
{
    public class TencentCloudBlobProviderConfiguration
    {
        public string AppId
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>(TencentCloudBlobProviderConfigurationNames.AppId);
            set => _containerConfiguration.SetConfiguration(TencentCloudBlobProviderConfigurationNames.AppId, value);
        }

        public string SecretId
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>(TencentCloudBlobProviderConfigurationNames.SecretId);
            set => _containerConfiguration.SetConfiguration(TencentCloudBlobProviderConfigurationNames.SecretId, value);
        }

        public string SecretKey
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>(TencentCloudBlobProviderConfigurationNames.SecretKey);
            set => _containerConfiguration.SetConfiguration(TencentCloudBlobProviderConfigurationNames.SecretKey, value);
        }

        public string Region
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>(TencentCloudBlobProviderConfigurationNames.Region);
            set => _containerConfiguration.SetConfiguration(TencentCloudBlobProviderConfigurationNames.Region, value);
        }


        public int ConnectionTimeout
        {
            get => _containerConfiguration.GetConfigurationOrDefault<int>(TencentCloudBlobProviderConfigurationNames.ConnectionTimeout);
            set => _containerConfiguration.SetConfiguration(TencentCloudBlobProviderConfigurationNames.ConnectionTimeout, value);
        }

        public int ReadWriteTimeout
        {
            get => _containerConfiguration.GetConfigurationOrDefault<int>(TencentCloudBlobProviderConfigurationNames.ReadWriteTimeout);
            set => _containerConfiguration.SetConfiguration(TencentCloudBlobProviderConfigurationNames.ReadWriteTimeout, value);
        }

        public int KeyDurationSecond
        {
            get => _containerConfiguration.GetConfigurationOrDefault<int>(TencentCloudBlobProviderConfigurationNames.KeyDurationSecond);
            set => _containerConfiguration.SetConfiguration(TencentCloudBlobProviderConfigurationNames.KeyDurationSecond, value);
        }

        public string ContainerName
        {
            get => _containerConfiguration.GetConfigurationOrDefault<string>(TencentCloudBlobProviderConfigurationNames.ContainerName);
            set => _containerConfiguration.SetConfiguration(TencentCloudBlobProviderConfigurationNames.ContainerName, value);
        }

        public bool CreateContainerIfNotExists
        {
            get => _containerConfiguration.GetConfigurationOrDefault<bool>(TencentCloudBlobProviderConfigurationNames.CreateContainerIfNotExists);
            set => _containerConfiguration.SetConfiguration(TencentCloudBlobProviderConfigurationNames.CreateContainerIfNotExists, value);
        }

        private readonly BlobContainerConfiguration _containerConfiguration;

        public TencentCloudBlobProviderConfiguration(BlobContainerConfiguration containerConfiguration)
        {
            _containerConfiguration = containerConfiguration;
        }
    }
}