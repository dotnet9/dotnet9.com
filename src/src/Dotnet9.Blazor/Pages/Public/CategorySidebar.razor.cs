using Castle.Components.DictionaryAdapter;
using Dotnet9.Categories;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using BlazorComponent;

namespace Dotnet9.Blazor.Pages.Public;

public partial class CategorySidebar
{
    [Inject] private ICategoryAppService CategoryAppService { set; get; }
    [Inject] private NavigationManager NavigationManager { set; get; }

    private IList<Item> _expandedNodes = new List<Item>();

    private List<Item> _items = new();

    private Item _selectedItem;

    private StringNumber _selected = -1;

    private StringNumber Selected
    {
        get => _selected;
        set
        {
            if (value.Value == null || !int.TryParse(value.Value.ToString(), out var index) || index < 0)
            {
                return;
            }

            _selected = value;
            SelectedItem = _items[index];
        }
    }

    public CategorySidebar()
    {
    }

    private Item SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            NavigationManager.NavigateTo($"/cat/{HttpUtility.UrlEncode(value.Text)}", true);
        }
    }

    protected override async Task OnInitializedAsync()
    {
        var categoryDtos = await CategoryAppService.GetListCountAsync();
        _items.Clear();
        ReadChildren(categoryDtos, null, _items);
        await base.OnInitializedAsync();
    }

    private void ReadChildren(List<CategoryCountDto> sources, Guid? parentId, List<Item> parentChildren)
    {
        var children = sources.FindAll(x => x.ParentId == parentId);
        if (children is not { Count: > 0 })
        {
            return;
        }

        foreach (var child in children)
        {
            var currentItem = new Item
            {
                Text = child.Name,
                Count = child.BlogPostCount,
                Children = new EditableList<Item>()
            };
            parentChildren.Add(currentItem);
            ReadChildren(sources, child.Id, currentItem.Children);
        }
    }

    public class Item
    {
        public string Text { get; set; }
        public int Count { get; set; }
        public List<Item> Children { get; set; }
    }
}