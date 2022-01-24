using Castle.Components.DictionaryAdapter;
using Dotnet9.Categories;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet9.Blazor.Pages.Public;

public partial class CategorySidebar
{
    private ICategoryAppService _categoryAppService;
    private readonly NavigationManager _navigationManager;

    private IList<Item> _expandedNodes = new List<Item>();

    private List<Item> _items = new();

    private Item _selectedItem;

    public CategorySidebar(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    private Item SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            _navigationManager.NavigateTo($"/cat/{HttpUtility.UrlEncode(value.Text)}", true);
        }
    }

    protected override async Task OnInitializedAsync()
    {
        _categoryAppService = ScopedServices.GetRequiredService<ICategoryAppService>();
        var categoryDtos = await _categoryAppService.GetListAsync();
        _items.Clear();
        ReadChildren(categoryDtos, null, _items);
    }

    private void ReadChildren(List<CategoryDto> sources, Guid? parentId, List<Item> parentChildren)
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
                Children = new EditableList<Item>()
            };
            parentChildren.Add(currentItem);
            ReadChildren(sources, child.Id, currentItem.Children);
        }
    }

    public class Item
    {
        public string Text { get; set; }
        public List<Item> Children { get; set; }
    }
}