using System.Security.Cryptography;
using System.Text;

namespace Dotnet9.Common.Helpers;

public class Md5Helper
{
    public static string Md5Encrypt16(string password)
    {
        var md5 = MD5.Create();
        var t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
        t2 = t2.Replace("-", string.Empty);
        return t2;
    }

    public static string Md5Encrypt32(string password = "")
    {
        var pwd = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(password) && !string.IsNullOrWhiteSpace(password))
            {
                var md5 = MD5.Create();

                var s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                pwd = s.Aggregate(pwd, (current, item) => string.Concat(current, item.ToString("X2")));
            }
        }
        catch
        {
            throw new Exception($"错误的 password 字符串:【{password}】");
        }

        return pwd;
    }

    public static string Md5Encrypt64(string password)
    {
        var md5 = MD5.Create();
        var s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(s);
    }
}