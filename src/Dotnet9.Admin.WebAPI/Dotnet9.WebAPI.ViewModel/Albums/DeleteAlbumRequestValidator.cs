namespace Dotnet9.WebAPI.ViewModel.Albums;

public class DeleteAlbumRequestValidator : AbstractValidator<DeleteAlbumRequest>
{
    public DeleteAlbumRequestValidator()
    {
        RuleFor(e => e.Ids).Must(e => e != null && e.Any()).WithMessage("删除的ID不能为空");
    }
}