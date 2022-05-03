using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAD_Project.DB
{
    public partial class DAD_HarpreetContext : DbContext
    {
        public DAD_HarpreetContext()
        {
        }

        public DAD_HarpreetContext(DbContextOptions<DAD_HarpreetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IndividualTruck> IndividualTrucks { get; set; }
        public virtual DbSet<TruckCustomer> TruckCustomers { get; set; }
        public virtual DbSet<TruckEmployee> TruckEmployees { get; set; }
        public virtual DbSet<TruckFeature> TruckFeatures { get; set; }
        public virtual DbSet<TruckFeatureAssociation> TruckFeatureAssociations { get; set; }
        public virtual DbSet<TruckModel> TruckModels { get; set; }
        public virtual DbSet<TruckPerson> TruckPeople { get; set; }
        public virtual DbSet<TruckRental> TruckRentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=citizen.manukautech.info,6306;Initial Catalog=DAD_Harpreet;Persist Security Info=True;User ID=DAD_Harpreet;Password=DAD_1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndividualTruck>(entity =>
            {
                entity.HasKey(e => e.TruckId);

                entity.ToTable("IndividualTruck");

                entity.Property(e => e.TruckId).HasColumnName("TruckID");

                entity.Property(e => e.AdvanceDepositRequired).HasColumnType("money");

                entity.Property(e => e.Colour)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DailyRentalPrice).HasColumnType("money");

                entity.Property(e => e.DateImported).HasColumnType("date");

                entity.Property(e => e.FuelType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationExpiryDate).HasColumnType("date");

                entity.Property(e => e.RegistrationNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Transmission)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TruckModelId).HasColumnName("TruckModelID");

                entity.Property(e => e.WofexpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("WOFExpiryDate");

                entity.HasOne(d => d.TruckModel)
                    .WithMany(p => p.IndividualTrucks)
                    .HasForeignKey(d => d.TruckModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IndividualTruck_TruckModel");
            });

            modelBuilder.Entity<TruckCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("TruckCustomer");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerID");

                entity.Property(e => e.LicenseExpiryDate).HasColumnType("date");

                entity.Property(e => e.LicenseNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithOne(p => p.TruckCustomer)
                    .HasForeignKey<TruckCustomer>(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TruckCustomer_TruckPerson");
            });

            modelBuilder.Entity<TruckEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("TruckEmployee");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.OfficeAddress)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneExtensionNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.TruckEmployee)
                    .HasForeignKey<TruckEmployee>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TruckEmployee_TruckPerson");
            });

            modelBuilder.Entity<TruckFeature>(entity =>
            {
                entity.HasKey(e => e.FeatureId);

                entity.ToTable("TruckFeature");

                entity.Property(e => e.FeatureId).HasColumnName("FeatureID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TruckFeatureAssociation>(entity =>
            {
                entity.HasKey(e => new { e.TruckId, e.FeatureId });

                entity.ToTable("Truck_Feature_Association");

                entity.Property(e => e.TruckId).HasColumnName("TruckID");

                entity.Property(e => e.FeatureId).HasColumnName("FeatureID");

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.TruckFeatureAssociations)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Truck_Feature_Association_TruckFeature");

                entity.HasOne(d => d.Truck)
                    .WithMany(p => p.TruckFeatureAssociations)
                    .HasForeignKey(d => d.TruckId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Truck_Feature_Association_IndividualTruck");
            });

            modelBuilder.Entity<TruckModel>(entity =>
            {
                entity.HasKey(e => e.ModelId);

                entity.ToTable("TruckModel");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TruckPerson>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.ToTable("TruckPerson");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TruckRental>(entity =>
            {
                entity.HasKey(e => e.RentalId);

                entity.ToTable("TruckRental");

                entity.Property(e => e.RentalId).HasColumnName("RentalID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.RentDate).HasColumnType("date");

                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.Property(e => e.ReturnDueDate).HasColumnType("date");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.Property(e => e.TruckId).HasColumnName("TruckID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TruckRentals)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TruckRental_TruckCustomer");

                entity.HasOne(d => d.Truck)
                    .WithMany(p => p.TruckRentals)
                    .HasForeignKey(d => d.TruckId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TruckRental_IndividualTruck");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
