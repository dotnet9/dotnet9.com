using System.Security.Cryptography;
using System.Text;

namespace Dotnet9.Commons;

public static class HashHelper
{
    private static string ToHashString(byte[] bytes)
    {
        StringBuilder builder = new();
        foreach (byte t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();
    }

    public static string ComputeSha256Hash(Stream stream)
    {
        using SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(stream);
        return ToHashString(bytes);
    }

    public static string ComputeSha256Hash(string input)
    {
        using SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        return ToHashString(bytes);
    }

    public static string ComputeMd5Hash(string input)
    {
        using MD5 md5Hash = MD5.Create();
        byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        return ToHashString(bytes);
    }

    public static string ComputeMd5Hash(Stream input)
    {
        using MD5 md5Hash = MD5.Create();
        byte[] bytes = md5Hash.ComputeHash(input);
        return ToHashString(bytes);
    }
}