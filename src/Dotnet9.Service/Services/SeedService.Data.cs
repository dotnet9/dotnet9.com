namespace Dotnet9.Service.Services;

public partial class SeedService
{
    private const string Title = "title: ";
    private const string Banner = "banner: ";
    private const string Slug = "slug: ";
    private const string Description = "description: ";
    private const string Date = "date: ";
    private const string LastModifyDate = "lastmod: ";
    private const string Copyright = "copyright: ";
    private const string Author = "author: ";
    private const string OriginalTitle = "originaltitle: ";
    private const string OriginalLink = "originallink: ";
    private const string Draft = "draft: ";
    private const string Cover = "cover: ";
    private const string Albums = "albums: ";
    private const string Categories = "categories: ";
    private const string Tags = "tags: ";

    private async Task CreateSeedDataAsync()
    {
        if (_dbContext.Blogs!.Any())
        {
            Console.WriteLine("存在文章数据，默认不再初始化");
            return;
        }

        if (!Directory.Exists(_siteOptions.AssetsLocalPath))
        {
            Console.WriteLine($"请配置种子数据目录：{nameof(_siteOptions.AssetsLocalPath)}");
            return;
        }

        var sw = new Stopwatch();
        sw.Start();
        var blogInfo = await CheckBlogs();
        if (!blogInfo.Status)
        {
            return;
        }

        await SeedAbouts();
        var albumsOfDb = await SeedAlbums(blogInfo.Albums);
        var categoriesOfDb = await SeedCategories(blogInfo.Categories);
        var tagsOfDb = await SeedTags(blogInfo.Tags);
        await SeedBlogs(blogInfo.Blogs?.ToList(), albumsOfDb?.ToList(), categoriesOfDb?.ToList(),
            tagsOfDb?.ToList());

        await SeedDonations();
        await SeedFriendlyLinks();
        await SeedPrivacies();
        await SeedTimelines();
        await SeedUsers();
        sw.Stop();
        Console.WriteLine($"Seed time: {sw.ElapsedMilliseconds} ms");
    }

    private async
        Task<(bool Status, BlogSeedDto[]? Blogs, AlbumSeedDto[]? Albums, CategorySeedDto[]? Categories,
            string[]?
            Tags)> CheckBlogs()
    {
        var allCategoryNames = new HashSet<string>();
        var tagNames = new HashSet<string>();

        bool CheckAlbumExist(AlbumSeedDto[] albums, IReadOnlyCollection<string>? albumNames)
        {
            if (albumNames == null || !albumNames.Any())
            {
                return true;
            }

            var exist = albumNames.All(albumName => albums.Any(x => x.Name == albumName));
            if (exist)
            {
                return true;
            }

            Debug.WriteLine($"文章头配置的专辑未在专辑列表中配置: {albumNames.JoinAsString(",")}");
            return false;
        }

        bool CheckCategoryExist(IReadOnlyCollection<string>? categoryNames)
        {
            if (categoryNames == null || !categoryNames.Any())
            {
                return true;
            }

            var exist = categoryNames.All(albumName => allCategoryNames.Any(x => x == albumName));
            if (exist)
            {
                return true;
            }

            Debug.WriteLine($"文章头配置的分类未在分类列表中配置: {categoryNames.JoinAsString(",")}");
            return false;
        }

        void ReadCategoryName(CategorySeedDto[]? datas)
        {
            if (datas == null || !datas.Any())
            {
                return;
            }

            foreach (var categorySeedDto in datas)
            {
                allCategoryNames.Add(categorySeedDto.Name);
                ReadCategoryName(categorySeedDto.Children);
            }
        }


        void ReadTagName(string[]? data)
        {
            if (data == null || !data.Any())
            {
                return;
            }

            foreach (var s in data)
            {
                tagNames.Add(s);
            }
        }

        var albums = JsonSerializer.Deserialize<AlbumSeedDto[]>(
            await System.IO.File.ReadAllTextAsync(Path.Combine(_siteOptions.AssetsLocalPath, "albums",
                "album.json")))!;
        var categories = JsonSerializer.Deserialize<CategorySeedDto[]>(
            await System.IO.File.ReadAllTextAsync(Path.Combine(_siteOptions.AssetsLocalPath, "cats",
                "category.json")))!;
        ReadCategoryName(categories);
        var blogFiles = new List<string>();
        for (var i = _siteOptions.Start; i <= DateTime.Now.Year; i++)
        {
            blogFiles.AddRange(Directory.GetFiles(Path.Combine(_siteOptions.AssetsLocalPath, i.ToString()),
                "*.md",
                SearchOption.AllDirectories));
        }

        var blogs = blogFiles.Select(Read).ToArray();
        foreach (var blogOfMarkdown in blogs)
        {
            if (!CheckAlbumExist(albums, blogOfMarkdown.Albums)
                || !CheckCategoryExist(blogOfMarkdown.Categories))
            {
                return (false, null, null, null, null);
            }

            ReadTagName(blogOfMarkdown.Tags);
        }

        return (true, blogs, albums, categories, tagNames.ToArray());
    }

