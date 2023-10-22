using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;
/// <summary>
/// 可用状态
/// </summary>
public interface IAvailability
{
    /// <summary>
    /// 可用状态
    /// </summary>
    AvailabilityStatus Status { get; set; }
}