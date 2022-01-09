using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Ratings;

public class RatingDto : EntityDto<Guid>
{
    public short StartCount { get; set; }
}