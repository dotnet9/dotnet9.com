using HashidsNet;

namespace Dotnet9.Core;

public class HashidsUtil
{
    public static string GetHashids(string sourceStr,int number=9)
    {
        var hashids = new Hashids(sourceStr);
        return hashids.Encode(number);
    }
}