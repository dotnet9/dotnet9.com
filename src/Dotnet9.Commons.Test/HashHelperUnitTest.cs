using System.Security.Cryptography;
using System.Text;

namespace Dotnet9.Commons.Test;

[TestClass]
public class HashHelperUnitTest
{
    [TestMethod]
    public void Hashids_Success()
    {
        var blogPostSlugStr = "Is-it-possible-to-use-it-as-a-short-link-generator-Hashidsnet";

        var encodeStr1 = blogPostSlugStr.GetHashids();
        var encodeStr2 = blogPostSlugStr.GetHashids();

        Assert.AreEqual(encodeStr1, encodeStr2);
    }


    [TestMethod]
    public void Hashids_Best_Success()
    {
        var blogPostSlugStr = "Is-it-possible-to-use-it-as-a-short-link-generator-Hashidsnet";

        var encodeStr1 = blogPostSlugStr.GetHashids();
        var encodeStr2 = ShortenString(blogPostSlugStr);
        var encodeStr3 = ShortenString2(blogPostSlugStr);

        Assert.IsTrue(encodeStr1.Length < encodeStr2.Length, "Hashids生成的短字符串比Base64还短");

        Assert.IsTrue(encodeStr1.Length < encodeStr3.Length, "Hashids生成的短字符串还是短那么一点点");
    }

    public static string ShortenString(string longString)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(longString);
        string shortString = Convert.ToBase64String(bytes);
        return shortString;
    }

    public static string RestoreString(string shortString)
    {
        byte[] bytes = Convert.FromBase64String(shortString);
        string longString = Encoding.UTF8.GetString(bytes);
        return longString;
    }

    public static string ShortenString2(string longString)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(longString));
            string shortString = Convert.ToBase64String(hashBytes);
            return shortString;
        }
    }
}