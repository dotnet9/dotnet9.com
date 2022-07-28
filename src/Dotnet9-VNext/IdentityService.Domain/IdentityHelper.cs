namespace IdentityService.Domain;

public static class IdentityHelper
{
    public static string SumErrors(this IEnumerable<IdentityError> errors)
    {
        IEnumerable<string> strs = errors.Select(e => $"code={e.Code},message={e.Description}");
        return string.Join('\n', strs);
    }
}