namespace Dotnet9.Core.SqlSugar.Seed
{
    public class BlogPostSeedDto
    {
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public CreationType Copyright { get; set; }
        public string Author { get; set; } = null!;
        public string? OriginalTitle { get; set; }
        public string? OriginalLink { get; set; }
        public bool Draft { get; set; }
        public string Cover { get; set; } = null!;
        public string? Album { get; set; }
        public string Category { get; set; }
        public string[]? Tags { get; set; }
        public string Content { get; set; } = null!;
        public bool Banner { get; set; }
    }
}