using DAL.Base;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.EFAccess;

public partial class TaskManagerDbContext : DbContext
{
    private string connectionString;
    private readonly IDbStringConnection dbConnection;
    public TaskManagerDbContext(IDbStringConnection dbConnection)
    {
        this.dbConnection = dbConnection;
        this.connectionString = dbConnection.ConnectionString;
    }

    public virtual DbSet<DbModels.Comment> Comments { get; set; }

    public virtual DbSet<DbModels.Project> Projects { get; set; }

    public virtual DbSet<DbModels.Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => dbConnection.SetOptions(optionsBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbModels.Comment>(entity =>
        {
           

            entity.Property(e => e.CommentId)
                .HasConversion(
                    v => v.ToString(),
                    v => Guid.Parse(v)
                );
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.TaskId)
                .HasConversion(
                    v => v.ToString(),
                    v => Guid.Parse(v));                
            entity.HasOne(d => d.Task).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DbModels.Project>(entity =>
        {
            
            entity.Property(e => e.ProjectId)
                .HasConversion(
                    v => v.ToString(),
                    v => Guid.Parse(v));
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DbModels.Task>(entity =>
        {
           

            entity.Property(e => e.TaskId)
                .HasConversion(
                    v => v.ToString(),
                    v => Guid.Parse(v));
            entity.Property(e => e.CancelDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ProjectId)
                .HasConversion(
                    v => v.ToString(),
                    v => Guid.Parse(v));
              
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TaskName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
