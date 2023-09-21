using DAL.Base;
using Microsoft.EntityFrameworkCore;

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
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCAE0C9CB88");

            entity.Property(e => e.CommentId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.TaskId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Task).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TempSales_SalesReason");
        });

        modelBuilder.Entity<DbModels.Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABEF00817AE03");

            entity.HasIndex(e => e.ProjectName, "UQ__Projects__BCBE781C0510D072").IsUnique();

            entity.Property(e => e.ProjectId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DbModels.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B1E3437741");

            entity.Property(e => e.TaskId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CancelDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ProjectId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TaskName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProjectId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
