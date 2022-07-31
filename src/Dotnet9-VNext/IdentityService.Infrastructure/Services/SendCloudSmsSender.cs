namespace IdentityService.Infrastructure.Services;

internal class SendCloudSmsSender : ISmsSender
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SendCloudSmsSender> _logger;
    private readonly IOptionsSnapshot<SendCloudSmsSettings> _smsSettings;

    public SendCloudSmsSender(ILogger<SendCloudSmsSender> logger,
        IHttpClientFactory httpClientFactory,
        IOptionsSnapshot<SendCloudSmsSettings> smsSettings)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _smsSettings = smsSettings;
    }

    public async Task SendAsync(string toPhoneNumber, params string[] args)
    {
        _logger.LogInformation($"Send Sms to {toPhoneNumber}, args:{string.Join("|", args)}");
        Dictionary<string, string> postBody = new Dictionary<string, string>
        {
            ["smsUser"] = _smsSettings.Value.SmsUser!,
            ["templateId"] = "10010",
            ["phone"] = toPhoneNumber,
            ["vars"] = args.ToJsonString()
        };

        string signature = CalcSignature(postBody);
        postBody["signature"] = signature;
        using FormUrlEncodedContent httpContent = new FormUrlEncodedContent(postBody);
        HttpClient httpClient = _httpClientFactory.CreateClient();
        HttpResponseMessage responseMsg =
            await httpClient.PostAsync("http://www.sendcloud.net/smsapi/send", httpContent);
        if (!responseMsg.IsSuccessStatusCode)
        {
            throw new ApplicationException($"发送短信响应码错误：{responseMsg.StatusCode}");
        }

        string respBody = await responseMsg.Content.ReadAsStringAsync();
        SendCloudResponseModel? respModel = respBody.ParseJson<SendCloudResponseModel>();
        if (!respModel!.Result)
        {
            throw new ApplicationException($"发送短信失败：{respModel.Message}");
        }
    }

    private string CalcSignature(IEnumerable<KeyValuePair<string, string>> parameters)
    {
        string? smsKey = _smsSettings.Value.SmsKey;
        IEnumerable<string> orderedItems = parameters.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key}={kv.Value}");
        string originParams = string.Join('&', orderedItems);
        string signStr = $"{smsKey}&{originParams}&{smsKey}";
        string signature = HashHelper.ComputeMd5Hash(signStr);
        return signature;
    }
}