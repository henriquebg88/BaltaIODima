/*
    Criado manualmente
    Adicionar NuGet Package do EntityFramework para SQL Server
    Adicionar NuGet Package do EntityFramework Design
*/
using System.Reflection;
using Dima.API.Data.Mappings;
using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Dima.API.Data
{
    class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }
        public DbSet<Category> Categorias { get; set; } = null!;
        public DbSet<Transaction> Transacoes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new CategoryMap());
            // modelBuilder.ApplyConfiguration(new TransactionMap());
            //          Ou

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Vai varrer e procurar por todos os itens que façam uso da interface IEntityTypeConfiguration
        }
    }
}