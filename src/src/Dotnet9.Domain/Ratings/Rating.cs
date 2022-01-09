using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Ratings;

public class Rating : FullAuditedAggregateRoot<Guid>
{
    public short StarCount { get; set; }
}