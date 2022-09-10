using Dotnet9.WebAPI.Domain.Shared.Abouts;

namespace Dotnet9.WebAPI.Application.Contracts.Abouts;

public record AddOrUpdateAboutRequest(string Content);

public class AddOrUpdateAboutRequestValidator : AbstractValidator<AddOrUpdateAboutRequest>
{
    public AddOrUpdateAboutRequestValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("关于内容不能为空")
            .Length(AboutConsts.MinContentLength, AboutConsts.MaxContentLength)
            .WithMessage($"关于内容在[{AboutConsts.MinContentLength},{AboutConsts.MaxContentLength}]之间");
    }
}