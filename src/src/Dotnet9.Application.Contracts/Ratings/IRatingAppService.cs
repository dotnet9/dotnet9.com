using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dotnet9.Ratings;

public interface IRatingAppService : IApplicationService
{
    Task<RatingDto> GetAsync(Guid id);

    Task<PagedResultDto<RatingDto>> GetListAsync(GetRatingListDto input);

    Task<RatingDto> CreateAsync(CreateRatingDto input);

    Task UpdateAsync(Guid id, UpdateRatingDto input);

    Task DeleteAsync(Guid id);
}