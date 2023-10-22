using HashidsNet;
using Yitter.IdGenerator;

namespace Easy.Core;

public static class YitIdHelperExtension
{
    /// <summary>
    /// 盐值
    /// </summary>
    private const string Salt = "dkjsDLVK1S7y5be8XYzoq0C2nHaEmBfA";

    /// <summary>
    /// 注册雪花ID服务（默认配置）
    /// </summary>
    /// <param name="service"></param>
    public static void AddIdGenerator(this IServiceCollection service)
    {
        service.AddIdGenerator(new IdGeneratorOptions(0));
    }

    /// <summary>
    /// 注册雪花ID服务（自定义配置）
    /// </summary>
    /// <param name="service"></param>
    /// <param name="sectionName"></param>
    public static void AddIdGenerator(this IServiceCollection service, string sectionName)
    {
        var options = App.GetConfig<IdGeneratorOptions>(sectionName) ?? new IdGeneratorOptions(0);
        options.BaseTime = options.BaseTime.ToUniversalTime();
        YitIdHelper.SetIdGenerator(options);
        service.AddSingleton<IIdGenerator>(new DefaultIdGenerator(options));
    }

    /// <summary>
    /// 注册雪花ID服务（自定义配置）
    /// </summary>
    /// <param name="service"></param>
    /// <param name="options">配置</param>
    public static void AddIdGenerator(this IServiceCollection service, IdGeneratorOptions options)
    {
        options.BaseTime = options.BaseTime.ToUniversalTime();
        YitIdHelper.SetIdGenerator(options);
        service.AddSingleton<IIdGenerator>(new DefaultIdGenerator(options));
    }

    /// <summary>
    /// 生成ID
    /// </summary>
    /// <param name="generator"></param>
    /// <returns></returns>
    public static long NextId(this IIdGenerator generator)
    {
        return generator.NewLong();
    }

    /// <summary>
    /// 加密ID（生成短ID）
    /// </summary>
    /// <param name="generator"></param>
    /// <param name="id">long类型ID</param>
    /// <param name="salt">盐值</param>
    /// <returns></returns>
    public static string Encode(this IIdGenerator generator, long id, string salt = Salt)
    {
        return new Hashids(salt).EncodeLong(id);
    }

    /// <summary>
    /// 解密加密ID（解密短ID）
    /// </summary>
    /// <param name="generator"></param>
    /// <param name="text">密文</param>
    /// <param name="salt">盐值</param>
    /// <returns></returns>
    public static long Decode(this IIdGenerator generator, string text, string salt = Salt)
    {
        return new Hashids(salt).DecodeSingleLong(text);
    }
}