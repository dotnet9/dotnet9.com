namespace Dotnet9.WebAPI.EFCore.Services;

public class SendCloudEmailSender : IEmailSender
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SendCloudEmailSender> _logger;
    private readonly IOptionsSnapshot<SendCloudEmailSettings> _sendCloudSettings;

    public SendCloudEmailSender(ILogger<SendCloudEmailSender> logger,
        IHttpClientFactory httpClientFactory,
        IOptionsSnapshot<SendCloudEmailSettings> sendCloudSettings)
    {
        this._logger = logger;
        this._httpClientFactory = httpClientFactory;
        this._sendCloudSettings = sendCloudSettings;
    }

    public async Task SendAsync(string toEmail, string subject, string body)
    {
        _logger.LogInformation("SendCloud Email to {0},subject:{1},body:{2}", toEmail, subject, body);
        var postBody = new Dictionary<string, string>();
        postBody["apiUser"] = _sendCloudSettings.Value.ApiUser!;
        postBody["apiKey"] = _sendCloudSettings.Value.ApiKey!;
        postBody["from"] = _sendCloudSettings.Value.From!;
        postBody["to"] = toEmail;
        postBody["subject"] = subject;
        postBody["html"] = body;

        using var httpContent = new FormUrlEncodedContent(postBody);
        var httpClient = _httpClientFactory.CreateClient();
        var responseMsg = await httpClient.PostAsync("https://api.sendcloud.net/apiv2/mail/send", httpContent);
        if (!responseMsg.IsSuccessStatusCode)
        {
            throw new Exception($"发送邮件响应码错误:{responseMsg.StatusCode}");
        }

        var respBody = await responseMsg.Content.ReadAsStringAsync();
        var respModel = respBody.ParseJson<SendCloudResponseModel>();
        if (respModel is { Result: false })
        {
            throw new Exception($"发送邮件响应返回失败，状态码：{respModel.StatusCode},消息：{respModel.Message}");
        }
    }
}