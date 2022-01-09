using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Ratings;

public class RatingAppService : Dotnet9AppService, IRatingAppService
{
    private readonly IRepository<Rating, Guid> _ratingRepository;

    public RatingAppService(IRepository<Rating, Guid> ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task<RatingDto> GetAsync(Guid id)
    {
        var rating = await _ratingRepository.GetAsync(id);
        return ObjectMapper.Map<Rating, RatingDto>(rating);
    }

    public async Task<PagedResultDto<RatingDto>> GetListAsync(GetRatingListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Rating.StarCount);
        }

        var queryable = await _ratingRepository.GetQueryableAsync();

        var query = from rating in queryable.AsQueryable() select rating;

        query = query
            .OrderBy(input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(query);

        var totalCount = await _ratingRepository.GetCountAsync();

        return new PagedResultDto<RatingDto>(totalCount,
            ObjectMapper.Map<List<Rating>, List<RatingDto>>(queryResult));
    }

    [Authorize(Dotnet9Permissions.Ratings.Create)]
    public async Task<RatingDto> CreateAsync(CreateRatingDto input)
    {
        var rating = ObjectMapper.Map<CreateRatingDto, Rating>(input);

        await _ratingRepository.InsertAsync(rating);

        return ObjectMapper.Map<Rating, RatingDto>(rating);
    }

    [Authorize(Dotnet9Permissions.Ratings.Edit)]
    public async Task UpdateAsync(Guid id, UpdateRatingDto input)
    {
        var rating = await _ratingRepository.GetAsync(id);

        rating.StarCount = input.StarCount;

        await _ratingRepository.UpdateAsync(rating);
    }

    [Authorize(Dotnet9Permissions.Ratings.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _ratingRepository.DeleteAsync(id);
    }
}