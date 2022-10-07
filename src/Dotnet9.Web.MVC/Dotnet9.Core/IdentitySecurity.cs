using System.Security.Cryptography;

namespace Dotnet9.Core;

public class IdentitySecurity
{
    public static string HashPassword(string password)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));

        byte[] salt;
        byte[] bytes;
        using (Rfc2898DeriveBytes rfc2898DeriveBytes = new(password, 16, 1000))
        {
            salt = rfc2898DeriveBytes.Salt;
            bytes = rfc2898DeriveBytes.GetBytes(32);
        }

        var array = new byte[49];
        Buffer.BlockCopy(salt, 0, array, 1, 16);
        Buffer.BlockCopy(bytes, 0, array, 17, 32);
        return Convert.ToBase64String(array);
    }

    public static bool VerifyHashedPassword(string hashedPassword, string password)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword)) return false;

        if (password == null) throw new ArgumentNullException(nameof(password));

        var array = Convert.FromBase64String(hashedPassword);
        if (array.Length != 49 || array[0] != 0) return false;

        var array2 = new byte[16];
        Buffer.BlockCopy(array, 1, array2, 0, 16);
        var array3 = new byte[32];
        Buffer.BlockCopy(array, 17, array3, 0, 32);
        byte[] bytes;
        using (Rfc2898DeriveBytes rfc2898DeriveBytes = new(password, array2, 1000))
        {
            bytes = rfc2898DeriveBytes.GetBytes(32);
        }

        return ByteArraysEqual(array3, bytes);
    }

    private static bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (ReferenceEquals(a, b)) return true;

        if (a == null || b == null || a.Length != b.Length) return false;

        var flag = true;
        for (var i = 0; i < a.Length; i++) flag &= a[i] == b[i];

        return flag;
    }

    /// <summary>
    ///     计算密码强度
    /// </summary>
    /// <param name="password">密码字符串</param>
    /// <returns></returns>
    public static Strength PasswordStrength(string password)
    {
        //空字符串强度值为0
        if (password == "") return Strength.Invalid;
        //字符统计
        int iNum = 0, iLtt = 0, iSym = 0;
        foreach (var c in password)
            switch (c)
            {
                case >= '0' and <= '9':
                    iNum++;
                    break;
                case >= 'a' and <= 'z':
                case >= 'A' and <= 'Z':
                    iLtt++;
                    break;
                default:
                    iSym++;
                    break;
            }

        if (iLtt == 0 && iSym == 0) return Strength.Weak; //纯数字密码
        if (iNum == 0 && iLtt == 0) return Strength.Weak; //纯符号密码
        if (iNum == 0 && iSym == 0) return Strength.Weak; //纯字母密码
        if (password.Length <= 6) return Strength.Weak; //长度不大于6的密码
        if (iLtt == 0) return Strength.Normal; //数字和符号构成的密码
        if (iSym == 0) return Strength.Normal; //数字和字母构成的密码
        if (iNum == 0) return Strength.Normal; //字母和符号构成的密码
        return password.Length <= 10 ? Strength.Normal : Strength.Strong;
    }
}

/// <summary>
///     密码强度
/// </summary>
public enum Strength
{
    /// <summary>
    ///     无效密码
    /// </summary>
    Invalid = 0,

    /// <summary>
    ///     低强度密码
    /// </summary>
    Weak = 1,

    /// <summary>
    ///     中强度密码
    /// </summary>
    Normal = 2,

    /// <summary>
    ///     高强度密码
    /// </summary>
    Strong = 3
}