namespace Dotnet9.Core.Shared;
/// <summary>
/// 下拉框
/// </summary>
public class SelectOutput
{
    /// <summary>
    /// 文本
    /// </summary>
    public string Label { get; set; } = null!;

    /// <summary>
    /// 值
    /// </summary>
    public long Value { get; set; }
}