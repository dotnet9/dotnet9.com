namespace Dotnet9.Permissions
{
    public static class Dotnet9Permissions
    {
        public const string GroupName = "Dotnet9";

        public static class Tags
        {
            public const string Default = GroupName + ".Tags";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}