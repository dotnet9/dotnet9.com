using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Dotnet9.AnyDBConfigProvider;

public class DbConfigurationProvider : ConfigurationProvider, IDisposable
{
    private readonly DbConfigOptions _options;
    private bool _isDisposed;
    private readonly ReaderWriterLockSlim _lockObj = new();

    public DbConfigurationProvider(DbConfigOptions options)
    {
        _options = options;
        TimeSpan interval = TimeSpan.FromSeconds(3);
        if (options.ReloadInterval != null)
        {
            interval = options.ReloadInterval.Value;
        }

        if (options.ReloadOnChange)
        {
            ThreadPool.QueueUserWorkItem(obj =>
            {
                while (!_isDisposed)
                {
                    Load();
                    Thread.Sleep(interval);
                }
            });
        }
    }

    public void Dispose()
    {
        _isDisposed = true;
    }

    public override IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string? parentPath)
    {
        _lockObj.EnterReadLock();

        try
        {
            return base.GetChildKeys(earlierKeys, parentPath);
        }
        finally
        {
            _lockObj.ExitReadLock();
        }
    }

    public override bool TryGet(string key, out string? value)
    {
        _lockObj.EnterReadLock();

        try
        {
            return base.TryGet(key, out value);
        }
        finally
        {
            _lockObj.ExitReadLock();
        }
    }

    public override void Load()
    {
        base.Load();
        IDictionary<string, string>? clonedData = null;
        try
        {
            _lockObj.EnterWriteLock();
            clonedData = Data.Clone();
            string tableName = _options.TableName;
            Data.Clear();
            using (IDbConnection conn = _options.CreateDbConnection())
            {
                conn.Open();
                DoLoad(tableName, conn);
            }
        }
        catch (DbException)
        {
            //if DbException is thrown, restore to the original data.
            Data = clonedData;
            throw;
        }
        finally
        {
            _lockObj.ExitWriteLock();
        }

        //OnReload cannot be between EnterWriteLock and ExitWriteLock, or "A read lock may not be acquired with the write lock held in this mode" will be thrown.
        if (Helper.IsChanged(clonedData, Data))
        {
            OnReload();
        }
    }

    private void DoLoad(string tableName, IDbConnection conn)
    {
        using (IDbCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText =
                $"select Name,Value from {tableName} where Id in(select Max(Id) from {tableName} group by Name)";
            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    string? value = reader.GetString(1);
                    if (value == null)
                    {
                        Data[name] = value;
                        continue;
                    }

                    value = value.Trim();
                    //if the value is like [...] or {} , it may be a json array value or json object value,
                    //so try to parse it as json
                    if ((value.StartsWith("[") && value.EndsWith("]"))
                        || (value.StartsWith("{") && value.EndsWith("}")))
                    {
                        TryLoadAsJson(name, value);
                    }
                    else
                    {
                        Data[name] = value;
                    }
                }
            }
        }
    }

    private void LoadJsonElement(string name, JsonElement jsonRoot)
    {
        if (jsonRoot.ValueKind == JsonValueKind.Array)
        {
            int index = 0;
            foreach (JsonElement item in jsonRoot.EnumerateArray())
            {
                //https://andrewlock.net/creating-a-custom-iconfigurationprovider-in-asp-net-core-to-parse-yaml/
                //parse as "a:b:0"="hello";"a:b:1"="world"
                string path = name + ConfigurationPath.KeyDelimiter + index;
                LoadJsonElement(path, item);
                index++;
            }
        }
        else if (jsonRoot.ValueKind == JsonValueKind.Object)
        {
            foreach (JsonProperty jsonObj in jsonRoot.EnumerateObject())
            {
                string pathOfObj = name + ConfigurationPath.KeyDelimiter + jsonObj.Name;
                LoadJsonElement(pathOfObj, jsonObj.Value);
            }
        }
        else
        {
            //if it is not json array or object, parse it as plain string value
            Data[name] = jsonRoot.GetValueForConfig();
        }
    }

    private void TryLoadAsJson(string name, string value)
    {
        JsonDocumentOptions jsonOptions = new JsonDocumentOptions
            { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip };
        try
        {
            JsonElement jsonRoot = JsonDocument.Parse(value, jsonOptions).RootElement;
            LoadJsonElement(name, jsonRoot);
        }
        catch (JsonException ex)
        {
            //if it is not valid json, parse it as plain string value
            Data[name] = value;
            Debug.WriteLine($"When trying to parse {value} as json object, exception was thrown. {ex}");
        }
    }
}