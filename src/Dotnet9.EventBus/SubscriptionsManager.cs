namespace Dotnet9.EventBus;

internal class SubscriptionsManager
{
    //key是eventName，值是监听这个事件的实现了IIntegrationEventHandler接口的类型
    private readonly Dictionary<string, List<Type>> _handlers = new();

    public bool IsEmpty => !_handlers.Keys.Any();

    public event EventHandler<string> OnEventRemoved;

    public void Clear()
    {
        _handlers.Clear();
    }

    /// <summary>
    ///     把eventHandlerType类型（实现了eventHandlerType接口）注册为监听了eventName事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="eventHandlerType"></param>
    public void AddSubscription(string eventName, Type eventHandlerType)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            _handlers.Add(eventName, new List<Type>());
        }

        //如果已经注册过，则报错
        if (_handlers[eventName].Contains(eventHandlerType))
        {
            throw new ArgumentException($"Handler Type {eventHandlerType} already registered for '{eventName}'",
                nameof(eventHandlerType));
        }

        _handlers[eventName].Add(eventHandlerType);
    }

    public void RemoveSubscription(string eventName, Type handlerType)
    {
        _handlers[eventName].Remove(handlerType);
        if (!_handlers[eventName].Any())
        {
            _handlers.Remove(eventName);
            OnEventRemoved?.Invoke(this, eventName);
        }
    }

    /// <summary>
    ///     得到名字为eventName的所有监听者
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public IEnumerable<Type> GetHandlersForEvent(string eventName)
    {
        return _handlers[eventName];
    }

    /// <summary>
    ///     是否有类型监听eventName这个事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public bool HasSubscriptionsForEvent(string eventName)
    {
        return _handlers.ContainsKey(eventName);
    }
}