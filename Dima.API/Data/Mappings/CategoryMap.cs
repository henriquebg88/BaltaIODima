using Dima.Core.Enums;
using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.API.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.id);

            builder.ToTable("Category");

            builder.Property(x => x.title)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnType(DBTypes.NVARCHAR);

            builder.Property(x => x.description)
                .IsRequired(false)
                .HasMaxLength(255)
                .HasColumnType(DBTypes.NVARCHAR);

            builder.Property(x => x.userID)
                .IsRequired()
                .HasMaxLength(160)
                .HasColumnType(DBTypes.NVARCHAR);
        }
    }
}