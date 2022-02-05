namespace Dotnet9.Models.Models.RootTKeys;

public class EntityTKeyDto<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}