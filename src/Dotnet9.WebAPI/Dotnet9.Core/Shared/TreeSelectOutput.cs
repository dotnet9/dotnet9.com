namespace Dotnet9.Core.Shared;
/// <summary>
/// 树形下拉框
/// </summary>
public class TreeSelectOutput : SelectOutput
{
    /// <summary>
    /// 子选项
    /// </summary>
    public List<TreeSelectOutput>? Children { get; set; }
}