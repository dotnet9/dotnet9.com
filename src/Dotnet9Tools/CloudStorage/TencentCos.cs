using COSXML;
using COSXML.Auth;
using COSXML.Transfer;

namespace Dotnet9Tools.CloudStorage;

public class TencentCos
{
    public TencentCos(string sECRET_ID, string sECRET_KEY)
    {
        SECRET_ID = sECRET_ID;
        SECRET_KEY = sECRET_KEY;
    }

    private string SECRET_ID { get; }

    public string SECRET_KEY { get; set; }


    /// <summary>
    ///     上传文件
    /// </summary>
    /// <param name="region"></param>
    /// <param name="bucketName"></param>
    /// <param name="localFile"></param>
    /// <param name="cosFile"></param>
    /// <returns></returns>
    public async Task Upload(string region, string bucketName, string localFile, string cosFile)
    {
        string secretId = SECRET_ID;
        string secretKey = SECRET_KEY;
        long durationSecond = 600; //每次请求签名有效时长，单位为秒
        QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
            secretId, secretKey, durationSecond);
        CosXmlConfig? config = new CosXmlConfig.Builder()
            .IsHttps(true) //设置默认 HTTPS 请求
            .SetRegion(region) //设置一个默认的存储桶地域
            .SetDebugLog(true) //显示日志
            .Build(); //创建 CosXmlConfig 对象
        CosXml cosXml = new CosXmlServer(config, cosCredentialProvider);

        // 初始化 TransferConfig
        TransferConfig transferConfig = new TransferConfig();

        // 初始化 TransferManager
        TransferManager transferManager = new TransferManager(cosXml, transferConfig);

        string bucket = bucketName; //存储桶，格式：BucketName-APPID
        string cosPath = cosFile; //对象在存储桶中的位置标识符，即称对象键
        string srcPath = localFile; //本地文件绝对路径

        // 上传对象
        COSXMLUploadTask uploadTask = new(bucket, cosPath);
        uploadTask.SetSrcPath(srcPath);

        uploadTask.progressCallback = delegate(long completed, long total)
        {
            Console.WriteLine("progress = {0:##.##}%", completed * 100.0 / total);
        };

        try
        {
            COSXMLUploadTask.UploadTaskResult? result = await
                transferManager.UploadAsync(uploadTask);
            Console.WriteLine(result.GetResultInfo());
            string? eTag = result.eTag;
        }
        catch (Exception e)
        {
            Console.WriteLine("CosException: " + e);
        }
    }
}