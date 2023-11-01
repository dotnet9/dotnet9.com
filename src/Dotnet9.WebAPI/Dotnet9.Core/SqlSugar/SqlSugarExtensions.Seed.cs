using Dotnet9.Core.Entities;
using Dotnet9.Core.Options;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using Dotnet9.Core.SqlSugar.Seed;
using Easy.Core;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace SqlSugar;

public static partial class SqlSugarExtensions
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

    /// <summary>
    /// 初始化基础数据
    /// </summary>
    private static void InitData(SqlSugarScope client)
    {
        List<SysUser> users = new List<SysUser>()
        {
            new SysUser()
            {
                Id = 1,
                Account = "admin",
                Password = "9658b68df5e6208e97ae6c8f4eac6c39",
                Name = "管理员",
                Gender = Gender.Female,
                NickName = "管理员",
                Avatar = "https://oss.okay123.top/oss//2023/07/13/Yln0lzZQLN.jpg",
                CreatedUserId = 1
            },
            new SysUser()
            {
                Id = 50048753934341,
                Account = "test",
                Password = "65caa9533b8d9f336bd07919dd9bf74e",
                Name = "测试",
                Gender = Gender.Female,
                NickName = "测试",
                CreatedUserId = 1
            }
        };
        client.Storageable(users).ToStorage().AsInsertable.ExecuteCommand();

        string path = Path.Combine(AppContext.BaseDirectory, "InitData");
        var dir = new DirectoryInfo(path);
        var files = dir.GetFiles("*.json").ToList();
        InitDataFromFile(client, users[0]);
        foreach (var file in files)
        {
            using var reader = file.OpenText();
            string s = reader.ReadToEnd();
            var table = JsonConvert.DeserializeObject<DataTable>(s);
            if (table.Rows.Count == 0)
            {
                continue;
            }

            table.TableName = file.Name.Replace(".json", "");

            client.Storageable(table).WhereColumns("Id").ToStorage().AsInsertable.ExecuteCommand();
        }
    }

    /// <summary>
    /// 获取初始化文件列表
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static void InitDataFromFile(SqlSugarScope client, SysUser user)
    {
        var siteOptions = App.GetConfig<SiteOptions>("Site");
        if (string.IsNullOrWhiteSpace(siteOptions.AssetsDir) || !Directory.Exists(siteOptions.AssetsDir))
        {
            return;
        }

        InitFriendLink(client, siteOptions.AssetsDir);
        InitTimelines(client, siteOptions.AssetsDir);
        var cats = InitCategory(client, siteOptions.AssetsDir, user);
        var albums = InitAlbum(client, siteOptions.AssetsDir, user);
        if (cats?.Count > 0 && albums?.Count > 0)
        {
            InitArticles(client, siteOptions, user, cats, albums);
        }
    }

    /// <summary>
    /// 初始化友情链接数据
    /// </summary>
    /// <param name="client"></param>
    /// <param name="assetsDir"></param>
    /// <exception cref="Exception"></exception>
    private static void InitFriendLink(SqlSugarScope client, string assetsDir)
    {
        var filePath = Path.Combine(assetsDir, "site", "FriendLink.json");
        if (!File.Exists(filePath))
        {
            return;
        }

        var friendLinks = JsonConvert.DeserializeObject<List<FriendLink>>(File.ReadAllText(filePath));
        var id = 0;
        friendLinks.ForEach(link =>
        {
            link.Id = ++id;
            link.Link = link.Url;
        });
        client.Storageable(friendLinks).ToStorage().AsInsertable.ExecuteCommand();
    }

    /// <summary>
    /// 初始化时间线（说说）数据
    /// </summary>
    /// <param name="client"></param>
    /// <param name="assetsDir"></param>
    /// <exception cref="Exception"></exception>
    private static void InitTimelines(SqlSugarScope client, string assetsDir)
    {
        var filePath = Path.Combine(assetsDir, "site", "timelines.json");
        if (!File.Exists(filePath))
        {
            return;
        }

        var timelines = JsonConvert.DeserializeObject<List<TimelineSeedDto>>(File.ReadAllText(filePath));
        var id = 0;
        var allTimelines = timelines.Select(timeline => new Talks()
        {
            Id = ++id,
            Content = timeline.Content,
            CreatedTime = timeline.Time,
            IsAllowComments = true
        }).ToList();
        client.Storageable(allTimelines).ToStorage().AsInsertable.ExecuteCommand();
    }

    /// <summary>
    /// 初始化分类
    /// </summary>
    /// <param name="client"></param>
    /// <param name="assetsDir"></param>
    /// <param name="user"></param>
    /// <exception cref="Exception"></exception>
    private static List<Categories>? InitCategory(SqlSugarScope client, string assetsDir, SysUser user)
    {
        void UpdateCategory(List<Categories> all, Categories category, string assetsUrl, ref long id)
        {
            if (category.Children.Count <= 0)
            {
                return;
            }

            foreach (var cat in category.Children)
            {
                cat.Id = ++id;
                cat.ParentId = category.Id;
                cat.CreatedUserId = user.Id;
                cat.Cover = $"{assetsUrl}/{cat.Cover}";
                all.Add(cat);

                UpdateCategory(all, cat, assetsUrl, ref id);
            }
        }

        var filePath = Path.Combine(assetsDir, "cats", "category.json");
        if (!File.Exists(filePath))
        {
            return null;
        }

        var siteOptions = App.GetConfig<SiteOptions>("Site");
        if (string.IsNullOrWhiteSpace(siteOptions.AssetsUrl))
        {
            throw new Exception("请配置资源Url");
        }

        var categories = JsonConvert.DeserializeObject<List<Categories>>(File.ReadAllText(filePath));
        var allCategories = new List<Categories>();


        long id = 0;
        foreach (var cat in categories)
        {
            cat.Id = ++id;
            cat.ParentId = null;
            cat.CreatedUserId = user.Id;
            cat.Cover = $"{siteOptions.AssetsUrl}/{cat.Cover}";
            allCategories.Add(cat);

            UpdateCategory(allCategories, cat, siteOptions.AssetsUrl, ref id);
        }

        client.Storageable(allCategories).ToStorage().AsInsertable.ExecuteCommand();
        return allCategories;
    }

    /// <summary>
    /// 初始化专辑
    /// </summary>
    /// <param name="client"></param>
    /// <param name="assetsDir"></param>
    /// <param name="user"></param>
    /// <exception cref="Exception"></exception>
    private static List<Albums>? InitAlbum(SqlSugarScope client, string assetsDir, SysUser user)
    {
        var filePath = Path.Combine(assetsDir, "albums", "album.json");
        if (!File.Exists(filePath))
        {
            return null;
        }

        var siteOptions = App.GetConfig<SiteOptions>("Site");
        if (string.IsNullOrWhiteSpace(siteOptions.AssetsUrl))
        {
            throw new Exception("请配置资源Url");
        }

        var albums = JsonConvert.DeserializeObject<List<Albums>>(File.ReadAllText(filePath));


        long id = 0;
        foreach (var album in albums)
        {
            album.Id = ++id;
            album.CreatedUserId = user.Id;
            album.Cover = $"{siteOptions.AssetsUrl}/{album.Cover}";
        }

        client.Storageable(albums).ToStorage().AsInsertable.ExecuteCommand();
        return albums;
    }

    /// <summary>
    /// 初始化文章
    /// </summary>
    /// <param name="client"></param>
    /// <param name="siteInfo"></param>
    /// <param name="user"></param>
    /// <param name="categories"></param>
    /// <param name="albums"></param>
    /// <exception cref="Exception"></exception>
    private static void InitArticles(SqlSugarScope client, SiteOptions siteInfo, SysUser user,
        List<Categories> categories, List<Albums> albums)
    {
        var blogPostFiles = new List<string>();
        for (var i = siteInfo.Start; i <= DateTime.Now.Year; i++)
        {
            blogPostFiles.AddRange(Directory.GetFiles(Path.Combine(siteInfo.AssetsDir, i.ToString()),
                "*.md",
                SearchOption.AllDirectories));
        }

        var articles = new List<Article>();
        var articleCategories = new List<ArticleCategory>();
        var articleAlbums = new List<ArticleAlbum>();
        var tags = new List<Tags>();
        var articleTags = new List<ArticleTag>();
        var blogPosts = blogPostFiles.Select(Read).ToArray();
        foreach (var blogPostOfMarkdown in blogPosts)
        {
            var category = categories.FirstOrDefault(cat => cat.Name == blogPostOfMarkdown.Category);
            var album = albums.FirstOrDefault(albumSeed => albumSeed.Name == blogPostOfMarkdown.Album);
            if (category == null)
            {
                throw new Exception($"文章分类 【{blogPostOfMarkdown.Category}】需要先配置");
            }

            if (!string.IsNullOrWhiteSpace(blogPostOfMarkdown.Album) && album == null)
            {
                throw new Exception($"文章分类专辑【{blogPostOfMarkdown.Album}】需要先配置");
            }

            var article = new Article()
            {
                Id = articles.Count + 1,
                Title = blogPostOfMarkdown.Title,
                Slug = blogPostOfMarkdown.Slug,
                ShortSlug = blogPostOfMarkdown.Slug.Encode(),
                CreationType = blogPostOfMarkdown.Copyright,
                Cover = $"{blogPostOfMarkdown.Cover}",
                IsTop = blogPostOfMarkdown.Banner,
                Author = blogPostOfMarkdown.Author,
                Summary = blogPostOfMarkdown.Description,
                Content = blogPostOfMarkdown.Content,
                CreatedUserId = user.Id,
                CreatedTime = blogPostOfMarkdown.Date,
                PublishTime = blogPostOfMarkdown.Date,
                UpdatedTime = blogPostOfMarkdown.LastModifyDate
            };
            if (article.CreationType == CreationType.Original)
            {
                article.Author = siteInfo.Owner;
            }
            else
            {
                article.Link = blogPostOfMarkdown.OriginalLink;
            }

            articles.Add(article);
            articleCategories.Add(new ArticleCategory()
            {
                Id = articleCategories.Count + 1,
                ArticleId = article.Id,
                CategoryId = category.Id
            });
            if (album != null)
            {
                articleAlbums.Add(new ArticleAlbum()
                {
                    Id = articleAlbums.Count + 1,
                    ArticleId = article.Id,
                    AlbumId = album.Id
                });
            }

            if (blogPostOfMarkdown.Tags?.Any() == true)
            {
                foreach (var tagItem in blogPostOfMarkdown.Tags)
                {
                    var tag = tags.FirstOrDefault(t => t.Name == tagItem);
                    if (tag == null)
                    {
                        tag = new Tags()
                        {
                            Id = tags.Count + 1,
                            Name = tagItem,
                            Cover = "https://img1.dotnet9.com/site/logo.png",
                            CreatedUserId = user.Id
                        };
                        tags.Add(tag);
                    }

                    var articleTag = articleTags.FirstOrDefault(at => at.ArticleId == article.Id && at.TagId == tag.Id);
                    if (articleTag == null)
                    {
                        articleTags.Add(new ArticleTag()
                        {
                            Id = articleTags.Count + 1,
                            ArticleId = article.Id,
                            TagId = tag.Id
                        });
                    }
                }
            }
        }

        var pageIndex = 0;
        const int pageSize = 100;
        while (true)
        {
            var temp = articles.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
            if (temp.Length <= 0)
            {
                break;
            }


            client.Storageable(temp).ToStorage().AsInsertable.ExecuteCommand();

            pageIndex++;
        }

        client.Storageable(tags).ToStorage().AsInsertable.ExecuteCommand();
        client.Storageable(articleCategories).ToStorage().AsInsertable.ExecuteCommand();
        client.Storageable(articleAlbums).ToStorage().AsInsertable.ExecuteCommand();
        client.Deleteable<ArticleTag>().ExecuteCommand();
        client.Storageable(articleTags).ToStorage().AsInsertable.ExecuteCommand();
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
                    (CreationType)Enum.Parse(typeof(CreationType), lines[i][Copyright.Length..]);
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
                blogPostOfMarkdown.Album = lines[i][Albums.Length..];
            }
            else if (lines[i].StartsWith(Categories))
            {
                blogPostOfMarkdown.Category =
                    lines[i][Categories.Length..];
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
}