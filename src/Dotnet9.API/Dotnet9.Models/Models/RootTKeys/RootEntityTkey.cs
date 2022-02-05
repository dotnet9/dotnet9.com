namespace Dotnet9.Models.Models.RootTKeys;

public class RootEntityTkey<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}