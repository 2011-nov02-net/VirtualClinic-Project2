using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class CliniqueDbContext : DbContext 
    {
        public CliniqueDbContext()
        {
        }

        public CliniqueDbContext(DbContextOptions<CliniqueDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientReport> PatientReports { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<Timeslot> Timeslots { get; set; }
        public virtual DbSet<Vital> Vitals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointments", "Clinic");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Notes).HasMaxLength(2000);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ApointmentPatient_FK");

                entity.HasOne(d => d.Vitals)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.VitalsId)
                    .HasConstraintName("ApointmentVitals_FK");
            });
      

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctors", "Clinic");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(100);
            });


            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patients", "Clinic");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Insurance).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Ssn)
                    .HasMaxLength(11)
                    .HasColumnName("SSN");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PatientsDoctor_FK");
            });

            modelBuilder.Entity<PatientReport>(entity =>
            {
                entity.ToTable("PatientReports", "Clinic");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Information).HasMaxLength(2000);

                entity.Property(e => e.ReportTime).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientReports)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ReportPatient_FK");

                entity.HasOne(d => d.Vitals)
                    .WithMany(p => p.PatientReports)
                    .HasForeignKey(d => d.VitalsId)
                    .HasConstraintName("ReportVitals_FK");
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("Prescriptions", "Clinic");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Drug).HasMaxLength(300);

                entity.Property(e => e.Information).HasMaxLength(2000);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PerscriptionDoctor_FK");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PerscriptionPatient_FK");
            });

            modelBuilder.Entity<Timeslot>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Timeslots", "Clinic");

                entity.HasOne(d => d.Appointment)
                    .WithMany()
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("TimeslotAppointment_FK");

                entity.HasOne(d => d.Doctor)
                    .WithMany()
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Doctor_FK");
            });

            modelBuilder.Entity<Vital>(entity =>
            {
                entity.ToTable("Vitals", "Clinic");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Temperature).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