    private async Task SeedAbouts()
    {
        var content =
            await System.IO.File.ReadAllTextAsync(Path.Combine(_siteOptions.AssetsLocalPath, "site",
                "about.md"));

        var about = await _aboutRepository.GetAsync();
        if (about == null)
        {
            about = _aboutManager.Create(content);
            await _dbContext.AddAsync(about);
        }
        else
        {
            about.ChangeContent(content);
        }

        await _dbContext.SaveChangesAsync();
    }

    private async Task<List<Album>?> SeedAlbums(AlbumSeedDto[]? albumSeeds)
    {
        if (albumSeeds == null || !albumSeeds.Any())
        {
            return null;
        }

        var albumsForDb = albumSeeds.Select(x =>
                _albumManager.CreateForSeed(x.SequenceNumber, x.Name, x.Slug, x.Cover, x.Description))
            .ToList();
        await _dbContext.AddRangeAsync(albumsForDb);
        await _dbContext.SaveChangesAsync();
        return albumsForDb;
    }

    private async Task SeedBlogs(List<BlogSeedDto>? blogSeeds, List<Album>? albums,
        List<Category>? categories, List<Tag>? tags)
    {
        if (blogSeeds == null || !blogSeeds.Any())
        {
            return;
        }

        var allBlogs = new List<Blog>();
        foreach (var blogSeed in blogSeeds)
        {
            var albumIds = albums?.Where(x => blogSeed.Albums?.Contains(x.Name) ?? false).Select(x => x.Id)
                .ToArray();
            var categoryIds = categories?.Where(x => blogSeed.Categories?.Contains(x.Name) ?? false)
                .Select(x => x.Id).ToArray();
            var tagIds = tags?.Where(x => blogSeed.Tags?.Contains(x.Name) ?? false).Select(x => x.Id).ToArray();
            var blog = _blogManager.CreateForSeed(blogSeed.Title, blogSeed.Slug,
                blogSeed.Description, blogSeed.Cover, blogSeed.Content, blogSeed.Copyright,
                blogSeed.Author, null, blogSeed.OriginalTitle, blogSeed.OriginalLink, blogSeed.Draft, blogSeed.Banner,
                true, albumIds,
                categoryIds, tagIds, blogSeed.Date);
            allBlogs.Add(blog);
        }

        var pageIndex = 0;
        const int pageSize = 100;
        while (true)
        {
            var temp = allBlogs.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
            if (temp.Length <= 0)
            {
                break;
            }

            await _dbContext.AddRangeAsync(temp);
            await _dbContext.SaveChangesAsync();

            pageIndex++;
        }
    }

    private async Task<List<Category>?> SeedCategories(CategorySeedDto[]? categorySeeds)
    {
        if (categorySeeds == null || !categorySeeds.Any())
        {
            return null;
        }

        var allCategories = new List<Category>();

        void ReadChildren(Guid? parentId, CategorySeedDto[]? seeds)
        {
            if (seeds == null || !seeds.Any())
            {
                return;
            }

            foreach (var seed in seeds)
            {
                var category =
                    _categoryManager.CreateForSeed(seed.SequenceNumber, seed.Name, seed.Slug, seed.Cover, parentId);
                allCategories!.Add(category);
                ReadChildren(category.Id, seed.Children);
            }
        }

        ReadChildren(null, categorySeeds);

        await _dbContext.AddRangeAsync(allCategories);
        await _dbContext.SaveChangesAsync();
        return allCategories;
    }

    private async Task<List<Tag>?> SeedTags(string[]? tagSeeds)
    {
        if (tagSeeds == null || !tagSeeds.Any())
        {
            return null;
        }

        var tagsForDb = tagSeeds.Select(x => _tagManager.CreateForSeed(x)).ToList();
        await _dbContext.AddRangeAsync(tagsForDb);
        await _dbContext.SaveChangesAsync();
        return tagsForDb;
    }

