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
						cos.AppId = "1258841763";
						cos.SecretId = "AKIDJRKwINBj14qMcbJkvpSoujHYYHjwub1A";
						cos.SecretKey = "tc4F9nAG5evvf9LHwKTN0MSIr9BwQTFl";
						cos.Region = "ap-beijing";
						cos.KeyDurationSecond = 600;
						cos.ConnectionTimeout = 600;
						cos.ContainerName = "img2-dotnet9";
						cos.CreateContainerIfNotExists = false;
					});
				});
			});

#if DEBUG
			context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
		}
	}
}