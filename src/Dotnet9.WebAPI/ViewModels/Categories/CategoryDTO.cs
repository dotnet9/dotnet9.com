namespace Dotnet9.WebAPI.ViewModels.Categories;

public record CategoryDto(Guid Id, int SequenceNumber, string Name, string Slug, string Cover,
    string? Description = null, bool Visible = false, Guid? ParentId = null);