using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Ratings;

public class Rating : FullAuditedAggregateRoot<Guid>
{
    public virtual short StarCount { get; protected set; }
}