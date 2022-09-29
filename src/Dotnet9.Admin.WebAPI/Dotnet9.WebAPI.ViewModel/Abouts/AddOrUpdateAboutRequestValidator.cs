namespace Dotnet9.WebAPI.ViewModel.Abouts;

public class AddOrUpdateAboutRequestValidator : AbstractValidator<AddOrUpdateAboutRequest>
{
    public AddOrUpdateAboutRequestValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("关于内容不能为空")
            .Length(AboutConsts.MinContentLength, AboutConsts.MaxContentLength)
            .WithMessage($"关于内容在[{AboutConsts.MinContentLength},{AboutConsts.MaxContentLength}]之间");
    }
}