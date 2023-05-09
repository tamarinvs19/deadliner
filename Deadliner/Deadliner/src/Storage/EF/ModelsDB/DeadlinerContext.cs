using Microsoft.EntityFrameworkCore;

namespace Deadliner.Storage.EF.ModelsDB;

public partial class DeadlinerContext : DbContext
{
    public DeadlinerContext()
    {
    }

    public DeadlinerContext(DbContextOptions<DeadlinerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<LocalAction> LocalActions { get; set; }

    public virtual DbSet<LocalEvent> LocalEvents { get; set; }

    public virtual DbSet<LocalTask> LocalTasks { get; set; }

    public virtual DbSet<SuperGroup> SuperGroups { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserToGroup> UserToGroups { get; set; }

    public virtual DbSet<UserToLocalAction> UserToLocalActions { get; set; }

    public virtual DbSet<UserToSuperGroup> UserToSuperGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer("Server=THINKBOOK;Database=DEADLINER;Trusted_Connection=True;TrustServerCertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccessKey).HasColumnType("text");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title).HasColumnType("text");

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Groups_Users");

            entity.HasOne(d => d.SuperGroupNavigation).WithMany(p => p.Groups)
                .HasForeignKey(d => d.SuperGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Groups_SuperGroups");
        });

        modelBuilder.Entity<LocalAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LocalEvents");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Dgroup).HasColumnName("DGroup");
            entity.Property(e => e.Title).HasColumnType("text");

            entity.HasOne(d => d.DgroupNavigation).WithMany(p => p.LocalActions)
                .HasForeignKey(d => d.Dgroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocalActions_Groups");

            entity.HasOne(d => d.ParentNavigation).WithMany(p => p.InverseParentNavigation)
                .HasForeignKey(d => d.Parent)
                .HasConstraintName("FK_LocalEvents_LocalEvents");
        });

        modelBuilder.Entity<LocalEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LocalEvents_1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateTime).HasColumnType("datetime");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.LocalEvent)
                .HasForeignKey<LocalEvent>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocalEvents_LocalActions");
        });

        modelBuilder.Entity<LocalTask>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDateTime).HasColumnType("datetime");
            entity.Property(e => e.Deadline).HasColumnType("datetime");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.LocalTask)
                .HasForeignKey<LocalTask>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocalTasks_LocalActions");
        });

        modelBuilder.Entity<SuperGroup>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccessKey).HasColumnType("text");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title).HasColumnType("text");

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.SuperGroups)
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuperGroups_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Password).HasColumnType("text");
            entity.Property(e => e.Username).HasColumnType("text");
        });

        modelBuilder.Entity<UserToGroup>(entity =>
        {
            entity.ToTable("UserToGroup");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Group).WithMany(p => p.UserToGroups)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserToGroup_Groups");

            entity.HasOne(d => d.User).WithMany(p => p.UserToGroups)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserToGroup_Users");
        });

        modelBuilder.Entity<UserToLocalAction>(entity =>
        {
            entity.ToTable("UserToLocalAction");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.LocalAction).WithMany(p => p.UserToLocalActions)
                .HasForeignKey(d => d.LocalActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserToLocalAction_LocalActions");

            entity.HasOne(d => d.User).WithMany(p => p.UserToLocalActions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserToLocalAction_Users");
        });

        modelBuilder.Entity<UserToSuperGroup>(entity =>
        {
            entity.ToTable("UserToSuperGroup");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.SuperGroup).WithMany(p => p.UserToSuperGroups)
                .HasForeignKey(d => d.SuperGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserToSuperGroup_SuperGroups");

            entity.HasOne(d => d.User).WithMany(p => p.UserToSuperGroups)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserToSuperGroup_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
