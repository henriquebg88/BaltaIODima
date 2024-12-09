using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Account
{
    public class LoginRequest : BaseRequest
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Password { get; set; } = string.Empty;
    }
}