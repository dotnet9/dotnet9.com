namespace Dotnet9.WebAPI.FluentValidations.Tags;

public class QueryTagRequestValidator : AbstractValidator<QueryTagRequest>
{
    public QueryTagRequestValidator()
    {
        RuleFor(e => e.Keywords).MaximumLength(100).WithMessage("查询关键字长度不能超过100");
        RuleFor(e => e.PageIndex).GreaterThan(0).WithMessage("页索引从1开始"); //页号从1开始
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(5).WithMessage("分页大小需大于等于5");
    }
}