namespace Dotnet9.WebAPI.Domain.Shared.Categories;

// ReSharper disable once InconsistentNaming
public record CategoryDTO(Guid Id, int SequenceNumber, string Name, string Slug, string Cover,
    string? Description = null, bool Visible = false, Guid? ParentId = null);