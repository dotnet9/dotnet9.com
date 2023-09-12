namespace Dotnet9Tools.Helper;

public static class StringExtensions
{
    /// <summary>
    ///     去掉空格，换行符等
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string RemoveEmpty(this string str)
    {
        return str.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
    }

    /// <summary>
    ///     从左取多个个
    /// </summary>
    /// <param name="str"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string Left(this string str, int len)
    {
        if (str.Length > len)
        {
            return str.Substring(0, len);
        }

        return str;
    }
}