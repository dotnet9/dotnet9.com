namespace Dotnet9.WebAPI.FluentValidations.Donations;

public class AddOrUpdateDonationRequestValidator : AbstractValidator<AddOrUpdateDonationRequest>
{
    public AddOrUpdateDonationRequestValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("内容不能为空")
            .Length(DonationConsts.MinContentLength, DonationConsts.MaxContentLength)
            .WithMessage($"内容在[{DonationConsts.MinContentLength},{DonationConsts.MaxContentLength}]之间");
    }
}