using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DEPI_DbFirst_EfCore.Models;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentLocation> DepartmentLocations { get; set; }

    public virtual DbSet<Dependent> Dependents { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Manage> Manages { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<WorkIn> WorkIns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-O3P5F2Q\\SQLEXPRESS;Database=Company;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Dnum).HasName("PK__Departme__7A77508594F8B346");

            entity.ToTable("Department");

            entity.HasIndex(e => e.Dname, "UQ__Departme__83BFD84957CCC349").IsUnique();

            entity.Property(e => e.Dnum).ValueGeneratedNever();
            entity.Property(e => e.Dname)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DepartmentLocation>(entity =>
        {
            entity.HasKey(e => new { e.Dnum, e.Location }).HasName("PK__Departme__74228334C2F8E5CB");

            entity.ToTable("Department_Locations");

            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.DepartmentLocations)
                .HasForeignKey(d => d.Dnum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Department__Dnum__3F466844");
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.HasKey(e => new { e.Ssn, e.DependentName }).HasName("PK__Dependen__9335CD1E17CFDDD3");

            entity.ToTable("Dependent");

            entity.Property(e => e.Ssn).HasColumnName("SSN");
            entity.Property(e => e.DependentName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Dependent_Name");
            entity.Property(e => e.BirthDate).HasColumnName("Birth_Date");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.SsnNavigation).WithMany(p => p.Dependents)
                .HasForeignKey(d => d.Ssn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dependent__SSN__4AB81AF0");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Ssn).HasName("PK__Employee__CA1E8E3DF1DB4B0B");

            entity.ToTable("Employee");

            entity.Property(e => e.Ssn)
                .ValueGeneratedNever()
                .HasColumnName("SSN");
            entity.Property(e => e.BirthDate).HasColumnName("Birth_Date");
            entity.Property(e => e.FName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("F_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("L_name");
            entity.Property(e => e.SupervisorSsn).HasColumnName("SupervisorSSN");

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Dnum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Dnum__3B75D760");

            entity.HasOne(d => d.SupervisorSsnNavigation).WithMany(p => p.InverseSupervisorSsnNavigation)
                .HasForeignKey(d => d.SupervisorSsn)
                .HasConstraintName("FK__Employee__Superv__3C69FB99");
        });

        modelBuilder.Entity<Manage>(entity =>
        {
            entity.HasKey(e => e.Dnum).HasName("PK__Manage__7A775085B61FF137");

            entity.ToTable("Manage");

            entity.Property(e => e.Dnum).ValueGeneratedNever();
            entity.Property(e => e.HireDate).HasColumnName("Hire_Date");
            entity.Property(e => e.Ssn).HasColumnName("SSN");

            entity.HasOne(d => d.DnumNavigation).WithOne(p => p.Manage)
                .HasForeignKey<Manage>(d => d.Dnum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Manage__Dnum__59063A47");

            entity.HasOne(d => d.SsnNavigation).WithMany(p => p.Manages)
                .HasForeignKey(d => d.Ssn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Manage__SSN__59FA5E80");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Pnumber).HasName("PK__Project__DDE0878DA3CF28F0");

            entity.ToTable("Project");

            entity.Property(e => e.Pnumber)
                .ValueGeneratedNever()
                .HasColumnName("PNumber");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pname)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Dnum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Project__Dnum__4222D4EF");
        });

        modelBuilder.Entity<WorkIn>(entity =>
        {
            entity.HasKey(e => new { e.Ssn, e.Pnumber }).HasName("PK__WORK_IN__07C086457CA65356");

            entity.ToTable("WORK_IN");

            entity.Property(e => e.Ssn).HasColumnName("SSN");
            entity.Property(e => e.Pnumber).HasColumnName("PNumber");
            entity.Property(e => e.Hours).HasColumnType("decimal(4, 1)");

            entity.HasOne(d => d.PnumberNavigation).WithMany(p => p.WorkIns)
                .HasForeignKey(d => d.Pnumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WORK_IN__PNumber__46E78A0C");

            entity.HasOne(d => d.SsnNavigation).WithMany(p => p.WorkIns)
                .HasForeignKey(d => d.Ssn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WORK_IN__SSN__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
