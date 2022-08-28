using System.Security.Cryptography;

namespace Dotnet9.Web.Utils;

public class CryptographyHelper
{
    public static string Md5Hash(string str)
    {
        var md5 = MD5.Create();
        var s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
        return s.Aggregate("", (current, t) => current + t.ToString("X"));
    }
}