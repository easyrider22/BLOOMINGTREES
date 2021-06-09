using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BLOOMINGTREES_WpfApp
{
    public partial class BloomingTreesContext : DbContext
    {
        public BloomingTreesContext()
        {
        }

        public BloomingTreesContext(DbContextOptions<BloomingTreesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ImagesFlowers> ImagesFlowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=BloomingTrees;integrated security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImagesFlowers>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.ItemDescription)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ItemImagePath)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ItemPrice).HasColumnType("money");

                entity.Property(e => e.ItemPurchaseDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemSupplier)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
