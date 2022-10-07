namespace Dotnet9.Core;

public static class Check
{
    public static T NotNull<T>(T? value, string parameterName)
    {
        if (value == null) throw new ArgumentNullException(parameterName);

        return value;
    }

    public static T NotNull<T>(T? value, string parameterName, string message)
    {
        if (value == null) throw new ArgumentNullException(parameterName, message);

        return value;
    }

    public static string NotNull(string? value, string parameterName, int maxLength = int.MaxValue, int minLength = 0)
    {
        if (value == null) throw new ArgumentException(parameterName + " 不能为null!", parameterName);

        if (value.Length > maxLength)
            throw new ArgumentException($"{parameterName} 长度必须小于或等于 {maxLength}!", parameterName);

        if (minLength > 0 && value.Length < minLength)
            throw new ArgumentException($"{parameterName} 长度必须大于或等于 {minLength}!", parameterName);

        return value;
    }

    public static string NotNullOrWhiteSpace(string? value, string parameterName, int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (value.IsNullOrWhiteSpace()) throw new ArgumentException(parameterName + " 不能为null，空，空白字符!", parameterName);

        if (value!.Length > maxLength)
            throw new ArgumentException($"{parameterName} 长度必须小于或等于 {maxLength}!", parameterName);

        if (minLength > 0 && value.Length < minLength)
            throw new ArgumentException($"{parameterName} 长度必须大于或等于 {minLength}!", parameterName);

        return value;
    }

    public static string NotNullOrEmpty(string? value, string parameterName, int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (value.IsNullOrEmpty()) throw new ArgumentException(parameterName + " 不能为null或者空!", parameterName);

        if (value!.Length > maxLength)
            throw new ArgumentException($"{parameterName} 长度必须小于或等于 {maxLength}!", parameterName);

        if (minLength > 0 && value.Length < minLength)
            throw new ArgumentException($"{parameterName} 长度必须大于或等于 {minLength}!", parameterName);

        return value;
    }

    public static ICollection<T> NotNullOrEmpty<T>(ICollection<T>? value, string parameterName)
    {
        if (value.IsNullOrEmpty()) throw new ArgumentException(parameterName + " 不能为null或者空!", parameterName);

        return value!;
    }

    public static string? Length(string? value, string parameterName, int maxLength, int minLength = 0)
    {
        if (minLength > 0)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException(parameterName + " 不能为null或者空!", parameterName);

            if (value.Length < minLength)
                throw new ArgumentException($"{parameterName} 长度必须大于或等于 {minLength}!", parameterName);
        }

        if (value != null && value.Length > maxLength)
            throw new ArgumentException($"{parameterName} 长度必须小于或等于 {maxLength}!", parameterName);

        return value;
    }
}