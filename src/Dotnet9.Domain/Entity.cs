using Dotnet9.Core;

namespace Dotnet9.Domain;

public abstract class Entity : IEntity
{
    public abstract object[] GetKeys();

    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Keys = {GetKeys().JoinAsString(",")}";
    }
}