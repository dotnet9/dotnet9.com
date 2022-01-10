using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dotnet9.Contacts;

public sealed class ContactAppServiceTests : Dotnet9ApplicationTestBase
{
    private readonly IContactAppService _contactAppService;

    public ContactAppServiceTests()
    {
        _contactAppService = GetRequiredService<IContactAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Contact_Without_Any_Filter()
    {
        var result = await _contactAppService.GetListAsync(new GetContactListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(contact => contact.Name == "乐趣课堂");
    }

    [Fact]
    public async Task Should_Get_Filtered_Contact()
    {
        var result = await _contactAppService.GetListAsync(
            new GetContactListDto { Filter = "乐" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(contact => contact.Name == "乐趣课堂");
    }

    [Fact]
    public async Task Should_Create_A_New_Contact()
    {
        var contactDto = await _contactAppService.CreateAsync(
            new CreateContactDto
            {
                Name = "dotnet9",
                Email = "dotnet9@dotnet9.com",
                Subject = "thank you",
                Message = "thnak you too"
            }
        );

        contactDto.Id.ShouldNotBe(Guid.Empty);
        contactDto.Name.ShouldBe("dotnet9");
    }
}