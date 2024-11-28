using Microsoft.AspNetCore.Identity;

namespace Dima.API.Models
{
    public class User : IdentityUser<long> //Chave primária. padrão é string.
    {
        public List<IdentityRole<long>>? Roles { get; set; }
    }
}