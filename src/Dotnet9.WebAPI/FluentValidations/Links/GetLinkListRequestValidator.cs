namespace Dotnet9.WebAPI.FluentValidations.Links;

public class GetLinkListRequestValidator : AbstractValidator<GetLinkListRequest>
{
    public GetLinkListRequestValidator()
    {
        RuleFor(e => e.Name).MaximumLength(LinkConsts.MaxNameLength)
            .WithMessage($"查询名称长度不能超过{LinkConsts.MaxNameLength}");
        RuleFor(e => e.Url).MaximumLength(LinkConsts.MaxUrlLength).WithMessage($"查询链接长度不能超过{LinkConsts.MaxUrlLength}");
        RuleFor(e => e.Description).MaximumLength(LinkConsts.MaxDescriptionLength)
            .WithMessage($"查询描述长度不能超过{LinkConsts.MaxDescriptionLength}");
        RuleFor(e => e.Current).GreaterThan(0).WithMessage("页索引从1开始"); //页号从1开始
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(5).WithMessage("分页大小需大于等于5");
    }
}