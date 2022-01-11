using Blazorise;
using Blazorise.DataGrid;
using Dotnet9.Categories;
using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Blazor.Pages.Admin;

public partial class Categories
{
    private Validations _createValidationsRef;

    private Validations _editValidationsRef;

    public Categories()
    {
        NewCategory = new CreateCategoryDto();
        EditingCategory = new UpdateCategoryDto();
    }

    private IReadOnlyList<CategoryDto> CategoryList { get; set; }

    private string Filter { get; set; }
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private bool CanCreateCategory { get; set; }
    private bool CanEditCategory { get; set; }
    private bool CanDeleteCategory { get; set; }

    private CreateCategoryDto NewCategory { get; set; }

    private Guid EditingCategoryId { get; set; }
    private UpdateCategoryDto EditingCategory { get; set; }

    private Modal CreateCategoryModal { get; set; }
    private Modal EditCategoryModal { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetCategoriesAsync();
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateCategory = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.Categories.Create);

        CanEditCategory = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.Categories.Edit);

        CanDeleteCategory = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.Categories.Delete);
    }

    private async Task GetCategoriesAsync()
    {
        var result = await CategoryAppService.GetListAsync(
            new GetCategoryListDto
            {
                Filter = Filter,
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );

        CategoryList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OnFilterChanged(string value)
    {
        Filter = value;
        await GetCategoriesAsync();
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CategoryDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.None)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;

        await GetCategoriesAsync();

        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateCategoryModal()
    {
        _createValidationsRef.ClearAll();

        NewCategory = new CreateCategoryDto();
        CreateCategoryModal.Show();
    }

    private void CloseCreateCategoryModal()
    {
        CreateCategoryModal.Hide();
    }

    private void OpenEditCategoryModal(CategoryDto category)
    {
        _editValidationsRef.ClearAll();

        EditingCategoryId = category.Id;
        EditingCategory = ObjectMapper.Map<CategoryDto, UpdateCategoryDto>(category);
        EditCategoryModal.Show();
    }

    private async Task DeleteCategoryAsync(CategoryDto category)
    {
        var confirmMessage = L["CategoryDeletionConfirmationMessage", category.Name];
        if (!await Message.Confirm(confirmMessage))
        {
            return;
        }

        await CategoryAppService.DeleteAsync(category.Id);
        await GetCategoriesAsync();
    }

    private void CloseEditCategoryModal()
    {
        EditCategoryModal.Hide();
    }

    private async Task CreateCategoryAsync()
    {
        try
        {
            if (await _createValidationsRef.ValidateAll())
            {
                await CategoryAppService.CreateAsync(NewCategory);

                await GetCategoriesAsync();
                await CreateCategoryModal.Hide();
            }
        }
        catch (CategoryAlreadyExistsException)
        {
            await Message.Warn(message: L[Dotnet9DomainErrorCodes.Categories.CagetoryAlreadyExist, NewCategory.Name]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task UpdateCategoryAsync()
    {
        try
        {
            if (await _editValidationsRef.ValidateAll())
            {
                await CategoryAppService.UpdateAsync(EditingCategoryId, EditingCategory);

                await GetCategoriesAsync();
                await EditCategoryModal.Hide();
            }
        }
        catch (CategoryAlreadyExistsException)
        {
            await Message.Warn(message: L[Dotnet9DomainErrorCodes.Categories.CagetoryAlreadyExist, EditingCategory.Name]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}