namespace Dotnet9.Application.File.Dtos;

public class UploadFileOutput
{
    /// <summary>
    /// 文件名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 附件链接
    /// </summary>
    public string Url { get; set; }
}