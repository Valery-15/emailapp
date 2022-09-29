using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EmailApp
{
    public partial class emaildbContext : DbContext
    {
        public emaildbContext()
        {
        }

        public emaildbContext(DbContextOptions<emaildbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Letters> Letters { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-KG2I69C;Database=emaildb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Letters>(entity =>
            {
                entity.ToTable("letters");

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.Recipient)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.SendDateTime).HasColumnType("datetime");

                entity.Property(e => e.Sender)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Theme)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.RecipientNavigation)
                    .WithMany(p => p.LettersRecipientNavigation)
                    .HasForeignKey(d => d.Recipient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_letters_users1");

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.LettersSenderNavigation)
                    .HasForeignKey(d => d.Sender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_letters_users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("users");

                entity.Property(e => e.Username).HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
