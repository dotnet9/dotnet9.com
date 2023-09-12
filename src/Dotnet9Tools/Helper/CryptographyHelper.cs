using System.Security.Cryptography;
using System.Text;

namespace Dotnet9Tools.Helper;

/// <summary>
///     加、解密帮助类
/// </summary>
public static class CryptographyHelper
{
    /// <summary>
    ///     计算32位MD5
    /// </summary>
    /// <param name="str">需要计算的字符串</param>
    /// <returns>32位的MD5值</returns>
    public static string Md5(this string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        return bytes.Md5();
    }

    public static string Md5(this byte[] bytes)
    {
        MD5 md5 = MD5.Create();
        byte[] targetData = md5.ComputeHash(bytes);
        return BitConverter.ToString(targetData).Replace("-", "").ToLower();
    }

    /// <summary>
    ///     计算8位MD5
    /// </summary>
    /// <param name="str">需要计算的字符串</param>
    /// <returns>8位的MD5值</returns>
    public static string Md5Short8(this string str)
    {
        return Md5(str).Substring(8, 16).ToLower();
    }

    /// <summary>
    ///     DES加密
    /// </summary>
    /// <param name="key">秘钥</param>
    /// <param name="str">需要加密的字符串</param>
    /// <returns>加密后的字符串</returns>
    public static string ToDes(string key, string str)
    {
        using DES cryptoProvider = DES.Create();
        byte[] bytes = Encoding.ASCII.GetBytes(key);
        ICryptoTransform crypto = cryptoProvider.CreateEncryptor(bytes, bytes);
        MemoryStream ms = new MemoryStream();
        CryptoStream cst = new CryptoStream(ms, crypto, CryptoStreamMode.Write);
        StreamWriter sw = new StreamWriter(cst);
        sw.Write(str);
        sw.Flush();
        cst.FlushFinalBlock();
        sw.Flush();
        return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
    }

    #region SHA256加密算法

    /// <summary>
    ///     SHA256函数
    /// </summary>
    /// <param name="str">原始字符串</param>
    /// <returns>SHA256结果(返回长度为44字节的字符串)</returns>
    public static string GetSHA256(this string str)
    {
        byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
        using SHA256 Sha256 = SHA256.Create();
        byte[] Result = Sha256.ComputeHash(SHA256Data);
        return Convert.ToBase64String(Result); //返回长度为44字节的字符串
    }

    #endregion

    /// <summary>
    ///     AES加密算法
    /// </summary>
    /// <param name="input">明文字符串</param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns>字符串</returns>
    public static string EncryptByAES(string input, string key, string iv)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        using Aes rijndaelManaged = Aes.Create();
        rijndaelManaged.Mode = CipherMode.CBC;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        rijndaelManaged.FeedbackSize = 128;
        rijndaelManaged.Key = Encoding.UTF8.GetBytes(key);
        rijndaelManaged.IV = Encoding.UTF8.GetBytes(iv);
        ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(input);
                }

                byte[] bytes = msEncrypt.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }
    }

    /// <summary>
    ///     AES解密
    /// </summary>
    /// <param name="input">密文字节数组</param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns>返回解密后的字符串</returns>
    public static string DecryptByAES(string input, string key, string iv)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        byte[] buffer = Convert.FromBase64String(input);
        using Aes rijndaelManaged = Aes.Create();
        rijndaelManaged.Mode = CipherMode.CBC;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        rijndaelManaged.FeedbackSize = 128;
        rijndaelManaged.Key = Encoding.UTF8.GetBytes(key);
        rijndaelManaged.IV = Encoding.UTF8.GetBytes(iv);
        ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
        using (MemoryStream msEncrypt = new MemoryStream(buffer))
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srEncrypt = new StreamReader(csEncrypt))
                {
                    return srEncrypt.ReadToEnd();
                }
            }
        }
    }

    #region Base64加密解密

    /// <summary>
    ///     Base64是一種使用64基的位置計數法。它使用2的最大次方來代表僅可列印的ASCII 字元。
    ///     這使它可用來作為電子郵件的傳輸編碼。在Base64中的變數使用字元A-Z、a-z和0-9 ，
    ///     這樣共有62個字元，用來作為開始的64個數字，最後兩個用來作為數字的符號在不同的
    ///     系統中而不同。
    ///     Base64加密
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Base64Encrypt(this string str)
    {
        byte[] encbuff = Encoding.UTF8.GetBytes(str);
        return Convert.ToBase64String(encbuff);
    }

    /// <summary>
    ///     Base64解密
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Base64Decrypt(this string str)
    {
        byte[] decbuff = Convert.FromBase64String(str);
        return Encoding.UTF8.GetString(decbuff);
    }

    #endregion
}