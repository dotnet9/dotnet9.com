namespace Dotnet9.Models.Dtos.Blogs.Dto;

public class PostRequestModel : BasePageModel
{
    public Guid? CateId { get; set; }

    public Guid? TagId { get; set; }
    public string? Keywords { get; set; }

    public bool FilterPublish { get; set; } = true;

    public bool PublishStatus { get; set; } = true;
}

public class GetCateModel : BasePageModel
{
}

public class GetTagModel : BasePageModel
{
}

public class GetFriendLinkModel : BasePageModel
{
}