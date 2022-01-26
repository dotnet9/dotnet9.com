using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dotnet9.Categories;

public sealed class CategoryAppServiceTests : Dotnet9ApplicationTestBase
{
    private readonly ICategoryAppService _categoryAppService;

    public CategoryAppServiceTests()
    {
        _categoryAppService = GetRequiredService<ICategoryAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Category_Without_Any_Filter()
    {
        var result = await _categoryAppService.GetListAsync(new GetCategoryListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(category => category.Name == "WPF");
        result.Items.ShouldContain(category => category.Name == "Winform");
    }

    [Fact]
    public async Task Should_Get_All_CategoryCount_Without_Any_Filter2()
    {
        var result = await _categoryAppService.GetListCountAsync();

        result.Count.ShouldBeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public async Task Should_Get_Filtered_Category()
    {
        var result = await _categoryAppService.GetListAsync(
            new GetCategoryListDto { Filter = "WPF" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(category => category.Name == "WPF");
    }

    [Fact]
    public async Task Should_Create_A_New_Category()
    {
        var categoryDto = await _categoryAppService.CreateAsync(
            new CreateCategoryDto
            {
                Name = "MAUI",
                CoverImageUrl = "https://img1.dotnet9.com/2022/01/10/cover_03.jpeg",
                Description = "test category"
            }
        );

        categoryDto.Id.ShouldNotBe(Guid.Empty);
        categoryDto.Name.ShouldBe("MAUI");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Category()
    {
        await Assert.ThrowsAsync<CategoryAlreadyExistsException>(async () =>
        {
            await _categoryAppService.CreateAsync(
                new CreateCategoryDto
                {
                    Name = "WPF",
                    CoverImageUrl = "https://img1.dotnet9.com/2022/01/10/cover_03.jpeg",
                    Description = "test"
                }
            );
        });
    }
}