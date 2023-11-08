using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proje1.Models
{
    public partial class ProjeBirContext : DbContext
    {
        public ProjeBirContext()
        {
        }

        public ProjeBirContext(DbContextOptions<ProjeBirContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlisKiraye> AlisKirayes { get; set; } = null!;
        public virtual DbSet<Menzil> Menzils { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Qesebe> Qesebes { get; set; } = null!;
        public virtual DbSet<Rayon> Rayons { get; set; } = null!;
        public virtual DbSet<Seher> Sehers { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-UAPLAQ7\\SQLEXPRESS;Database=ProjeBir;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlisKiraye>(entity =>
            {
                entity.ToTable("AlisKiraye");

                entity.Property(e => e.AlisKirayeName).HasMaxLength(20);
            });

            modelBuilder.Entity<Menzil>(entity =>
            {
                entity.ToTable("Menzil");

                entity.Property(e => e.MenzilName).HasMaxLength(30);
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.PhotoUrl).HasMaxLength(150);

                entity.HasOne(d => d.PhotoProduct)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.PhotoProductId)
                    .HasConstraintName("FK__Photo__PhotoProd__4BAC3F29");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductDate).HasMaxLength(30);

                entity.Property(e => e.ProductDescription).HasMaxLength(700);

                entity.Property(e => e.ProductLocation).HasMaxLength(100);

                entity.HasOne(d => d.ProductAlisKiraye)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductAlisKirayeId)
                    .HasConstraintName("FK__Product__Product__59063A47");

                entity.HasOne(d => d.ProductMenzil)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductMenzilId)
                    .HasConstraintName("FK__Product__Product__5441852A");

                entity.HasOne(d => d.ProductQesebe)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductQesebeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Product__Product__19DFD96B");

                entity.HasOne(d => d.ProductRayon)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductRayonId)
                    .HasConstraintName("FK__Product__Product__5165187F");

                entity.HasOne(d => d.ProductSeher)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductSeherId)
                    .HasConstraintName("FK__Product__Product__4E88ABD4");

                entity.HasOne(d => d.ProductUser)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Product__Product__3A4CA8FD");
            });

            modelBuilder.Entity<Qesebe>(entity =>
            {
                entity.ToTable("Qesebe");

                entity.Property(e => e.QesebeName).HasMaxLength(30);
            });

            modelBuilder.Entity<Rayon>(entity =>
            {
                entity.ToTable("Rayon");

                entity.Property(e => e.RayonName).HasMaxLength(30);
            });

            modelBuilder.Entity<Seher>(entity =>
            {
                entity.ToTable("Seher");

                entity.Property(e => e.SeherName).HasMaxLength(30);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusName).HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserMail).HasMaxLength(30);

                entity.Property(e => e.UserName).HasMaxLength(30);

                entity.Property(e => e.UserPassword).HasMaxLength(100);

                entity.Property(e => e.UserPhone).HasMaxLength(15);

                entity.Property(e => e.UserPhoto).HasMaxLength(100);

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatusId)
                    .HasConstraintName("FK__Users__UserStatu__71D1E811");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
