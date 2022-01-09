namespace Dotnet9
{
    public static class Dotnet9DomainErrorCodes
    {
        public static class Tags
        {
            public const string TagAlreadyExist = "Dotnet9:Tag:0001";
        }

        public static class Albums
        {
            public const string AlbumAlreadyExist = "Dotnet9:Album:0001";
        }

        public static class BlogPosts
        {
            public const string TitleAlreadyExist = "Dotnet9:BlogPost:0001";
            public const string SlugAlreadyExist = "Dotnet9:BlogPost:0002";
        }

        public static class Categories
        {
            public const string CagetoryAlreadyExist = "Dotnet9:Category:0001";
        }

        public static class UrlLinks
        {
            public const string NameAlreadyExist = "Dotnet9:UrlLink:0001";
            public const string UrlAlreadyExist = "Dotnet9:UrlLink:0002";
        }
    }
}
