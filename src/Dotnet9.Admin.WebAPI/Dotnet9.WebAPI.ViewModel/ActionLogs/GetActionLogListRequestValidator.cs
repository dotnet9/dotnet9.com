namespace Dotnet9.WebAPI.ViewModel.ActionLogs;

public class GetActionLogListRequestValidator : AbstractValidator<GetActionLogListRequest>
{
    public GetActionLogListRequestValidator()
    {
        RuleFor(e => e.Current).GreaterThan(0).WithMessage("页索引从1开始"); //页号从1开始
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(5).WithMessage("分页大小需大于等于5");
    }
}