using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClubEqui_V_1_1.Models
{
    public partial class ClubEquitationBDContext : DbContext
    {
        public ClubEquitationBDContext()
        {
        }

        public ClubEquitationBDContext(DbContextOptions<ClubEquitationBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Seance> Seances { get; set; }
        public virtual DbSet<Tach> Taches { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("PK__Client__A6A610D453048F92");

                entity.ToTable("Client");

                entity.Property(e => e.IdClient).HasColumnName("idClient");

                entity.Property(e => e.DateInscription)
                    .HasColumnType("datetime")
                    .HasColumnName("dateInscription");

                entity.Property(e => e.DateNais)
                    .HasColumnType("date")
                    .HasColumnName("dateNais");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IdentityNum)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("identityNum");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.MotPasse)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("motPasse");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nom");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.Photo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("prenom");

                entity.Property(e => e.Telephone).HasColumnName("telephone");

                entity.Property(e => e.ValiditeAssurence)
                    .HasColumnType("datetime")
                    .HasColumnName("validiteAssurence");
            });

            modelBuilder.Entity<Seance>(entity =>
            {
                entity.HasKey(e => e.IdSeance)
                    .HasName("PK__Seance__7CD10F7DC226EDD6");

                entity.ToTable("Seance");

                entity.Property(e => e.IdSeance).HasColumnName("idSeance");

                entity.Property(e => e.Commentaires).HasColumnName("commentaires");

                entity.Property(e => e.DateDebut)
                    .HasColumnType("datetime")
                    .HasColumnName("dateDebut");

                entity.Property(e => e.DureeMinutes).HasColumnName("dureeMinutes");

                entity.Property(e => e.IdClient).HasColumnName("idClient");

                entity.Property(e => e.IdMoniteur).HasColumnName("idMoniteur");

                entity.Property(e => e.IdPayement).HasColumnName("idPayement");

                entity.Property(e => e.IsDone).HasColumnName("isDone");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Seances)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("FK__Seance__idClient__286302EC");

                entity.HasOne(d => d.IdMoniteurNavigation)
                    .WithMany(p => p.Seances)
                    .HasForeignKey(d => d.IdMoniteur)
                    .HasConstraintName("FK__Seance__idMonite__29572725");
            });

            modelBuilder.Entity<Tach>(entity =>
            {
                entity.HasKey(e => e.IdTask)
                    .HasName("PK__Tasks__C3E0F4DA99E1710B");

                entity.Property(e => e.IdTask).HasColumnName("idTask");

                entity.Property(e => e.DateDebut)
                    .HasColumnType("datetime")
                    .HasColumnName("dateDebut");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.DureeMinutes).HasColumnName("dureeMinutes");

                entity.Property(e => e.IsDone).HasColumnName("isDone");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UserAttached).HasColumnName("userAttached");

                entity.HasOne(d => d.UserAttachedNavigation)
                    .WithMany(p => p.Taches)
                    .HasForeignKey(d => d.UserAttached)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Tasks__userAttac__2C3393D0");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.IdUtilisateur)
                    .HasName("PK__Utilisat__5366DB1952FDFA51");

                entity.ToTable("Utilisateur");

                entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");

                entity.Property(e => e.ContractDate)
                    .HasColumnType("datetime")
                    .HasColumnName("contractDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnType("datetime")
                    .HasColumnName("lastLoginTime");

                entity.Property(e => e.MotPasse)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("motPasse");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nom");

                entity.Property(e => e.Photo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("prenom");

                entity.Property(e => e.Telephone).HasColumnName("telephone");

                entity.Property(e => e.TypeUtilsateur)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("typeUtilsateur");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
