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
            builder.HasKey(x => x.id);

            builder.ToTable("Transaction");

            builder.Property(x => x.title)
                .IsRequired()
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(80);

            builder.Property(x => (int)x.type)
                .IsRequired()
                .HasColumnType(DBTypes.SMALLINT);

            builder.Property(x => x.amount)
                .IsRequired()
                .HasColumnType(DBTypes.MONEY);

            builder.Property(x => x.createdAt)
                .IsRequired();

            builder.Property(x => x.paidOrReceivedAt)
                .IsRequired(false);
            
            builder.Property(x => x.categoryId)
                .IsRequired()
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(160);

            builder.Property(x => x.userId)
                .IsRequired()
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(160);
        }
    }
}