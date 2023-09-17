﻿namespace Dotnet9.WebAPI.ViewModel.Categories;

public record AddCategoryRequest(int SequenceNumber, string Name, string Slug, string Cover, string? Description,
    Guid? ParentId, bool Visible);