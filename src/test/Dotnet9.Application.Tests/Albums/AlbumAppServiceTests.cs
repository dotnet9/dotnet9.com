using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dotnet9.Albums;

public sealed class AlbumAppServiceTests : Dotnet9ApplicationTestBase
{
    private readonly IAlbumAppService _albumAppService;

    public AlbumAppServiceTests()
    {
        _albumAppService = GetRequiredService<IAlbumAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Albums_Without_Any_Filter()
    {
        var result = await _albumAppService.GetListAsync(new GetAlbumListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(album => album.Name == "WPF");
        result.Items.ShouldContain(album => album.Name == "Winform");
    }

    [Fact]
    public async Task Should_Get_Filtered_Albums()
    {
        var result = await _albumAppService.GetListAsync(
            new GetAlbumListDto { Filter = "WPF" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(album => album.Name == "WPF");
    }

    [Fact]
    public async Task Should_Create_A_New_Album()
    {
        var authorDto = await _albumAppService.CreateAsync(
            new CreateAlbumDto
            {
                Name = "SwiftUI",
                CoverImageUrl = "https://img1.dotnet9.com/album_swiftui.png",
                Description = "Open source SwiftUI project"
            }
        );

        authorDto.Id.ShouldNotBe(Guid.Empty);
        authorDto.Name.ShouldBe("SwiftUI");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Album()
    {
        await Assert.ThrowsAsync<AlbumAlreadyExistsException>(async () =>
        {
            await _albumAppService.CreateAsync(
                new CreateAlbumDto
                {
                    Name = "WPF",
                    CoverImageUrl = "https://img1.dotnet9.com/test.png",
                    Description = ".NET"
                }
            );
        });
    }
}