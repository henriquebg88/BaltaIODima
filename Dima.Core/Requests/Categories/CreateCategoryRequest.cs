using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories
{
    public class CreateCategoryRequest : BaseRequest
    {
        [Required(ErrorMessage = "Título inválido")]
        [MaxLength(80, ErrorMessage = "Máximo de 80 carácteres")]
        public string title { get; set; } = null!;
        [Required(ErrorMessage = "Descrição inválida")]
        public string description { get; set; } = null!;
    }
}