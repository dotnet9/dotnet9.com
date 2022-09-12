namespace Dotnet9.WebAPI.FluentValidations.Privacies;

public class AddOrUpdatePrivacyRequestValidator : AbstractValidator<AddOrUpdatePrivacyRequest>
{
    public AddOrUpdatePrivacyRequestValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("内容不能为空")
            .Length(PrivacyConsts.MinContentLength, PrivacyConsts.MaxContentLength)
            .WithMessage($"内容在[{PrivacyConsts.MinContentLength},{PrivacyConsts.MaxContentLength}]之间");
    }
}