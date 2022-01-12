using Dotnet9.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dotnet9.Abouts;
using Dotnet9.Albums;
using Dotnet9.Blogs;
using Dotnet9.Categories;
using Dotnet9.Comments;
using Dotnet9.Contacts;
using Dotnet9.Privacies;
using Dotnet9.Ratings;
using Dotnet9.UrlLinks;
using Newtonsoft.Json;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9;

public class Dotnet9DataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly ITagRepository _tagRepository;
    private readonly TagManager _tagManager;
    private readonly IUrlLinkRepository _urlLinkRepository;
    private readonly UrlLinkManager _urlLinkManager;
    private readonly IAlbumRepository _albumRepository;
    private readonly AlbumManager _albumManager;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly BlogPostManager _blogPostManager;
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryManager _categoryManager;
    private readonly ICommentRepository _commentRepository;
    private readonly IRepository<Contact, Guid> _contactRepository;
    private readonly IRepository<Privacy, Guid> _privacyRepository;
    private readonly IRepository<Rating, Guid> _ratingRepository;
    private readonly IAboutRepository _aboutRepository;

    public Dotnet9DataSeederContributor(
        IAboutRepository aboutRepository,
        IAlbumRepository albumRepository,
        AlbumManager albumManager,
        IBlogPostRepository blogPostRepository,
        BlogPostManager blogPostManager,
        ICategoryRepository categoryRepository,
        CategoryManager categoryManager,
        ICommentRepository commentRepository,
        IRepository<Contact, Guid> contactRepository,
        IRepository<Privacy, Guid> privacyRepository,
        IRepository<Rating, Guid> ratingRepository,
        ITagRepository tagRepository,
        TagManager tagManager,
        IUrlLinkRepository urlLinkRepository,
        UrlLinkManager urlLinkManager)
    {
        _aboutRepository = aboutRepository;
        _albumRepository = albumRepository;
        _albumManager = albumManager;
        _blogPostRepository = blogPostRepository;
        _blogPostManager = blogPostManager;
        _categoryRepository = categoryRepository;
        _categoryManager = categoryManager;
        _commentRepository = commentRepository;
        _contactRepository = contactRepository;
        _privacyRepository = privacyRepository;
        _ratingRepository = ratingRepository;
        _tagRepository = tagRepository;
        _tagManager = tagManager;
        _urlLinkRepository = urlLinkRepository;
        _urlLinkManager = urlLinkManager;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _aboutRepository.GetCountAsync() <= 0)
        {
            await _aboutRepository.InsertAsync(new About
                { Details = "This is the content of the \"about\" page, which is actually saved as markdown text;" });
        }

        var albums = new List<Album>();
        if (await _albumRepository.GetCountAsync() <= 0)
        {
            if (File.Exists(ConfigurationConst.BlogBaseFilePath))
            {
                var baseInfo =
                    JsonConvert.DeserializeObject<BlogBaseDto>(
                        await File.ReadAllTextAsync(ConfigurationConst.BlogBaseFilePath));
                if (baseInfo is { Albums: { } })
                {
                    foreach (var album in baseInfo.Albums)
                    {
                        var albumFromManager = await _albumManager.CreateAsync(album.Name, album.Cover,
                            album.Name);
                        await _albumRepository.InsertAsync(albumFromManager);
                        albums.Add(albumFromManager);
                    }
                }
            }
            else
            {
                await _albumRepository.InsertAsync(
                    await _albumManager.CreateAsync("WPF", "https://img1.dotnet9.com/album_wpf.png",
                        "WPF open source project")
                );
                await _albumRepository.InsertAsync(
                    await _albumManager.CreateAsync("Winform", "https://img1.dotnet9.com/album_winform.png",
                        "Winform open source project")
                );
            }
        }

        var categories = new List<Category>();
        var tags = new List<Tag>();
        if (await _blogPostRepository.GetCountAsync() <= 0)
        {
            var blogPostFiles =
                Directory.GetFiles(ConfigurationConst.BlogPostDirPath, "*.info", SearchOption.AllDirectories);
            if (blogPostFiles.Length > 0)
            {
                foreach (var blogInfoPath in blogPostFiles)
                {
                    var blogInfoText = await File.ReadAllTextAsync(blogInfoPath);
                    var blogInfoDto = JsonConvert.DeserializeObject<BlogSeedDto>(blogInfoText);
                    blogInfoDto!.Content = await File.ReadAllTextAsync(blogInfoPath.Replace("info", "md"));

                    await _blogPostRepository.InsertAsync(
                        await _blogPostManager.CreateAsync(blogInfoDto!.Title, blogInfoDto!.Title,
                            blogInfoDto!.BriefDescription, blogInfoDto.Content,
                            blogInfoDto!.Cover, (CopyrightType)Enum.Parse(typeof(CopyrightType),
                                blogInfoDto.CopyrightType ??= CopyrightType.Default.ToString())));

                    if (blogInfoDto.Albums != null && blogInfoDto.Albums.Any())
                    {
                        foreach (var albumName in blogInfoDto.Albums)
                        {
                            var existAlbum = albums.FirstOrDefault(album =>
                                album.Name.ToLower() == albumName.ToLower());
                            if (existAlbum != null)
                            {
                                continue;
                            }

                            existAlbum =
                                await _albumManager.CreateAsync(albumName, string.Empty, string.Empty);
                            albums.Add(existAlbum);
                            await _albumRepository.InsertAsync(existAlbum);
                        }
                    }

                    if (blogInfoDto.Categories != null && blogInfoDto.Categories.Any())
                    {
                        foreach (var categoryName in blogInfoDto.Categories)
                        {
                            var existCategory = categories.FirstOrDefault(category =>
                                category.Name.ToLower() == categoryName.ToLower());
                            if (existCategory != null)
                            {
                                continue;
                            }

                            existCategory =
                                await _categoryManager.CreateAsync(categoryName, string.Empty, string.Empty);
                            categories.Add(existCategory);
                            await _categoryRepository.InsertAsync(existCategory);
                        }
                    }

                    if (blogInfoDto.Tags != null && blogInfoDto.Tags.Any())
                    {
                        foreach (var tagName in blogInfoDto.Tags)
                        {
                            var existTag = tags.FirstOrDefault(tag =>
                                tag.Name.ToLower() == tagName.ToLower());
                            if (existTag != null)
                            {
                                continue;
                            }

                            existTag =
                                await _tagManager.CreateAsync(tagName, string.Empty);
                            tags.Add(existTag);
                            await _tagRepository.InsertAsync(existTag);
                        }
                    }
                }
            }
            else
            {
                await _blogPostRepository.InsertAsync(
                    await _blogPostManager.CreateAsync("WPF UI Design", "wpf-ui-design",
                        "A beautiful and generous WPF design", "This is a test article",
                        "https://img1.dotnet9.com/2022/01/10/cover_01.jpeg", CopyrightType.Default));

                await _blogPostRepository.InsertAsync(
                    await _blogPostManager.CreateAsync("Winform UI Design", "winform-ui-design",
                        "A beautiful and generous winform design", "This is a test article",
                        "https://img1.dotnet9.com/2022/01/10/cover_02.jpeg", CopyrightType.Reprint, "lequ.co",
                        "Winform UI Design", "https://lequ.co/?p=2277"));
            }
        }

        if (await _categoryRepository.GetCountAsync() <= 0 && categories.Count = 0)
        {
            await _categoryRepository.InsertAsync(
                await _categoryManager.CreateAsync("WPF", "https://img1.dotnet9.com/album_wpf.png",
                    "WPF open source project")
            );
            await _categoryRepository.InsertAsync(
                await _categoryManager.CreateAsync("Winform", "https://img1.dotnet9.com/album_winform.png",
                    "Winform open source project")
            );
        }

        if (await _commentRepository.GetCountAsync() <= 0)
        {
            await _commentRepository.InsertAsync(new Comment
            {
                RepliedCommentId = Guid.NewGuid(),
                Text = "This article is good, thank you for sharing, I learned a lot of knowledge!"
            });
        }

        if (await _contactRepository.GetCountAsync() <= 0)
        {
            await _contactRepository.InsertAsync(new Contact(Guid.NewGuid(), "乐趣课堂", "test@gmail.com",
                "I'm a little confused about this article: https://lequ.co/?p=2277",
                "The login window is written like this. The article looks right to me. I realize the correct error. Can the webmaster add a friend to help me see it?"));
        }

        if (await _privacyRepository.GetCountAsync() <= 0)
        {
            await _privacyRepository.InsertAsync(new Privacy { Details = "This is the privacy policy of the website" });
        }

        if (await _ratingRepository.GetCountAsync() <= 0)
        {
            await _ratingRepository.InsertAsync(new Rating() { StarCount = 3 });
        }

        if (await _tagRepository.GetCountAsync() <= 0 && tags.Count <= 0)
        {
            await _tagRepository.InsertAsync(
                await _tagManager.CreateAsync("C#", "Language")
            );
            await _tagRepository.InsertAsync(
                await _tagManager.CreateAsync("C++", "Language")
            );
        }

        if (await _urlLinkRepository.GetCountAsync() <= 0)
        {
            await _urlLinkRepository.InsertAsync(
                await _urlLinkManager.CreateAsync("乐趣课堂lequ.co", "https://lequ.co",
                    "A website for sharing software development technology", 1)
            );
        }
    }
}