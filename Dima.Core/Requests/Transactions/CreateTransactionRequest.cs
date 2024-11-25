using System.ComponentModel.DataAnnotations;
using Dima.Core.Enums;

namespace Dima.Core.Requests.Transactions
{
    public class CreateTransactionRequest : BaseRequest
    {
        [Required(ErrorMessage = "Título inválido")]
        [MaxLength(80, ErrorMessage = "Máximo de 80 carácteres")]
        public string Title { get; set; } = null!;

        
        [Required(ErrorMessage = "Tipo inválido")]
        public EnumTransactionType MyProperty { get; set; }

        
        [Required(ErrorMessage = "Valor inválido")]
        public Decimal Amount { get; set; }

        [Required(ErrorMessage = "Tipo inválido")]
        public EnumTransactionType Type { get; set; }


        [Required(ErrorMessage = "Categoria inválida")]
        public long CategoryId { get; set; }


        [Required(ErrorMessage = "Data inválida")]
        public DateTime? PaidOrReceivedAt { get; set; }


        [Required(ErrorMessage = "Descrição inválida")]
        public string description { get; set; } = null!;
    }
}