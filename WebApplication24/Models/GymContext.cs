using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApplication24.Models
{
    public partial class GymContext : DbContext
    {
        public GymContext()
        {
        }

        public GymContext(DbContextOptions<GymContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AboutUs> AboutUs { get; set; }
        public virtual DbSet<Attendance1> Attendance1 { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmplyeeJob> EmplyeeJob { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<LoginTypes> LoginTypes { get; set; }
        public virtual DbSet<MemberCard> MemberCard { get; set; }
        public virtual DbSet<MemberShip> MemberShip { get; set; }
        public virtual DbSet<Offers> Offers { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Salary> Salary { get; set; }
        public virtual DbSet<Task> Task { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-0MNBIDO\\SQLEXPRESS;Database=Gym;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AboutUs>(entity =>
            {
                entity.HasKey(e => e.Phone);

                entity.ToTable("AboutUS");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(300);

                entity.Property(e => e.OurPartnars)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.OurStory)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Attendance1>(entity =>
            {
                entity.HasKey(e => e.AttendanceId);

                entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");

                entity.Property(e => e.CheckIn).HasColumnType("datetime");

                entity.Property(e => e.CheckOut).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Attendance1)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attendance1_Employee");
            });

            modelBuilder.Entity<ContactUs>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("Contact us");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.BirthDay).HasColumnType("datetime");

                entity.Property(e => e.CustimerImg)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("LName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_LoginTypes");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Evaluation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_LoginTypes");
            });

            modelBuilder.Entity<EmplyeeJob>(entity =>
            {
                entity.Property(e => e.EmplyeeJobId).HasColumnName("EmplyeeJobID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmplyeeJob)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmplyeeJob_Employee");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.EmplyeeJob)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmplyeeJob_Task");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmploeeId).HasColumnName("EmploeeID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Login_Customer");

                entity.HasOne(d => d.Emploee)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.EmploeeId)
                    .HasConstraintName("FK_Login_Employee");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Login_LoginTypes");
            });

            modelBuilder.Entity<LoginTypes>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.LoginType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MemberCard>(entity =>
            {
                entity.HasKey(e => e.CardId);

                entity.Property(e => e.CardId).HasColumnName("CardID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.MemberShipId).HasColumnName("MemberShipID");

                entity.Property(e => e.TimeEnd).HasColumnType("date");

                entity.Property(e => e.TimeStart).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.MemberCard)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberCard_Customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.MemberCard)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberCard_Employee");

                entity.HasOne(d => d.MemberShip)
                    .WithMany(p => p.MemberCard)
                    .HasForeignKey(d => d.MemberShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberCard_MemberShip");
            });

            modelBuilder.Entity<MemberShip>(entity =>
            {
                entity.Property(e => e.MemberShipId).HasColumnName("MemberShipID");

                entity.Property(e => e.MemberShipName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OfferId).HasColumnName("OfferID");

                entity.Property(e => e.TypeOfEvnet)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.MemberShip)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberShip_Offers");
            });

            modelBuilder.Entity<Offers>(entity =>
            {
                entity.HasKey(e => e.OfferId);

                entity.Property(e => e.OfferId).HasColumnName("OfferID");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.CardId).HasColumnName("CardID");

                entity.Property(e => e.CutsomerId).HasColumnName("CutsomerID");

                entity.Property(e => e.DateOfPayment).HasColumnType("date");

                entity.Property(e => e.MemberShipId).HasColumnName("MemberShipID");

                entity.Property(e => e.TypeOfPayment)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK_Payment_MemberCard");

                entity.HasOne(d => d.Cutsomer)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CutsomerId)
                    .HasConstraintName("FK_Payment_Customer");

                entity.HasOne(d => d.MemberShip)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.MemberShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_MemberShip");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.Property(e => e.SalaryId).HasColumnName("SalaryID");

                entity.Property(e => e.CutOfsalary).HasColumnName("CutOFSalary");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.MonthOfSalary).HasColumnType("date");

                entity.Property(e => e.Salary1).HasColumnName("Salary");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Salary)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salary_Employee");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.NameOfTask)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
