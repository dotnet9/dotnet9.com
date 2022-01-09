using AutoMapper;
using Dotnet9.Abouts;
using Dotnet9.Albums;
using Dotnet9.Blogs;
using Dotnet9.Categories;
using Dotnet9.Comments;
using Dotnet9.Contacts;
using Dotnet9.Privacies;
using Dotnet9.Ratings;
using Dotnet9.Tags;
using Dotnet9.UrlLinks;

namespace Dotnet9;

public class Dotnet9ApplicationAutoMapperProfile : Profile
{
    public Dotnet9ApplicationAutoMapperProfile()
    {
        CreateMap<About, AboutDto>();
        CreateMap<UpdateAboutDto, About>();
        CreateMap<Album, AlbumDto>();
        CreateMap<CreateAlbumDto, Album>();
        CreateMap<BlogPost, BlogPostDto>();
        CreateMap<CreateBlogPostDto, BlogPost>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<Comment, CommentDto>();
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<Contact, ContactDto>();
        CreateMap<CreateContactDto, Contact>();
        CreateMap<Privacy, PrivacyDto>();
        CreateMap<UpdatePrivacyDto, Privacy>();
        CreateMap<Rating, RatingDto>();
        CreateMap<CreateRatingDto, Rating>();
        CreateMap<Tag, TagDto>();
        CreateMap<CreateTagDto, Tag>();
        CreateMap<UrlLink, UrlLinkDto>();
        CreateMap<CreateUrlLinkDto, UrlLink>();
    }
}