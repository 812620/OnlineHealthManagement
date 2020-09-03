using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineHealthManagement.Models
{
    public partial class OnlineHMContext : DbContext
    {
        
        public OnlineHMContext(DbContextOptions<OnlineHMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId)
                    .HasColumnName("Admin_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DId).HasColumnName("D_Id");

                entity.Property(e => e.PId).HasColumnName("P_Id");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.D)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.DId)
                    .HasConstraintName("FK__Appointmen__D_Id__30F848ED");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PId)
                    .HasConstraintName("FK__Appointmen__P_Id__300424B4");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId)
                    .HasColumnName("Doctor_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsAvailable)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Specification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.PatientId)
                    .HasColumnName("Patient_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
