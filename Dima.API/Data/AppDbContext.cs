/*
    Criado manualmente
    Adicionar NuGet Package do EntityFramework para SQL Server
    Adicionar NuGet Package do EntityFramework Design
*/
using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Dima.API.Data
{
    class AppDbContext : DbContext
    {
        public DbSet<Category> Categorias { get; set; } = null!;
        public DbSet<Transaction> Transacoes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder mb){
            
        }
    }
}