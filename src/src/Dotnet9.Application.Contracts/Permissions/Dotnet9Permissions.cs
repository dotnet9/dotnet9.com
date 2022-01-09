namespace Dotnet9.Permissions
{
    public static class Dotnet9Permissions
    {
        public const string GroupName = "Blog";

        public static class Abouts
        {
            public const string Default = GroupName + ".Abouts";
            public const string Edit = Default + ".Edit";
        }

        public static class Albums
        {
            public const string Default = GroupName + ".Albums";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class BlogPosts
        {
            public const string Default = GroupName + ".BlogPosts";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Categories
        {
            public const string Default = GroupName + ".Categories";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Comments
        {
            public const string Default = GroupName + ".Comments";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Contacts
        {
            public const string Default = GroupName + ".Contacts";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Privacies
        {
            public const string Default = GroupName + ".Privacies";
            public const string Edit = Default + ".Edit";
        }

        public static class Ratings
        {
            public const string Default = GroupName + ".Ratings";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Tags
        {
            public const string Default = GroupName + ".Tags";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class UrlLinks
        {
            public const string Default = GroupName + ".UrlLinks";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}