/*
    Criado manualmente
    Adicionar NuGet Package do EntityFramework para SQL Server
    Adicionar NuGet Package do EntityFramework Design
*/
using System.Reflection;
using Dima.API.Data.Mappings;
using Dima.API.Models;
using Dima.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dima.API.Data
{
    public class AppDbContext 
    : IdentityDbContext //Antes de usar o identity era DbContext
    <
        User, 
        IdentityRole<long>, 
        long,
        IdentityUserClaim<long>,
        IdentityUserRole<long>,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>
    > //Para customizar o usuário. Role tem que ser passado junto mesmo que não seja alterado.
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }
        public DbSet<Category> Categorias { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new CategoryMap());
            // modelBuilder.ApplyConfiguration(new TransactionMap());
            //          Ou

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Vai varrer e procurar por todos os itens que façam uso da interface IEntityTypeConfiguration
        }
    }
}