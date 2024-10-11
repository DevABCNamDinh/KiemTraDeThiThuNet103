using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KiemTraDeThiTHu1.models
{
    public partial class ThucVatDB2Context : DbContext
    {
        public ThucVatDB2Context()
        {
        }

        public ThucVatDB2Context(DbContextOptions<ThucVatDB2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Hoa> Hoas { get; set; } = null!;
        public virtual DbSet<ThucVat> ThucVats { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-6F71DIH\\SQLEXPRESS;Database=ThucVatDB2;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hoa>(entity =>
            {
                entity.ToTable("Hoa");

                entity.Property(e => e.HoaId).HasColumnName("HoaID");

                entity.Property(e => e.TenHoa).HasMaxLength(100);

                entity.Property(e => e.ThucVatId).HasColumnName("ThucVatID");

                entity.HasOne(d => d.ThucVat)
                    .WithMany(p => p.Hoas)
                    .HasForeignKey(d => d.ThucVatId)
                    .HasConstraintName("FK__Hoa__ThucVatID__398D8EEE");
            });

            modelBuilder.Entity<ThucVat>(entity =>
            {
                entity.ToTable("ThucVat");

                entity.Property(e => e.ThucVatId).HasColumnName("ThucVatID");

                entity.Property(e => e.TenThucVat).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
