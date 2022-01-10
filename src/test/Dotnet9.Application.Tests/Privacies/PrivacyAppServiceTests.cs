using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Dotnet9.Privacies;

public sealed class PrivacyAppServiceTests : Dotnet9ApplicationTestBase
{
    private readonly IPrivacyAppService _privacyAppService;

    public PrivacyAppServiceTests()
    {
        _privacyAppService = GetRequiredService<IPrivacyAppService>();
    }

    [Fact]
    public async Task Should_Get_A_Privacy()
    {
        var result = await _privacyAppService.GetAsync();

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Should_Update_Privacy()
    {
        await _privacyAppService.UpdateAsync(new UpdatePrivacyDto { Details = "Test update privacy" });

        var result = await _privacyAppService.GetAsync();
        result.ShouldNotBeNull();
        result.Details.ShouldBe("Test update privacy");
    }
}