namespace Dotnet9.Models.Dtos;

public class RankListModel
{
    /// <summary>
    ///     标题
    /// </summary>
    public string Title { get; set; }


    public List<RankListItem> RankList { get; set; } = new();
}

public class RankListItem
{
    public string Url { get; set; }

    public string Text { get; set; }
}