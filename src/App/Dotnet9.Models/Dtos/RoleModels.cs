namespace Dotnet9.Models.Dtos;

public class RoleModels
{
}

public class RoleListRequest : BasePageModel
{
}

public class RoleListResponse
{
    public Guid Id { get; set; }

    public string RoleName { get; set; }

    public DateTime CreateTime { get; set; }


    public int Count { get; set; }
}