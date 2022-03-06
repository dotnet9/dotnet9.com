using System.Security.Cryptography;
using System.Text;

namespace Dotnet9.Tools;

public static class DesHelper
{
    private const string DesKey = "toolnet9";
    private const string DesIv = "toolnet9";

    public static string DesEncrypt(string data, string key = DesKey, string iv = DesIv)
    {
        var byKey = Encoding.ASCII.GetBytes(key);
        var byIV = Encoding.ASCII.GetBytes(iv);

        var cryptoProvider = new DESCryptoServiceProvider();
        var i = cryptoProvider.KeySize;
        var ms = new MemoryStream();
        var cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

        var sw = new StreamWriter(cst);
        sw.Write(data);
        sw.Flush();
        cst.FlushFinalBlock();
        sw.Flush();
        return Convert.ToBase64String(ms.GetBuffer(), 0, (int) ms.Length);
    }

    public static string DesDecrypt(string data, string key = DesKey, string iv = DesIv)
    {
        var byKey = Encoding.ASCII.GetBytes(key);
        var byIV = Encoding.ASCII.GetBytes(iv);

        byte[] byEnc;
        try
        {
            byEnc = Convert.FromBase64String(data);
        }
        catch
        {
            return null;
        }

        var cryptoProvider = new DESCryptoServiceProvider();
        var ms = new MemoryStream(byEnc);
        var cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
        var sr = new StreamReader(cst);
        return sr.ReadToEnd();
    }
}