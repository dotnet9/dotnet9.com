namespace Dotnet9.EventBus;

internal class RabbitMQConnection
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly object sync_root = new();
    private IConnection _connection;
    private bool _disposed;

    public RabbitMQConnection(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        }

        return _connection.CreateModel();
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _connection.Dispose();
    }

    public bool TryConnect()
    {
        lock (sync_root)
        {
            _connection = _connectionFactory.CreateConnection();

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;
                return true;
            }

            return false;
        }
    }

    private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed)
        {
            return;
        }

        TryConnect();
    }

    private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if (_disposed)
        {
            return;
        }

        TryConnect();
    }

    private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (_disposed)
        {
            return;
        }

        TryConnect();
    }
}