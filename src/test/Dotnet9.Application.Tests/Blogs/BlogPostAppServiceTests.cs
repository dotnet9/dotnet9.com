using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dotnet9.Blogs;

public sealed class BlogPostAppServiceTests : Dotnet9ApplicationTestBase
{
    private readonly IBlogPostAppService _blogPostAppService;

    public BlogPostAppServiceTests()
    {
        _blogPostAppService = GetRequiredService<IBlogPostAppService>();
    }

    [Fact]
    public async Task Should_Get_All_BlogPosts_Without_Any_Filter()
    {
        var result = await _blogPostAppService.GetListAsync(new GetBlogPostListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(blogPost => blogPost.Title == "WPF UI Design");
        result.Items.ShouldContain(blogPost => blogPost.Title == "Winform UI Design");
    }

    [Fact]
    public async Task Should_Get_Filtered_BlogPosts()
    {
        var result = await _blogPostAppService.GetListAsync(
            new GetBlogPostListDto { Filter = "WPF" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(blogPost => blogPost.Title == "WPF UI Design");
    }

    [Fact]
    public async Task Should_Create_A_New_BlogPost()
    {
        var blogPostDto = await _blogPostAppService.CreateAsync(
            new CreateBlogPostDto
            {
                Title = "MAUI UI Design",
                Slug = "maui-ui-design",
                ShortDescription = "A beautiful and generous maui design",
                Content = "This is a test article",
                CoverImageUrl = "https://img1.dotnet9.com/2022/01/10/cover_03.jpeg",
                BlogCopyrightTypeEnum = CopyrightType.Default
            }
        );

        blogPostDto.Id.ShouldNotBe(Guid.Empty);
        blogPostDto.Title.ShouldBe("MAUI UI Design");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_BlogPost()
    {
        await Assert.ThrowsAsync<BlogPostTitleAlreadyExistsException>(async () =>
        {
            await _blogPostAppService.CreateAsync(
                new CreateBlogPostDto
                {
                    Title = "WPF UI Design",
                    Slug = "test-slug",
                    ShortDescription = "A beautiful and generous maui design",
                    Content = "This is a test article",
                    CoverImageUrl = "https://img1.dotnet9.com/2022/01/10/cover_03.jpeg",
                    BlogCopyrightTypeEnum = CopyrightType.Default
                }
            );
        });
    }
}