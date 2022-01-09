using Blazorise;
using Dotnet9.Permissions;
using Dotnet9.Albums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Components;
using Blazorise.DataGrid;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Blazor.Pages.Admin;

public partial class Albums
{
    private Validations _createValidationsRef;

    private Validations _editValidationsRef;

    public Albums()
    {
        NewAlbum = new CreateAlbumDto();
        EditingAlbum = new UpdateAlbumDto();
    }

    private IReadOnlyList<AlbumDto> AlbumList { get; set; }

    private string Filter { get; set; }
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private bool CanCreateAlbum { get; set; }
    private bool CanEditAlbum { get; set; }
    private bool CanDeleteAlbum { get; set; }

    private CreateAlbumDto NewAlbum { get; set; }

    private Guid EditingAlbumId { get; set; }
    private UpdateAlbumDto EditingAlbum { get; set; }

    private Modal CreateAlbumModal { get; set; }
    private Modal EditAlbumModal { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetAlbumsAsync();
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateAlbum = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.Albums.Create);

        CanEditAlbum = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.Albums.Edit);

        CanDeleteAlbum = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.Albums.Delete);
    }

    private async Task GetAlbumsAsync()
    {
        var result = await AlbumAppService.GetListAsync(
            new GetAlbumListDto
            {
                Filter = Filter,
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );

        AlbumList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OnFilterChanged(string value)
    {
        Filter = value;
        await GetAlbumsAsync();
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<AlbumDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.None)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;

        await GetAlbumsAsync();

        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateAlbumModal()
    {
        _createValidationsRef.ClearAll();

        NewAlbum = new CreateAlbumDto();
        CreateAlbumModal.Show();
    }

    private void CloseCreateAlbumModal()
    {
        CreateAlbumModal.Hide();
    }

    private void OpenEditAlbumModal(AlbumDto album)
    {
        _editValidationsRef.ClearAll();

        EditingAlbumId = album.Id;
        EditingAlbum = ObjectMapper.Map<AlbumDto, UpdateAlbumDto>(album);
        EditAlbumModal.Show();
    }

    private async Task DeleteAlbumAsync(AlbumDto album)
    {
        var confirmMessage = L["AlbumDeletionConfirmationMessage", album.Name];
        if (!await Message.Confirm(confirmMessage))
        {
            return;
        }

        await AlbumAppService.DeleteAsync(album.Id);
        await GetAlbumsAsync();
    }

    private void CloseEditAlbumModal()
    {
        EditAlbumModal.Hide();
    }

    private async Task CreateAlbumAsync()
    {
        try
        {
            if (await _createValidationsRef.ValidateAll())
            {
                await AlbumAppService.CreateAsync(NewAlbum);

                await GetAlbumsAsync();
                await CreateAlbumModal.Hide();
            }
        }
        catch (AlbumAlreadyExistsException)
        {
            await Message.Warn(message: L[Dotnet9DomainErrorCodes.Albums.AlbumAlreadyExist, NewAlbum.Name]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task UpdateAlbumAsync()
    {
        try
        {
            if (await _editValidationsRef.ValidateAll())
            {
                await AlbumAppService.UpdateAsync(EditingAlbumId, EditingAlbum);

                await GetAlbumsAsync();
                await EditAlbumModal.Hide();
            }
        }
        catch (AlbumAlreadyExistsException)
        {
            await Message.Warn(message: L[Dotnet9DomainErrorCodes.Albums.AlbumAlreadyExist, EditingAlbum.Name]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}