    private async Task SeedDonations()
    {
        var content =
            await System.IO.File.ReadAllTextAsync(Path.Combine(_siteOptions.AssetsLocalPath, "pays",
                "Donation.md"));

        var donation = await _donationRepository.GetAsync();
        if (donation == null)
        {
            donation = _donationManager.Create(content);
            await _dbContext.AddAsync(donation);
        }
        else
        {
            donation.ChangeContent(content);
        }

        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedFriendlyLinks()
    {
        var links = JsonSerializer.Deserialize<List<FriendlyLinkSeedDto>>(
            await System.IO.File.ReadAllTextAsync(Path.Combine(_siteOptions.AssetsLocalPath, "site",
                "link.json")));
        var linksForDb = links!.Select(x =>
            _linkManager.CreateForSeed(x.SequenceNumber, x.Name, x.Url, x.Description, x.Kind!));
        await _dbContext.AddRangeAsync(linksForDb);
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedPrivacies()
    {
        var content =
            await System.IO.File.ReadAllTextAsync(Path.Combine(_siteOptions.AssetsLocalPath, "site",
                "Privacy.md"));
        var privacy = await _privacyRepository.GetAsync();
        if (privacy == null)
        {
            privacy = _privacyManager.Create(content);
            await _dbContext.AddAsync(privacy);
        }
        else
        {
            privacy.ChangeContent(content);
        }

        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedTimelines()
    {
        var timelines = JsonSerializer.Deserialize<List<TimelineSeedDto>>(
            await System.IO.File.ReadAllTextAsync(Path.Combine(_siteOptions.AssetsLocalPath, "site",
                "timelines.json")));
        var timelinesForDb = timelines!.Select(x =>
            _timelineManager.CreateForSeed(x.Time, x.Title, x.Content));
        await _dbContext.AddRangeAsync(timelinesForDb);
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedUsers()
    {
        var adminUser = _userManager.CreateForSeed("admin", "管理员", "16888888888", "admin@dotnet9.com");
        await _dbContext.AddAsync(adminUser);
        await _dbContext.SaveChangesAsync();
    }

    private static BlogSeedDto Read(string markdownAbsolutePath)
    {
        var blogOfMarkdown = new BlogSeedDto();
        var lines = System.IO.File.ReadAllLines(markdownAbsolutePath);
        var isReadStart = false;
        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("---"))
            {
                if (isReadStart == false)
                {
                    isReadStart = true;
                    continue;
                }

                var contentLineStartOfAllLines = i + 2;
                var contentLineLength = lines.Length - contentLineStartOfAllLines;
                var contentLines = new string[contentLineLength];
                Array.Copy(lines, contentLineStartOfAllLines, contentLines, 0, contentLineLength);
                blogOfMarkdown.Content = string.Join("\n", contentLines);
                return blogOfMarkdown;
            }

            if (lines[i].StartsWith(Title))
            {
                blogOfMarkdown.Title = lines[i][Title.Length..];
            }
            else if (lines[i].StartsWith(Slug))
            {
                blogOfMarkdown.Slug = lines[i][Slug.Length..];
            }
            else if (lines[i].StartsWith(Description))
            {
                blogOfMarkdown.Description = lines[i][Description.Length..];
            }
            else if (lines[i].StartsWith(Date))
            {
                blogOfMarkdown.Date = DateTime.Parse(lines[i][Date.Length..]);
            }
            else if (lines[i].StartsWith(LastModifyDate))
            {
                blogOfMarkdown.LastModifyDate = DateTime.Parse(lines[i][LastModifyDate.Length..]);
            }
            else if (lines[i].StartsWith(Copyright))
            {
                blogOfMarkdown.Copyright =
                    (CopyRightType)Enum.Parse(typeof(CopyRightType), lines[i][Copyright.Length..]);
            }
            else if (lines[i].StartsWith(Author))
            {
                blogOfMarkdown.Author = lines[i][Author.Length..];
            }
            else if (lines[i].StartsWith(OriginalTitle))
            {
                blogOfMarkdown.OriginalTitle = lines[i][OriginalTitle.Length..];
            }
            else if (lines[i].StartsWith(OriginalLink))
            {
                blogOfMarkdown.OriginalLink = lines[i][OriginalLink.Length..];
            }
            else if (lines[i].StartsWith(Draft))
            {
                blogOfMarkdown.Draft = bool.Parse(lines[i][Draft.Length..]);
            }
            else if (lines[i].StartsWith(Cover))
            {
                blogOfMarkdown.Cover = lines[i][Cover.Length..];
            }
            else if (lines[i].StartsWith(Albums))
            {
                blogOfMarkdown.Albums = lines[i][Albums.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else if (lines[i].StartsWith(Categories))
            {
                blogOfMarkdown.Categories =
                    lines[i][Categories.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else if (lines[i].StartsWith(Tags))
            {
                blogOfMarkdown.Tags = lines[i][Tags.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else if (lines[i].StartsWith(Banner))
            {
                blogOfMarkdown.Banner = bool.Parse(lines[i][Banner.Length..]);
            }
        }

        return blogOfMarkdown;
    }

    private static void Write(string markdownAbsolutePath, BlogSeedDto blogOfMarkdown)
    {
        var lines = new List<string>();
        lines.Add("---");

        if (!string.IsNullOrWhiteSpace(blogOfMarkdown.Title))
        {
            lines.Add($"{Title}{blogOfMarkdown.Title}");
        }

        if (!string.IsNullOrWhiteSpace(blogOfMarkdown.Slug))
        {
            lines.Add($"{Slug}{blogOfMarkdown.Slug}");
        }

        if (!string.IsNullOrWhiteSpace(blogOfMarkdown.Description))
        {
            lines.Add($"{Description}{blogOfMarkdown.Description}");
        }

        if (blogOfMarkdown.Date != default)
        {
            lines.Add($"{Date}{blogOfMarkdown.Date:yyyy-MM-dd HH:mm:ss}");
        }

        if (blogOfMarkdown.LastModifyDate != null && blogOfMarkdown.LastModifyDate != default(DateTime))
        {
            lines.Add($"{LastModifyDate}{blogOfMarkdown.LastModifyDate:yyyy-MM-dd HH:mm:ss}");
        }

        lines.Add($"{Copyright}{blogOfMarkdown.Copyright}");
        if (!string.IsNullOrWhiteSpace(blogOfMarkdown.Author))
        {
            lines.Add($"{Author}{blogOfMarkdown.Author}");
        }

        if (!string.IsNullOrWhiteSpace(blogOfMarkdown.OriginalTitle))
        {
            lines.Add($"{OriginalTitle}{blogOfMarkdown.OriginalTitle}");
        }

        if (!string.IsNullOrWhiteSpace(blogOfMarkdown.OriginalLink))
        {
            lines.Add($"{OriginalLink}{blogOfMarkdown.OriginalLink}");
        }

        lines.Add($"{Draft}{blogOfMarkdown.Draft}");

        if (!string.IsNullOrWhiteSpace(blogOfMarkdown.Cover))
        {
            lines.Add($"{Cover}{blogOfMarkdown.Cover}");
        }

        if (blogOfMarkdown.Albums is { Length: > 0 })
        {
            lines.Add($"{Albums}{string.Join(',', blogOfMarkdown.Albums)}");
        }

        if (blogOfMarkdown.Categories is { Length: > 0 })
        {
            lines.Add($"{Categories}{string.Join(',', blogOfMarkdown.Categories)}");
        }

        if (blogOfMarkdown.Tags is { Length: > 0 })
        {
            lines.Add($"{Tags}{string.Join(',', blogOfMarkdown.Tags)}");
        }

        lines.Add("---");

        if (System.IO.File.Exists(markdownAbsolutePath))
        {
            System.IO.File.Delete(markdownAbsolutePath);
        }

        System.IO.File.WriteAllLines(markdownAbsolutePath, lines);
        System.IO.File.AppendAllText(markdownAbsolutePath, $"\n{blogOfMarkdown.Content}");
    }
}

public class BlogSeedDto
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime? LastModifyDate { get; set; }
    public CopyRightType Copyright { get; set; }
    public string Author { get; set; } = null!;
    public string? OriginalTitle { get; set; }
    public string? OriginalLink { get; set; }
    public bool Draft { get; set; }
    public string Cover { get; set; } = null!;
    public string[]? Albums { get; set; }
    public string[]? Categories { get; set; }
    public string[]? Tags { get; set; }
    public string Content { get; set; } = null!;
    public bool Banner { get; set; }
}

public record AlbumSeedDto(int SequenceNumber, string Name, string Slug, string Cover, string Description);

public record CategorySeedDto(int SequenceNumber, string Name, string Slug, string Cover, CategorySeedDto[]? Children);

public record FriendlyLinkSeedDto(int SequenceNumber, string Name, string Url,
    string? Description = null, string? Kind = null);

public record TimelineSeedDto(DateTime Time, string Title, string Content);