namespace Dotnet9.WebAPI.Services;

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
        if (_dbContext.BlogPosts!.Any())
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
        var blogPostInfo = await CheckBlogPosts();
        if (!blogPostInfo.Status)
        {
            return;
        }

        await SeedAbouts();
        var albumsOfDb = await SeedAlbums(blogPostInfo.Albums);
        var categoriesOfDb = await SeedCategories(blogPostInfo.Categories);
        var tagsOfDb = await SeedTags(blogPostInfo.Tags);
        await SeedBlogPosts(blogPostInfo.BlogPosts?.ToList(), albumsOfDb?.ToList(), categoriesOfDb?.ToList(),
            tagsOfDb?.ToList());

        await SeedDonations();
        await SeedLinks();
        await SeedPrivacies();
        await SeedTimelines();
        sw.Stop();
        Console.WriteLine($"Seed time: {sw.ElapsedMilliseconds} ms");
    }

    private async
        Task<(bool Status, BlogPostSeedDto[]? BlogPosts, AlbumSeedDto[]? Albums, CategorySeedDto[]? Categories,
            string[]?
            Tags)> CheckBlogPosts()
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
        var blogPostFiles = new List<string>();
        for (var i = _siteOptions.Start; i <= DateTime.Now.Year; i++)
        {
            blogPostFiles.AddRange(Directory.GetFiles(Path.Combine(_siteOptions.AssetsLocalPath, i.ToString()),
                "*.md",
                SearchOption.AllDirectories));
        }

        var blogPosts = blogPostFiles.Select(Read).ToArray();
        foreach (var blogPostOfMarkdown in blogPosts)
        {
            if (!CheckAlbumExist(albums, blogPostOfMarkdown.Albums)
                || !CheckCategoryExist(blogPostOfMarkdown.Categories))
            {
                return (false, null, null, null, null);
            }

            ReadTagName(blogPostOfMarkdown.Tags);
        }

        return (true, blogPosts, albums, categories, tagNames.ToArray());
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

        var albumsForDb = albumSeeds.Select(x => _albumManager.CreateForSeed(x.SequenceNumber, x.Name, x.Slug, x.Cover))
            .ToList();
        await _dbContext.AddRangeAsync(albumsForDb);
        await _dbContext.SaveChangesAsync();
        return albumsForDb;
    }

    private async Task SeedBlogPosts(List<BlogPostSeedDto>? blogPostSeeds, List<Album>? albums,
        List<Category>? categories, List<Tag>? tags)
    {
        if (blogPostSeeds == null || !blogPostSeeds.Any())
        {
            return;
        }

        var allBlogPosts = new List<BlogPost>();
        foreach (var blogPostSeed in blogPostSeeds)
        {
            var albumIds = albums?.Where(x => blogPostSeed.Albums?.Contains(x.Name) ?? false).Select(x => x.Id)
                .ToArray();
            var categoryIds = categories?.Where(x => blogPostSeed.Categories?.Contains(x.Name) ?? false)
                .Select(x => x.Id).ToArray();
            var tagIds = tags?.Where(x => blogPostSeed.Tags?.Contains(x.Name) ?? false).Select(x => x.Id).ToArray();
            var blogPost = _blogPostManager.CreateForSeed(blogPostSeed.Title, blogPostSeed.Slug,
                blogPostSeed.Description, blogPostSeed.Cover, blogPostSeed.Content, blogPostSeed.Copyright,
                blogPostSeed.Author, null, blogPostSeed.OriginalTitle, blogPostSeed.OriginalLink, blogPostSeed.Banner,
                true, albumIds,
                categoryIds, tagIds, blogPostSeed.Date);
            allBlogPosts.Add(blogPost);
        }

        var pageIndex = 0;
        const int pageSize = 100;
        while (true)
        {
            var temp = allBlogPosts.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
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

    private async Task SeedLinks()
    {
        var links = JsonSerializer.Deserialize<List<LinkSeedDto>>(
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

    private static BlogPostSeedDto Read(string markdownAbsolutePath)
    {
        var blogPostOfMarkdown = new BlogPostSeedDto();
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
                blogPostOfMarkdown.Content = string.Join("\n", contentLines);
                return blogPostOfMarkdown;
            }

            if (lines[i].StartsWith(Title))
            {
                blogPostOfMarkdown.Title = lines[i][Title.Length..];
            }
            else if (lines[i].StartsWith(Slug))
            {
                blogPostOfMarkdown.Slug = lines[i][Slug.Length..];
            }
            else if (lines[i].StartsWith(Description))
            {
                blogPostOfMarkdown.Description = lines[i][Description.Length..];
            }
            else if (lines[i].StartsWith(Date))
            {
                blogPostOfMarkdown.Date = DateTime.Parse(lines[i][Date.Length..]);
            }
            else if (lines[i].StartsWith(LastModifyDate))
            {
                blogPostOfMarkdown.LastModifyDate = DateTime.Parse(lines[i][LastModifyDate.Length..]);
            }
            else if (lines[i].StartsWith(Copyright))
            {
                blogPostOfMarkdown.Copyright =
                    (CopyRightType)Enum.Parse(typeof(CopyRightType), lines[i][Copyright.Length..]);
            }
            else if (lines[i].StartsWith(Author))
            {
                blogPostOfMarkdown.Author = lines[i][Author.Length..];
            }
            else if (lines[i].StartsWith(OriginalTitle))
            {
                blogPostOfMarkdown.OriginalTitle = lines[i][OriginalTitle.Length..];
            }
            else if (lines[i].StartsWith(OriginalLink))
            {
                blogPostOfMarkdown.OriginalLink = lines[i][OriginalLink.Length..];
            }
            else if (lines[i].StartsWith(Draft))
            {
                blogPostOfMarkdown.Draft = bool.Parse(lines[i][Draft.Length..]);
            }
            else if (lines[i].StartsWith(Cover))
            {
                blogPostOfMarkdown.Cover = lines[i][Cover.Length..];
            }
            else if (lines[i].StartsWith(Albums))
            {
                blogPostOfMarkdown.Albums = lines[i][Albums.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else if (lines[i].StartsWith(Categories))
            {
                blogPostOfMarkdown.Categories =
                    lines[i][Categories.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else if (lines[i].StartsWith(Tags))
            {
                blogPostOfMarkdown.Tags = lines[i][Tags.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else if (lines[i].StartsWith(Banner))
            {
                blogPostOfMarkdown.Banner = bool.Parse(lines[i][Banner.Length..]);
            }
        }

        return blogPostOfMarkdown;
    }

    private static void Write(string markdownAbsolutePath, BlogPostSeedDto blogPostOfMarkdown)
    {
        var lines = new List<string>();
        lines.Add("---");

        if (!string.IsNullOrWhiteSpace(blogPostOfMarkdown.Title))
        {
            lines.Add($"{Title}{blogPostOfMarkdown.Title}");
        }

        if (!string.IsNullOrWhiteSpace(blogPostOfMarkdown.Slug))
        {
            lines.Add($"{Slug}{blogPostOfMarkdown.Slug}");
        }

        if (!string.IsNullOrWhiteSpace(blogPostOfMarkdown.Description))
        {
            lines.Add($"{Description}{blogPostOfMarkdown.Description}");
        }

        if (blogPostOfMarkdown.Date != default)
        {
            lines.Add($"{Date}{blogPostOfMarkdown.Date:yyyy-MM-dd HH:mm:ss}");
        }

        if (blogPostOfMarkdown.LastModifyDate != null && blogPostOfMarkdown.LastModifyDate != default(DateTime))
        {
            lines.Add($"{LastModifyDate}{blogPostOfMarkdown.LastModifyDate:yyyy-MM-dd HH:mm:ss}");
        }

        lines.Add($"{Copyright}{blogPostOfMarkdown.Copyright}");
        if (!string.IsNullOrWhiteSpace(blogPostOfMarkdown.Author))
        {
            lines.Add($"{Author}{blogPostOfMarkdown.Author}");
        }

        if (!string.IsNullOrWhiteSpace(blogPostOfMarkdown.OriginalTitle))
        {
            lines.Add($"{OriginalTitle}{blogPostOfMarkdown.OriginalTitle}");
        }

        if (!string.IsNullOrWhiteSpace(blogPostOfMarkdown.OriginalLink))
        {
            lines.Add($"{OriginalLink}{blogPostOfMarkdown.OriginalLink}");
        }

        lines.Add($"{Draft}{blogPostOfMarkdown.Draft}");

        if (!string.IsNullOrWhiteSpace(blogPostOfMarkdown.Cover))
        {
            lines.Add($"{Cover}{blogPostOfMarkdown.Cover}");
        }

        if (blogPostOfMarkdown.Albums is { Length: > 0 })
        {
            lines.Add($"{Albums}{string.Join(',', blogPostOfMarkdown.Albums)}");
        }

        if (blogPostOfMarkdown.Categories is { Length: > 0 })
        {
            lines.Add($"{Categories}{string.Join(',', blogPostOfMarkdown.Categories)}");
        }

        if (blogPostOfMarkdown.Tags is { Length: > 0 })
        {
            lines.Add($"{Tags}{string.Join(',', blogPostOfMarkdown.Tags)}");
        }

        lines.Add("---");

        if (System.IO.File.Exists(markdownAbsolutePath))
        {
            System.IO.File.Delete(markdownAbsolutePath);
        }

        System.IO.File.WriteAllLines(markdownAbsolutePath, lines);
        System.IO.File.AppendAllText(markdownAbsolutePath, $"\n{blogPostOfMarkdown.Content}");
    }
}