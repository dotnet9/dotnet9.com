namespace IdentityService.Infrastructure.Services;

internal class SendCloudEmailSender : IEmailSender
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SendCloudEmailSender> _logger;
    private readonly IOptionsSnapshot<SendCloudEmailSettings> _sendCloudSettings;

    public SendCloudEmailSender(ILogger<SendCloudEmailSender> logger, IHttpClientFactory httpClientFactory,
        IOptionsSnapshot<SendCloudEmailSettings> sendCloudSettings)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _sendCloudSettings = sendCloudSettings;
    }

    public async Task SendAsync(string toEmail, string subject, string body)
    {
        _logger.LogInformation($"SendCloud Email sent to {toEmail} with subject {subject} and body {body}");
        Dictionary<string, string> postBody = new()
        {
            { "from", _sendCloudSettings.Value.From! },
            { "to", toEmail },
            { "subject", subject },
            { "html", body }
        };
        postBody["apiUser"] = _sendCloudSettings.Value.ApiUser!;
        postBody["apiKey"] = _sendCloudSettings.Value.ApiKey!;

        using FormUrlEncodedContent httpContent = new(postBody);
        HttpClient httpClient = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMsg =
            await httpClient.PostAsync("https://api.sendcloud.net/apiv2/mail/send", httpContent);
        if (!responseMsg.IsSuccessStatusCode)
        {
            throw new Exception($"发送邮件响应码错误：{responseMsg.StatusCode}");
        }

        string respBody = await responseMsg.Content.ReadAsStringAsync();
        SendCloudResponseModel? respModel = respBody.ParseJson<SendCloudResponseModel>();
        if (!respModel!.Result)
        {
            throw new Exception($"发送邮件响应返回失败，状态码：{respModel.StatusCode}，消息：{respModel.Message}");
        }
    }
}