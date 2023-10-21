namespace Dotnet9.WebAPI.Extensions;

public static class CaptchaExtension
{
    /// <summary>
    /// 添加图形验证码
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection InstallCaptcha(this IServiceCollection services, IConfiguration configuration)
    {
        //图形验证码
        services.AddCaptcha(configuration, option =>
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

            option.ImageOption.TextBold = true; // 粗体，该配置2.0.3新增
        });
        return services;
    }
}