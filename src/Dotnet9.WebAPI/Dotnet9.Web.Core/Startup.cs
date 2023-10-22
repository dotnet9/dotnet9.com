using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SqlSugar;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Dotnet9.Core.Logging;
using Dotnet9.Core.Options;
using Easy.Core;
using Furion.Logging;
using Lazy.Captcha.Core;
using Lazy.Captcha.Core.Generator;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MrHuo.OAuth;
using MrHuo.OAuth.QQ;
using OnceMi.AspNetCore.OSS;

namespace Dotnet9.Web.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //解决Encoding无法使用gb2312编码问题
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        //添加自定义选项
        services.AddCustomOptions();
        services.AddJwt<JwtHandler>(enableGlobalAuthorize: true, jwtBearerConfigure: options =>
        {
            options.Events = new JwtBearerEvents()
            {
                OnMessageReceived = context =>
                {
                    if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                    {
                        //读取cookie中的token
                        string token = context.HttpContext.Request.Cookies["access-token"]?.Trim('"');
                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            context.Token = token;
                        }
                    }

                    return Task.CompletedTask;
                }
            };
        });
        services.AddConsoleFormatter();


        // 全局日志 文档：http://furion.baiqian.ltd/docs/logging/#18116-%E8%BE%93%E5%87%BA-json-%E6%94%AF%E6%8C%81%E5%BF%BD%E7%95%A5%E5%B1%9E%E6%80%A7%E5%90%8D%E6%88%96%E5%B1%9E%E6%80%A7%E7%B1%BB%E5%9E%8B
        services.AddMonitorLogging(options =>
        {
            options.IgnorePropertyNames = new[] { "Byte" };
            options.IgnorePropertyTypes = new[] { typeof(byte[]) };
            options.ConfigureLogger((logger, logContext, context) =>
            {
                var httpContext = context.HttpContext;

                logContext.Set("ip", httpContext.GetRemoteIp());
            });
        });

        //允许跨域
        services.AddCorsAccessor();

        //配置缓存 文档：https://easycaching.readthedocs.io/en/latest/
        services.AddEasyCaching(options =>
        {
            //options.UseCSRedis(App.Configuration);
            //options.WithJson("DefaultCSRedis");
            options.UseInMemory(App.Configuration);
            options.WithJson("DefaultInMemory");
        });

        // 配置ORM 文档：https://www.donet5.com/Home/Doc
        services.AddSqlSugar();

        //远程请求 文档：https://furion.baiqian.ltd/docs/http
        services.AddRemoteRequest();

        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // 首字母小写（驼峰样式）
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; // 时间格式化
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // 忽略循环引用
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;//忽略空值
            })
                .AddInjectWithUnifyResult();

        //雪花id 文档：https://github.com/yitter/IdGenerator
        services.AddIdGenerator(App.GetConfig<SnowIdOptions>("SnowId"));

        //注册事件总线 文档：https://furion.baiqian.ltd/docs/event-bus
        services.AddEventBus();

        // 配置限流
        services.Configure<IpRateLimitOptions>(App.Configuration.GetSection("IpRateLimiting"));
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        // 数据库日志
        // 文档：http://furion.baiqian.ltd/docs/logging#1871-%E5%9F%BA%E7%A1%80%E4%BD%BF%E7%94%A8
        services.AddDatabaseLogging<DatabaseLoggingWriter>(options =>
        {
            options.WithStackFrame = true; // 显示堆栈信息
            options.WithTraceId = true; // 显示线程Id
            options.IgnoreReferenceLoop = false; // 忽略循环检测
            options.WriteFilter = (logMsg) => logMsg.LogName == "System.Logging.LoggingMonitor";
        });

        //模板引擎 文档：https://furion.baiqian.ltd/docs/view-engine
        services.AddViewEngine();

        // OSS文档：https://github.com/oncemi/OnceMi.AspNetCore.OSS
        var ossOptions = App.GetConfig<OssConnectionOptions>("OssConnection");
        services.AddOSSService(options =>
        {
            ossOptions.Adapt(options);
        });
        var auth = new QQOAuth(OAuthConfig.LoadFrom(App.Configuration, "oauth:qq"));
        services.AddSingleton(auth);

        #region 图形验证码

        //图形验证码
        services.AddCaptcha(App.Configuration, option =>
        {
            option.CaptchaType = CaptchaType.WORD_NUMBER_LOWER; // 验证码类型
            option.CodeLength = 4; // 验证码长度, 要放在CaptchaType设置后.  当类型为算术表达式时，长度代表操作的个数
            option.ExpirySeconds = 60; // 验证码过期时间
            option.IgnoreCase = true; // 比较时是否忽略大小写
            option.StoreageKeyPrefix = ""; // 存储键前缀

            option.ImageOption.Animation = true; // 是否启用动画
            option.ImageOption.FrameDelay = 300; // 每帧延迟,Animation=true时有效, 默认30

            option.ImageOption.Width = 132; // 验证码宽度
            option.ImageOption.Height = 40; // 验证码高度
            //option.ImageOption.BackgroundColor = SixLabors.ImageSharp.Color.White; // 验证码背景色

            option.ImageOption.BubbleCount = 2; // 气泡数量
            option.ImageOption.BubbleMinRadius = 5; // 气泡最小半径
            option.ImageOption.BubbleMaxRadius = 15; // 气泡最大半径
            option.ImageOption.BubbleThickness = 1; // 气泡边沿厚度

            option.ImageOption.InterferenceLineCount = 2; // 干扰线数量

            option.ImageOption.FontSize = 36; // 字体大小
            option.ImageOption.FontFamily = DefaultFontFamilys.Instance.Actionj; // 字体

            /* 
             * 中文使用kaiti，其他字符可根据喜好设置（可能部分转字符会出现绘制不出的情况）。
             * 当验证码类型为“ARITHMETIC”时，不要使用“Ransom”字体。（运算符和等号绘制不出来）
             */

            option.ImageOption.TextBold = true;// 粗体，该配置2.0.3新增
        });

        #endregion
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // 添加状态码拦截中间件
        app.UseUnifyResultStatusCodes();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCorsAccessor();

        app.UseIpRateLimiting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseInject(string.Empty);

        //解决全局异常拦截器中无法拿到请求的内容
        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next.Invoke();
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
