using System.Net;

namespace Dotnet9.Models.Models;

public class MessageModel<T>
{
    public int Status { get; set; } = (int)HttpStatusCode.OK;

    public bool Success { get; set; } = false;

    public string Message { get; set; } = "服务器异常";

    public T? Response { get; set; }
}