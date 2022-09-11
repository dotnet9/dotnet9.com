namespace Dotnet9.WebAPI.FluentValidations.Albums;

public class GetAlbumByCategoryRequestValidator : AbstractValidator<GetAlbumsByCategoryRequest>
{
    public GetAlbumByCategoryRequestValidator()
    {
        RuleFor(e => e.PageIndex).GreaterThan(0).WithMessage("页索引从1开始"); //页号从1开始
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(5).WithMessage("分页大小需大于等于5");
    }
}