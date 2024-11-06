using Dima.Core.Enums;

namespace Dima.Core.Models
{
    public class Transaction
    {
        public long id { get; set; }
        public string title { get; set; } = string.Empty;

        
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime? paidOrReceivedAt { get; set; }

        public EnumTransactionType  type { get; set; }


        public decimal amount { get; set; }

        public long categoryId { get; set; }
        public string userId { get; set; } = string.Empty;
        public required Category Category { get; set; }
    }
}