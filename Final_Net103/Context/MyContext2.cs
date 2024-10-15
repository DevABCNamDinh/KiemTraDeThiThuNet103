using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Final_Net103.DomainClass;

namespace Final_Net103.Context
{
    public partial class MyContext2 : DbContext
    {
        public MyContext2()
        {
        }

        public MyContext2(DbContextOptions<MyContext2> options)
            : base(options)
        {
        }

        public virtual DbSet<Ca> Cas { get; set; } = null!;
        public virtual DbSet<Dongvat> Dongvats { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source= DESKTOP-6F71DIH\\SQLEXPRESS;Initial Catalog=FINAL_NET103_ABC;\nIntegrated Security=True;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ca>(entity =>
            {
                entity.HasOne(d => d.IdcaNavigation)
                    .WithMany(p => p.Cas)
                    .HasForeignKey(d => d.Idca)
                    .HasConstraintName("FK_CA_DV");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
