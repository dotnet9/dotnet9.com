using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dotnet9.Tags;

public sealed class TagAppServiceTests : Dotnet9ApplicationTestBase
{
    private readonly ITagAppService _tagAppService;

    public TagAppServiceTests()
    {
        _tagAppService = GetRequiredService<ITagAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Tags_Without_Any_Filter()
    {
        var result = await _tagAppService.GetListAsync(new GetTagListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(author => author.Name == "C#");
        result.Items.ShouldContain(author => author.Name == "C++");
    }

    [Fact]
    public async Task Should_Get_Filtered_Tags()
    {
        var result = await _tagAppService.GetListAsync(
            new GetTagListDto { Filter = "C#" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(author => author.Name == "C#");
    }

    [Fact]
    public async Task Should_Create_A_New_Tag()
    {
        var authorDto = await _tagAppService.CreateAsync(
            new CreateTagDto
            {
                Name = "SwiftUI",
                Description = "UIKit,AppKit"
            }
        );

        authorDto.Id.ShouldNotBe(Guid.Empty);
        authorDto.Name.ShouldBe("SwiftUI");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Tag()
    {
        await Assert.ThrowsAsync<TagAlreadyExistsException>(async () =>
        {
            await _tagAppService.CreateAsync(
                new CreateTagDto
                {
                    Name = "C#",
                    Description = ".NET"
                }
            );
        });
    }
}