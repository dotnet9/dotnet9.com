using Blazorise;
using Blazorise.DataGrid;
using Dotnet9.Permissions;
using Dotnet9.Tags;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Blazor.Pages.Admin;

public partial class Tags
{
    private Validations _createValidationsRef;

    private Validations _editValidationsRef;

    public Tags()
    {
        NewTag = new CreateTagDto();
        EditingTag = new UpdateTagDto();
    }

    private IReadOnlyList<TagDto> TagList { get; set; }

    private string Filter { get; set; }
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private bool CanCreateTag { get; set; }
    private bool CanEditTag { get; set; }
    private bool CanDeleteTag { get; set; }

    private CreateTagDto NewTag { get; set; }

    private Guid EditingTagId { get; set; }
    private UpdateTagDto EditingTag { get; set; }

    private Modal CreateTagModal { get; set; }
    private Modal EditTagModal { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetTagsAsync();
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateTag = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.Tags.Create);

        CanEditTag = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.Tags.Edit);

        CanDeleteTag = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.Tags.Delete);
    }

    private async Task GetTagsAsync()
    {
        var result = await TagAppService.GetListAsync(
            new GetTagListDto
            {
                Filter = Filter,
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );

        TagList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OnFilterChanged(string value)
    {
        Filter = value;
        await GetTagsAsync();
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<TagDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.None)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;

        await GetTagsAsync();

        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateTagModal()
    {
        _createValidationsRef.ClearAll();

        NewTag = new CreateTagDto();
        CreateTagModal.Show();
    }

    private void CloseCreateTagModal()
    {
        CreateTagModal.Hide();
    }

    private void OpenEditTagModal(TagDto tag)
    {
        _editValidationsRef.ClearAll();

        EditingTagId = tag.Id;
        EditingTag = ObjectMapper.Map<TagDto, UpdateTagDto>(tag);
        EditTagModal.Show();
    }

    private async Task DeleteTagAsync(TagDto tag)
    {
        var confirmMessage = L["TagDeletionConfirmationMessage", tag.Name];
        if (!await Message.Confirm(confirmMessage))
        {
            return;
        }

        await TagAppService.DeleteAsync(tag.Id);
        await GetTagsAsync();
    }

    private void CloseEditTagModal()
    {
        EditTagModal.Hide();
    }

    private async Task CreateTagAsync()
    {
        if (await _createValidationsRef.ValidateAll())
        {
            await TagAppService.CreateAsync(NewTag);

            await GetTagsAsync();
            await CreateTagModal.Hide();
        }
    }

    private async Task UpdateTagAsync()
    {
        if (await _editValidationsRef.ValidateAll())
        {
            await TagAppService.UpdateAsync(EditingTagId, EditingTag);

            await GetTagsAsync();
            await EditTagModal.Hide();
        }
    }
}