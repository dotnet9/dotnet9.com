using System;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Dotnet9.Ratings;

public sealed class RatingAppServiceTests : Dotnet9ApplicationTestBase
{
    private readonly IRatingAppService _ratingAppService;

    public RatingAppServiceTests()
    {
        _ratingAppService = GetRequiredService<IRatingAppService>();
    }

    [Fact]
    public async Task Should_Create_A_New_Rating()
    {
        var ratingDto = await _ratingAppService.CreateAsync(
            new CreateRatingDto
            {
                StarCount = 5
            }
        );

        ratingDto.Id.ShouldNotBe(Guid.Empty);
    }
}