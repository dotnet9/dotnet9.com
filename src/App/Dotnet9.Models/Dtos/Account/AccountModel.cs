using Dotnet9Tools.Helper;
using MaxMind.GeoIP2.Responses;
using UAParser;

namespace Dotnet9.Models.Dtos.Account;

internal class AccountModel
{
}

public class AccountListRequest : BasePageModel
{
}

public class AccountItemDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime? LastUpdateTime { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public List<string> RoleName { get; set; } = new();

    public DateTime LockedTime { get; set; }

    public bool IsLocked => LockedTime > DateTime.Now;
}

public class AccountLoginRecordDto
{
    public string AccountName { get; set; }

    public string Ip { get; set; }

    public string IpCity
    {
        get
        {
            CityResponse? response = IpHelper.City(Ip);
            if (response == null)
            {
                return Ip;
            }


            string str =
                $"{response.Continent.Names["zh-CN"]}/{response.Country.Names["zh-CN"]}/{response.City.Names["zh-CN"]}";
            AsnResponse? asn = IpHelper.Asn(Ip);

            if (asn != null)
            {
                str += $"[{asn.AutonomousSystemOrganization}]";
            }

            return str;
        }
    }

    public string UserAgent { get; set; }

    /// <summary>
    ///     浏览器和操作系统
    /// </summary>
    public string BrowserOs
    {
        get
        {
            ClientInfo? client = Parser.GetDefault().Parse(UserAgent);
            return $"{client.OS}-{client.UA}";
        }
    }

    public DateTime CreateTime { get; set; }
}