using Microsoft.JSInterop;

namespace Dotnet9.Tools.Utils;

public class CookieStorage
{
    private readonly IJSRuntime _jsRuntime;

    public CookieStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetItemAsync(string key, string? value)
    {
        await _jsRuntime.InvokeVoidAsync("setCookie", key, value);
    }

    public async Task<string> GetItemAsync(string key)
    {
        return await _jsRuntime.InvokeAsync<string>("getCookie", key);
    }
}