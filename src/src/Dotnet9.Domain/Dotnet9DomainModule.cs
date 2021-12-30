using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using Dotnet9.Abp.BlobStoring.TencentCloud;
using Dotnet9.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BlobStoring;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Dotnet9
{
    [DependsOn(
        typeof(Dotnet9DomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpEmailingModule),
        typeof(AbpBlobStoringTencentCloudModule)
    )]
    [DependsOn(typeof(AbpBlobStoringModule))]
    public class Dotnet9DomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = MultiTenancyConsts.IsEnabled; });
            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(container =>
                {
                    container.UseTencentCloud(cos =>
                    {
                        var cosJsonFile = "F://TencentCloud.json";
                        if (File.Exists(cosJsonFile))
                        {
                            try
                            {
                                var jsonStr = File.ReadAllText(cosJsonFile);
                                var localCos =
                                    JsonSerializer.Deserialize<Dictionary<string, object>>(jsonStr);
                                cos.AppId = localCos["AppId"].ToString();
                                cos.SecretId = localCos["SecretId"].ToString();
                                cos.SecretKey = localCos["SecretKey"].ToString();
                                cos.Region = localCos["Region"].ToString();
                                cos.KeyDurationSecond = int.Parse(localCos["KeyDurationSecond"].ToString());
                                cos.ConnectionTimeout = int.Parse(localCos["ConnectionTimeout"].ToString());
                                cos.ContainerName = localCos["ContainerName"].ToString();
                                cos.CreateContainerIfNotExists =
                                    bool.Parse(localCos["CreateContainerIfNotExists"].ToString());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                        }
                    });
                });
            });

#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
        }
    }
}