using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tim.Manager.Db.Entities;

namespace Tim.Manager.Db.Data.Configurations
{
    internal class PassItemConfiguration : IEntityTypeConfiguration<PassItem>
    {
        public void Configure(EntityTypeBuilder<PassItem> builder)
        {
            builder.ToTable("PassItems");

            builder.Property(i => i.Name)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(i => i.UserId)
                .IsRequired()
                .HasColumnType("nvarchar(450)");

            builder.Property(i => i.UserName)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(i => i.Password)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(i => i.Uri)
                .HasColumnType("nvarchar(max)");

            builder.Property(i => i.Created)
                .HasColumnType("datetime2")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getdate()");

            builder.Property(i => i.Discription)
                .HasColumnType("nvarchar(max)");

            builder.HasIndex(i => new { i.UserId, i.Name })
                .HasName("IDX_PASS_ITEM_USER_ID_AND_NAME");

            builder.HasKey(i => new { i.UserId, i.Name });
        }
    }
}