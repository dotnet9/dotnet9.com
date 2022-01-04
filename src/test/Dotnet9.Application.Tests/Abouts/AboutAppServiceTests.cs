using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Dotnet9.Abouts;

public sealed class AboutAppServiceTests : Dotnet9ApplicationTestBase
{
    private readonly IAboutAppService _aboutAppService;

    public AboutAppServiceTests()
    {
        _aboutAppService = GetRequiredService<IAboutAppService>();
    }

    [Fact]
    public async Task Should_Get_A_About()
    {
        var result = await _aboutAppService.GetAsync();

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Should_Update_About()
    {
        await _aboutAppService.UpdateAsync(new UpdateAboutDto { Details = "Test" });

        var result = await _aboutAppService.GetAsync();
        result.ShouldNotBeNull();
        result.Details.ShouldBe("Test");
    }
}