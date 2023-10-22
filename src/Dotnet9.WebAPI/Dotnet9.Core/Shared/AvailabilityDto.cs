using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Shared;

public class AvailabilityDto : KeyDto
{
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
}