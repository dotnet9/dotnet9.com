using System.Net;

namespace Dotnet9.Models;

public class MessageModel<T>
{
    public int Code { get; set; }

    public string? Message { get; set; }

    public T? Data { get; set; }

    public static MessageModel<T> CreateMessageModel(int code, string message, T data)
    {
        return new MessageModel<T> { Code = code, Message = message, Data = data };
    }

    public static MessageModel<T> Success(string msg)
    {
        return CreateMessageModel((int)HttpStatusCode.OK, msg, default);
    }

    public static MessageModel<T> Success(string msg, T response)
    {
        return CreateMessageModel((int)HttpStatusCode.OK, msg, response);
    }

    public static MessageModel<T> Success(T response)
    {
        return CreateMessageModel((int)HttpStatusCode.OK, "success", response);
    }

    public static MessageModel<T> Fail(string msg)
    {
        return CreateMessageModel((int)HttpStatusCode.BadRequest, msg, default);
    }

    public static MessageModel<T> Fail(string msg, T response)
    {
        return CreateMessageModel((int)HttpStatusCode.BadRequest, msg, response);
    }

    public override string ToString()
    {
        return $"code: {Code}，message：{Message}，data：{Data}";
    }
}