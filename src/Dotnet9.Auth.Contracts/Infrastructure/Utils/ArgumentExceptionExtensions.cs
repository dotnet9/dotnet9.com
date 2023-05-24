namespace Dotnet9.Auth.Contracts.Infrastructure.Utils;

public static class ArgumentExceptionExtensions
{
    public static T ThrowIfDefault<T>([NotNull] T? argument,
        [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument is null || argument.Equals(default(T)))
        {
            throw new UserFriendlyException($"Please provider {paramName},{paramName} is required");
        }

        return argument;
    }

    public static string ThrowIfNullOrEmpty([NotNull] string? argument,
        [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (string.IsNullOrEmpty(argument))
        {
            throw new UserFriendlyException($"Please provider {paramName},{paramName} is required");
        }

        return argument;
    }
}