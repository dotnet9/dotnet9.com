namespace Dotnet9.WebAPI.ViewModel.UserAdmin;

public class EditUserBaseRequestValidator : AbstractValidator<EditUserBaseRequest>
{
    public EditUserBaseRequestValidator()
    {
        RuleFor(e => e.NickName)
            .MaximumLength(UserConsts.MaxNickNameLength).WithMessage($"昵称长度不能超过{UserConsts.MaxNickNameLength}");

        RuleFor(e => e.Brief)
            .MaximumLength(UserConsts.MaxBriefLength).WithMessage($"个人简介长度不能超过{UserConsts.MaxBriefLength}");

        RuleFor(e => e.WebSite)
            .MaximumLength(UserConsts.MaxWebSiteLength).WithMessage($"个人网站长度不能超过{UserConsts.MaxWebSiteLength}");
    }
}