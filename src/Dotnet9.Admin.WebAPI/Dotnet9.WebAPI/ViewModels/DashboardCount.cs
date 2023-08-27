namespace Dotnet9.WebAPI.ViewModels;

public record DashboardCount(int ViewsCount, int MessageCount, int UserCount, int BlogPostCount,
    List<AlbumCount> AlbumCounts, List<CategoryCount> CategoryCounts, List<TagCount> TagCounts,
    List<BlogPostStatistics> BlogPostStatistics, List<BlogPostViewCount> UniqueViewCounts,
    List<BlogPostRank> BlogPostRanks);

public record AlbumCount(Guid Id, string Name, int BlogPostCount);

public record CategoryCount(Guid Id, string Name, int BlogPostCount);

public record TagCount(Guid Id, string Name, int BlogPostCount);

public record BlogPostStatistics(string Day, int Count);

public record BlogPostViewCount(string Day, int BlogPostCount);

public record BlogPostRank(string Name, int Count);