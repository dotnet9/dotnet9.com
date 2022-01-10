using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dotnet9.Comments;

public sealed class CommentAppServiceTests : Dotnet9ApplicationTestBase
{
    private readonly ICommentAppService _commentAppService;

    public CommentAppServiceTests()
    {
        _commentAppService = GetRequiredService<ICommentAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Comment_Without_Any_Filter()
    {
        var result = await _commentAppService.GetListAsync(new GetCommentListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(comment => comment.Text.Contains("thank you"));
    }

    [Fact]
    public async Task Should_Get_Filtered_Comment()
    {
        var result = await _commentAppService.GetListAsync(
            new GetCommentListDto { Filter = "sharing" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(comment => comment.Text.Contains("thank you"));
    }

    [Fact]
    public async Task Should_Create_A_New_Comment()
    {
        var commentDto = await _commentAppService.CreateAsync(
            new CreateCommentDto
            {
                RepliedCommentId = Guid.NewGuid(),
                Text = "Test"
            }
        );

        commentDto.Id.ShouldNotBe(Guid.Empty);
        commentDto.Text.ShouldBe("Test");
    }
}