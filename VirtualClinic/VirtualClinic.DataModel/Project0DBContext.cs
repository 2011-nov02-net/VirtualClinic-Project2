using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class Project0DBContext : DbContext
    {
        public Project0DBContext()
        {
        }

        public Project0DBContext(DbContextOptions<Project0DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Invintory> Invintories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
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

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasMaxLength(2000);

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AppointmentDoctor_FK");

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

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers", "Store");

                entity.HasIndex(e => new { e.FirstName, e.LastName, e.MiddleInitial }, "Inv_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.MiddleInitial)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StoreLocation).HasMaxLength(100);

                entity.HasOne(d => d.StoreLocationNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.StoreLocation)
                    .HasConstraintName("store_FK");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctors", "Clinic");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<Invintory>(entity =>
            {
                entity.HasKey(e => new { e.StoreLocation, e.ItemName })
                    .HasName("Inv_CPK");

                entity.ToTable("Invintory", "Store");

                entity.Property(e => e.StoreLocation).HasMaxLength(100);

                entity.Property(e => e.ItemName).HasMaxLength(50);

                entity.HasOne(d => d.ItemNameNavigation)
                    .WithMany(p => p.Invintories)
                    .HasForeignKey(d => d.ItemName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InvItm_FK");

                entity.HasOne(d => d.StoreLocationNavigation)
                    .WithMany(p => p.Invintories)
                    .HasForeignKey(d => d.StoreLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InvLoc_FK");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemName)
                    .HasName("Item_pk");

                entity.ToTable("Items", "Store");

                entity.Property(e => e.ItemName).HasMaxLength(50);

                entity.Property(e => e.ItemPrice).HasColumnType("money");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.LocationName)
                    .HasName("PK__Location__F946BB85A0BF01B5");

                entity.ToTable("Location", "Store");

                entity.Property(e => e.LocationName).HasMaxLength(100);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders", "Store");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.OrderTime).HasColumnType("datetime");

                entity.Property(e => e.OrderTotal).HasColumnType("money");

                entity.Property(e => e.StoreLocation)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerOrders_FK");

                entity.HasOne(d => d.StoreLocationNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StoreOrders_FK");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ItemId })
                    .HasName("ori_PK");

                entity.ToTable("OrderItems", "Store");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ItemId)
                    .HasMaxLength(50)
                    .HasColumnName("ItemID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderItemItem_FK");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderItemOrder_FK");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patients", "Clinic");

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
                entity.ToTable("Timeslots", "Clinic");

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Timeslots)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("TimeslotAppointment_FK");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Timeslots)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Doctor_FK");
            });

            modelBuilder.Entity<Vital>(entity =>
            {
                entity.ToTable("Vitals", "Clinic");

                entity.Property(e => e.Temperature).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
