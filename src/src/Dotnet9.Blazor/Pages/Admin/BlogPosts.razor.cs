using Blazorise;
using Blazorise.DataGrid;
using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet9.Albums;
using Dotnet9.Blogs;
using Dotnet9.Categories;
using Dotnet9.Tags;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Blazor.Pages.Admin;

public partial class BlogPosts
{
    private Validations _createValidationsRef;

    private Validations _editValidationsRef;

    public BlogPosts()
    {
        NewBlogPost = new CreateBlogPostDto();
        EditingBlogPost = new UpdateBlogPostDto();
    }

    private IReadOnlyList<BlogPostDto> BlogPostList { get; set; }
    private IReadOnlyList<AlbumLookupDto> AlbumLookupList { get; set;}
    private IReadOnlyList<CategoryLookupDto> CategoryLookupList { get; set; }
    private IReadOnlyList<TagLookupDto> TagLookupList { get; set; }

    private string Filter { get; set; }
    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private bool CanCreateBlogPost { get; set; }
    private bool CanEditBlogPost { get; set; }
    private bool CanDeleteBlogPost { get; set; }

    private CreateBlogPostDto NewBlogPost { get; set; }

    private Guid EditingBlogPostId { get; set; }
    private UpdateBlogPostDto EditingBlogPost { get; set; }

    private Modal CreateBlogPostModal { get; set; }
    private Modal EditBlogPostModal { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetBlogPostsAsync();
        AlbumLookupList = (await BlogPostAppService.GetAlbumLookupAsync()).Items;
        CategoryLookupList = (await BlogPostAppService.GetCategoryLookupAsync()).Items;
        TagLookupList = (await BlogPostAppService.GetTagLookupAsync()).Items;
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateBlogPost = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.BlogPosts.Create);

        CanEditBlogPost = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.BlogPosts.Edit);

        CanDeleteBlogPost = await AuthorizationService.IsGrantedAsync(Dotnet9Permissions.BlogPosts.Delete);
    }

    private async Task GetBlogPostsAsync()
    {
        var result = await BlogPostAppService.GetListAsync(
            new GetBlogPostListDto
            {
                Filter = Filter,
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );

        BlogPostList = result.Items;
        TotalCount = (int)result.TotalCount;
    }

    private async Task OnFilterChanged(string value)
    {
        Filter = value;
        await GetBlogPostsAsync();
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<BlogPostDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.None)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;

        await GetBlogPostsAsync();

        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateBlogPostModal()
    {
        _createValidationsRef.ClearAll();

        NewBlogPost = new CreateBlogPostDto();
        CreateBlogPostModal.Show();
    }

    private void CloseCreateBlogPostModal()
    {
        CreateBlogPostModal.Hide();
    }

    private void OpenEditBlogPostModal(BlogPostDto blogPost)
    {
        _editValidationsRef.ClearAll();

        EditingBlogPostId = blogPost.Id;
        EditingBlogPost = ObjectMapper.Map<BlogPostDto, UpdateBlogPostDto>(blogPost);
        EditBlogPostModal.Show();
    }

    private async Task DeleteBlogPostAsync(BlogPostDto blogPost)
    {
        var confirmMessage = L["BlogPostDeletionConfirmationMessage", blogPost.Title];
        if (!await Message.Confirm(confirmMessage))
        {
            return;
        }

        await BlogPostAppService.DeleteAsync(blogPost.Id);
        await GetBlogPostsAsync();
    }

    private void CloseEditBlogPostModal()
    {
        EditBlogPostModal.Hide();
    }

    private async Task CreateBlogPostAsync()
    {
        try
        {
            if (await _createValidationsRef.ValidateAll())
            {
                await BlogPostAppService.CreateAsync(NewBlogPost);

                await GetBlogPostsAsync();
                await CreateBlogPostModal.Hide();
            }
        }
        catch (BlogPostTitleAlreadyExistsException)
        {
            await Message.Warn(message: L[Dotnet9DomainErrorCodes.BlogPosts.TitleAlreadyExist, NewBlogPost.Title]);
        }
        catch (BlogPostSlugAlreadyExistsException)
        {
            await Message.Warn(message: L[Dotnet9DomainErrorCodes.BlogPosts.SlugAlreadyExist, NewBlogPost.Slug]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task UpdateBlogPostAsync()
    {
        try
        {
            if (await _editValidationsRef.ValidateAll())
            {
                await BlogPostAppService.UpdateAsync(EditingBlogPostId, EditingBlogPost);

                await GetBlogPostsAsync();
                await EditBlogPostModal.Hide();
            }
        }
        catch (BlogPostTitleAlreadyExistsException)
        {
            await Message.Warn(message: L[Dotnet9DomainErrorCodes.BlogPosts.TitleAlreadyExist, NewBlogPost.Title]);
        }
        catch (BlogPostSlugAlreadyExistsException)
        {
            await Message.Warn(message: L[Dotnet9DomainErrorCodes.BlogPosts.SlugAlreadyExist, NewBlogPost.Slug]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}