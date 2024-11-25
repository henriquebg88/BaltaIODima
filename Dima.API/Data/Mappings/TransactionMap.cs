using Dima.Core.Enums;
using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.API.Data.Mappings
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("Transaction");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(80);

            builder.Property(x => (int)x.Type)
                .IsRequired()
                .HasColumnType(DBTypes.SMALLINT);

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType(DBTypes.MONEY);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.PaidOrReceivedAt)
                .IsRequired(false);
            
            builder.Property(x => x.CategoryId)
                .IsRequired()
                .HasColumnType(DBTypes.BIGINT);
                

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(160);
        }
    }
}