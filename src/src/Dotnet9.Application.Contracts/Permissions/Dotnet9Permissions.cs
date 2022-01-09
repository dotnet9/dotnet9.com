namespace Dotnet9.Permissions
{
    public static class Dotnet9Permissions
    {
        public const string GroupName = "Blog";

        public static class Tags
        {
            public const string Default = GroupName + ".Tags";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Albums
        {
            public const string Default = GroupName + ".Albums";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Abouts
        {
            public const string Default = GroupName + ".Abouts";
            public const string Edit = Default + ".Edit";
        }
    }
}