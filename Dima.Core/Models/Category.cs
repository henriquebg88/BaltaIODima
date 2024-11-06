namespace Dima.Core.Models
{
    public class Category
    {
        public long id { get; set; }
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string userID { get; set; } = string.Empty;
    }
}