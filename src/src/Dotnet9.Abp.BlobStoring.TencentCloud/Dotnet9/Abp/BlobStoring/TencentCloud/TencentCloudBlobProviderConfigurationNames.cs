namespace Dotnet9.Abp.BlobStoring.TencentCloud
{
    public static class TencentCloudBlobProviderConfigurationNames
    {
        public const string ContainerName = "TencentCloud.ContainerName";
        public const string CreateContainerIfNotExists = "TencentCloud.CreateContainerIfNotExists";

        public const string AppId = "TencntCloud.AppId";
        public const string SecretId = "TencentCloud.SecretId";
        public const string SecretKey = "TencentCloud.SecretKey";

        public const string Region = "TencentCloud.Region";
        public const string ConnectionTimeout = "TencentCloud.ConnectionTimeout";
        public const string ReadWriteTimeout = "TencentCloud.ReadWriteTimeout";
        public const string KeyDurationSecond = "TencentCloud.KeyDurationSecond";
    }
}