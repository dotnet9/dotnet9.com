namespace Dotnet9.WebAPI.Infrastructure.Services;

public class SendCloudSmsSender : ISmsSender
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<SendCloudSmsSender> logger;
    private readonly IOptionsSnapshot<SendCloudSmsSettings> smsSettings;

    public SendCloudSmsSender(ILogger<SendCloudSmsSender> logger,
        IHttpClientFactory httpClientFactory,
        IOptionsSnapshot<SendCloudSmsSettings> smsSettings)
    {
        this.logger = logger;
        this.httpClientFactory = httpClientFactory;
        this.smsSettings = smsSettings;
    }

    public async Task SendAsync(string phoneNum, params string[] args)
    {
        logger.LogInformation("Send Sms to {0},args:{1}", phoneNum, string.Join("|", args));
        var postBody = new Dictionary<string, string>();
        postBody["smsUser"] = smsSettings.Value.SmsUser;
        postBody["templateId"] = "10010";
        postBody["phone"] = phoneNum;
        postBody["vars"] = args.ToJsonString();

        var signature = CalcSignature(postBody);
        postBody["signature"] = signature;
        using (var httpContent = new FormUrlEncodedContent(postBody))
        {
            var httpClient = httpClientFactory.CreateClient();
            var responseMsg = await httpClient.PostAsync("http://www.sendcloud.net/smsapi/send", httpContent);
            if (!responseMsg.IsSuccessStatusCode)
            {
                throw new ApplicationException($"发送短信响应码错误:{responseMsg.StatusCode}");
            }

            var respBody = await responseMsg.Content.ReadAsStringAsync();
            var respModel = respBody.ParseJson<SendCloudResponseModel>();
            if (!respModel.Result)
            {
                throw new ApplicationException($"发送短信失败：{respModel.Message}");
            }
        }
    }

    private string CalcSignature(IEnumerable<KeyValuePair<string, string>> parameters)
    {
        var smsKey = smsSettings.Value.SmsKey;
        var orderedItems = parameters.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key}={kv.Value}");
        var orginParams = string.Join('&', orderedItems);
        var signStr = $"{smsKey}&{orginParams}&{smsKey}";
        var signature = HashHelper.ComputeMd5Hash(signStr);
        return signature;
    }
}