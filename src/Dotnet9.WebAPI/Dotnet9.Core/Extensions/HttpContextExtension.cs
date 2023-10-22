using Furion.RemoteRequest.Extensions;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;

namespace Easy.Core;

public static class HttpContextExtension
{
    /// <summary>
    /// 获取ip
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetRemoteIp(this HttpContext context)
    {
        string ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(ip))
        {
            ip = context.GetRemoteIpAddressToIPv4();
        }

        return ip;
    }

    /// <summary>
    /// 获取Ip所属详细地理位置
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetGeolocation(this HttpContext context)
    {
        try
        {
            string ip = context.GetRemoteIp();
            //获取ip信息
            return GetGeolocation(ip);
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 获取ip详细信息
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static string GetGeolocation(string ip)
    {
        if (string.IsNullOrWhiteSpace(ip))
        {
            return string.Empty;
        }
        try
        {
            //获取ip信息
            byte[] bytes = $"http://whois.pconline.com.cn/ipJson.jsp?ip={ip}&json=true".GetAsByteArrayAsync().GetAwaiter().GetResult();
            string json = Encoding.GetEncoding("gb2312").GetString(bytes);
            return JsonConvert.DeserializeObject<IpInfoDto>(json)?.Address ?? "";
        }
        catch
        {
            return string.Empty;
        }
    }
}

public class IpInfoDto
{
    /// <summary>
    /// ip
    /// </summary>
    [JsonProperty("ip")]
    public string Ip { get; set; }

    /// <summary>
    /// 省份
    /// </summary>
    [JsonProperty("pro")]
    public string Province { get; set; }

    /// <summary>
    /// 省编码
    /// </summary>
    [JsonProperty("proCode")]
    public string ProCode { get; set; }

    /// <summary>
    /// 城市
    /// </summary>
    [JsonProperty("city")]
    public string City { get; set; }

    /// <summary>
    /// 城市编码
    /// </summary>
    [JsonProperty("cityCode")]
    public string CityCode { get; set; }

    /// <summary>
    /// 区域
    /// </summary>
    [JsonProperty("region")]
    public string Region { get; set; }

    /// <summary>
    /// 区编码
    /// </summary>
    [JsonProperty("regionCode")]
    public string RegionCode { get; set; }

    /// <summary>
    /// IP归属地
    /// </summary>
    [JsonProperty("addr")]
    public string Address { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("regionNames")]
    public string RegionNames { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    [JsonProperty("err")]
    public string Error { get; set; }
}