# dotnet9.com

一个使用abp vnext + blazor(C#)开发的博客网站

## 种子数据

本站的文章（转载、原创)都使用mardown保存，并存放在目录：`.\doc`，使用种子数据时需要修改以下类配置，暂时使用的本地绝对路径：

```C#
public class ConfigurationConst
{
    public const string AboutFilePath = "F://github_gitee/dotnet9.com//doc/blog_contents/about.md";
    public const string BlogBaseFilePath = "F://github_gitee/dotnet9.com/doc/blog_contents/base.info";
    public const string BlogPostDirPath = "F://github_gitee/dotnet9.com/doc/blog_contents/uploads";
}
```

## 1. 对象存储

暂时使用本地配置文件的方式保存密钥，后面可考虑加到项目配置文件中或者机密文件。

存储文件名如：F://TencentCloud.json

存储内容格式：

```json
{
  "ContainerName": "img2-xxxxx",
  "CreateContainerIfNotExists": false,
  "AppId": "xxx",
  "SecretId": "AKIDPd8NL3UeWxxxxOxEExPaU7",
  "SecretKey": "NTIp55oGSN1OVxxxxOJxKQGp",
  "Region": "ap-ssssng",
  "ConnectionTimeout": 600,
  "ReadWriteTimeout": "",
  "KeyDurationSecond": 600
}
```

读取密钥`Dotnet9DomainModule.cs`的方法`ConfigureServices`

```C#
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
```