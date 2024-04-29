using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.Db;

public partial class Entity : DbContext
{
    public Entity()
    {
    }

    public Entity(DbContextOptions<Entity> options)
        : base(options)
    {
    }

    public virtual DbSet<Departament> Departaments { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<ProductionOperation> ProductionOperations { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SecurityReport> SecurityReports { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Transportation> Transportations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MyCoursDB;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Departaments_pkey");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Workers).HasColumnName("workers");

            entity.HasOne(d => d.StaffNavigation).WithMany(p => p.Departaments)
                .HasForeignKey(d => d.Staff)
                .HasConstraintName("Staff");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Equipment_pkey");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.StaffNavigation).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.Staff)
                .HasConstraintName("Staff");
        });

        modelBuilder.Entity<ProductionOperation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductionOperations_pkey");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.OperationEnd).HasColumnName("operationEnd");
            entity.Property(e => e.OperationStart).HasColumnName("operationStart");
            entity.Property(e => e.UsedEquipment).HasColumnName("usedEquipment");

            entity.HasOne(d => d.ProjectsNavigation).WithMany(p => p.ProductionOperations)
                .HasForeignKey(d => d.Projects)
                .HasConstraintName("Projects");

            entity.HasOne(d => d.StaffNavigation).WithMany(p => p.ProductionOperations)
                .HasForeignKey(d => d.Staff)
                .HasConstraintName("Staff");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Projects_pkey");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_pkey");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SecurityReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SecurityReports_pkey");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.TheDegreeOfThreat).HasColumnName("theDegreeOfThreat");

            entity.HasOne(d => d.StaffNavigation).WithMany(p => p.SecurityReports)
                .HasForeignKey(d => d.Staff)
                .HasConstraintName("Staff");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Staff_pkey");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");

            entity.HasOne(d => d.RolesNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Roles)
                .HasConstraintName("Roles");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Users)
                .HasConstraintName("Users");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Transactions_pkey");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AmountOfMoneyPaidReceived).HasColumnName("amountOfMoneyPaidReceived");
            entity.Property(e => e.DateOfTransaction).HasColumnName("dateOfTransaction");
            entity.Property(e => e.Purchase)
                .HasMaxLength(300)
                .HasColumnName("purchase");

            entity.HasOne(d => d.StaffNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.Staff)
                .HasConstraintName("Staff");
        });

        modelBuilder.Entity<Transportation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Transportation_pkey");

            entity.ToTable("Transportation");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DateOfTransportation).HasColumnName("dateOfTransportation");
            entity.Property(e => e.Products)
                .HasMaxLength(300)
                .HasColumnName("products");

            entity.HasOne(d => d.StaffNavigation).WithMany(p => p.Transportations)
                .HasForeignKey(d => d.Staff)
                .HasConstraintName("Staff");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
