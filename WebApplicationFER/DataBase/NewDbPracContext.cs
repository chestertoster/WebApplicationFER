using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationFER.DataBase;

public partial class NewDbPracContext : DbContext
{
    public NewDbPracContext()
    {
    }

    public NewDbPracContext(DbContextOptions<NewDbPracContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityDirection> ActivityDirections { get; set; }

    public virtual DbSet<EnergyConsumption> EnergyConsumptions { get; set; }

    public virtual DbSet<EnergyType> EnergyTypes { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("host=localhost; password=1234; username=postgres; port=5432; database=new_db_prac");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityDirection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("activity_directions_pkey");

            entity.ToTable("activity_directions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<EnergyConsumption>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("energy_consumption");

            entity.Property(e => e.ActivityDirectionId).HasColumnName("activity_direction_id");
            entity.Property(e => e.EnergyTypeId).HasColumnName("energy_type_id");
            entity.Property(e => e.EquipmentCode)
                .HasMaxLength(50)
                .HasColumnName("equipment_code");
            entity.Property(e => e.EquipmentName)
                .HasMaxLength(50)
                .HasColumnName("equipment_name");
            entity.Property(e => e.ReadingDate).HasColumnName("reading_date");
            entity.Property(e => e.Volume).HasColumnName("volume");

            entity.HasOne(d => d.ActivityDirection).WithMany()
                .HasForeignKey(d => d.ActivityDirectionId)
                .HasConstraintName("energy_consumption_activity_direction_id_fkey");

            entity.HasOne(d => d.EnergyType).WithMany()
                .HasForeignKey(d => d.EnergyTypeId)
                .HasConstraintName("energy_consumption_energy_type_id_fkey");

            entity.HasOne(d => d.EquipmentCodeNavigation).WithMany()
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.EquipmentCode)
                .HasConstraintName("energy_consumption_equipment_code_fkey");

            entity.HasOne(d => d.Equipment).WithMany()
                .HasPrincipalKey(p => new { p.Code, p.Name })
                .HasForeignKey(d => new { d.EquipmentCode, d.EquipmentName })
                .HasConstraintName("energy_consumption_equipment_fk");
        });

        modelBuilder.Entity<EnergyType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("energy_types_pkey");

            entity.ToTable("energy_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("equipment_pkey");

            entity.ToTable("equipment");

            entity.HasIndex(e => e.Code, "equipment_code_key").IsUnique();

            entity.HasIndex(e => new { e.Code, e.Name }, "equipment_code_name_uniq").IsUnique();

            entity.HasIndex(e => e.Code, "equipment_code_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("equipment_type_id_fkey");
        });

        modelBuilder.Entity<EquipmentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("equipment_types_pkey");

            entity.ToTable("equipment_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
