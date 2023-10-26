namespace Dotnet9.Application.Client.Dtos;

public class PicturesQueryInput : Pagination
{
    /// <summary>
    /// 模块封面ID
    /// </summary>
    public long CoverId { get; set; }
}