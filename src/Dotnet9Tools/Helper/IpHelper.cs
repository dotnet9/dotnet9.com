using System.Reflection;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;

namespace Dotnet9Tools.Helper;

public class IpHelper
{
    public static CityResponse? City(string ipString)
    {
        try
        {
            string? dic = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (dic != null)
            {
                string path = Path.Combine(dic, "MaxMindDb", "GeoLite2-City.mmdb");
                using DatabaseReader reader = new DatabaseReader(path);
                CityResponse response = reader.City(ipString);
                return response;
            }

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static AsnResponse? Asn(string ipString)
    {
        try
        {
            string? dic = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (dic != null)
            {
                string path = Path.Combine(dic, "MaxMindDb", "GeoLite2-ASN.mmdb");
                using DatabaseReader reader = new DatabaseReader(path);
                AsnResponse response = reader.Asn(ipString);
                return response;
            }

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}