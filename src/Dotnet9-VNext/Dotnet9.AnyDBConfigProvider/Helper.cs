namespace Dotnet9.AnyDBConfigProvider;

internal static class Helper
{
    public static IDictionary<string, string> Clone(this IDictionary<string, string> dict)
    {
        IDictionary<string, string> newDict = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> kv in dict)
        {
            newDict[kv.Key] = kv.Value;
        }

        return newDict;
    }

    public static bool IsChanged(IDictionary<string, string> oldDict, IDictionary<string, string> newDict)
    {
        if (oldDict.Count != newDict.Count)
        {
            return true;
        }

        foreach ((string? oldKey, string? oldValue) in oldDict)
        {
            if (!newDict.ContainsKey(oldKey))
            {
                return true;
            }

            string newValue = newDict[oldKey];
            if (oldValue != newValue)
            {
                return true;
            }
        }

        return false;
    }
}