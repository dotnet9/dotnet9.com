namespace Dotnet9.AnyDBConfigProvider;

internal static class Helper
{
    public static IDictionary<string, string> Clone(this IDictionary<string, string> dict)
    {
        IDictionary<string, string> newDict = new Dictionary<string, string>();
        foreach (var kv in dict)
        {
            newDict[kv.Key] = kv.Value;
        }

        return newDict;
    }

    public static bool IsChanged(IDictionary<string, string> oldDict,
        IDictionary<string, string> newDict)
    {
        if (oldDict.Count != newDict.Count)
        {
            return true;
        }

        foreach (var oldKV in oldDict)
        {
            var oldKey = oldKV.Key;
            var oldValue = oldKV.Value;
            if (!newDict.ContainsKey(oldKey))
            {
                return true;
            }

            var newValue = newDict[oldKey];
            if (oldValue != newValue)
            {
                return true;
            }
        }

        return false;
    }
}