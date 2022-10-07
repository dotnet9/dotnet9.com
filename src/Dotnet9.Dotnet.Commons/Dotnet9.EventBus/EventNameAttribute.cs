namespace Dotnet9.EventBus;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class EventNameAttribute : Attribute
{
    public EventNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
}