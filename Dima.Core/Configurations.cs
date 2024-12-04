namespace Dima.Core
{
    public static class Configurations
    {
        public const int DefaultPageSize = 25;
        public const int DefaultPageNumber = 1;
        public const int DefaultStatusCode = 200;

        public static string connectionString { get; set; } = "";
        public static string FrontendURL { get; set; } = "";
        public static string BackendURL { get; set; } = "";
    }
}