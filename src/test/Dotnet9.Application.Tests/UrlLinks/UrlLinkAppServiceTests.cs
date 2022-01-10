using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dotnet9.UrlLinks;

public sealed class UrlLinkAppServiceTests : Dotnet9ApplicationTestBase
{
    private readonly IUrlLinkAppService _urlLinkAppService;

    public UrlLinkAppServiceTests()
    {
        _urlLinkAppService = GetRequiredService<IUrlLinkAppService>();
    }

    [Fact]
    public async Task Should_Get_All_UrlLink_Without_Any_Filter()
    {
        var result = await _urlLinkAppService.GetListAsync(new GetUrlLinkListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(urlLink => urlLink.Name == "乐趣课堂lequ.co");
    }

    [Fact]
    public async Task Should_Get_Filtered_UrlLink()
    {
        var result = await _urlLinkAppService.GetListAsync(
            new GetUrlLinkListDto { Filter = "lequ.co" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(urlLink => urlLink.Name == "乐趣课堂lequ.co");
    }

    [Fact]
    public async Task Should_Create_A_New_UrlLink()
    {
        var urlLinkDto = await _urlLinkAppService.CreateAsync(
            new CreateUrlLinkDto
            {
                Name = "Dotnet9",
                Url = "https://dotnet9.com",
                Description = "test UrlLink"
            }
        );

        urlLinkDto.Id.ShouldNotBe(Guid.Empty);
        urlLinkDto.Name.ShouldBe("Dotnet9");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Name_UrlLink()
    {
        await Assert.ThrowsAsync<UrlLinkNameAlreadyExistsException>(async () =>
        {
            await _urlLinkAppService.CreateAsync(
                new CreateUrlLinkDto
                {
                    Name = "乐趣课堂lequ.co",
                    Url = "https://dotnet9.com",
                    Description = "test"
                }
            );
        });
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Url_UrlLink()
    {
        await Assert.ThrowsAsync<UrlLinkUrlAlreadyExistsException>(async () =>
        {
            await _urlLinkAppService.CreateAsync(
                new CreateUrlLinkDto
                {
                    Name = "dotnet9.com",
                    Url = "https://lequ.co",
                    Description = "test"
                }
            );
        });
    }
}