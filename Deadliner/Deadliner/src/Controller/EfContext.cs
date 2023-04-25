using System.Globalization;
using Deadliner.Api.Models;
using Deadliner.Models;
using Deadliner.Storage.EF.DataProviders;
using Deadliner.Utils;
using Microsoft.EntityFrameworkCore;


namespace Deadliner.Controller;

public class EfContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<DbCalendar> Calendars => Set<DbCalendar>();
    public EfContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<DbCalendar>()
            .HasOne<User>();
        modelBuilder
            .Entity<DbCalendar>()
            .HasMany<LocalEvent>()
            .WithMany();
        modelBuilder
            .Entity<DbCalendar>()
            .HasMany<LocalTask>()
            .WithMany();
        modelBuilder
            .Entity<DbCalendar>()
            .Property(e => e.LocalEvents)
            .HasConversion(
                g => g,
                e => e
            );
        modelBuilder
            .Entity<LocalEvent>()
            .ToTable("LocalEvents");
        modelBuilder
            .Entity<LocalTask>()
            .ToTable("LocalTasks");
        modelBuilder
            .Entity<LocalEvent>()
            .Property(e => e.Group)
            .HasConversion(
                g => g,
                e => e
            );
        modelBuilder
            .Entity<LocalEvent>()
            .Property(e => e.Id)
            .HasConversion(
                g => g,
                e => e
            );
        modelBuilder
            .Entity<LocalEvent>()
            .Property(e => e.State)
            .HasConversion(
                g => StateIdTransformer.GetStateId(g),
                e => StateIdTransformer.GetState(e)
            );
        modelBuilder
            .Entity<LocalTask>()
            .Property(e => e.Id)
            .HasConversion(
                g => g,
                e => e
            );
        modelBuilder
            .Entity<LocalTask>()
            .Property(e => e.Group)
            .HasConversion(
                g => g,
                e => e
            );
        modelBuilder
            .Entity<LocalTask>()
            .Property(e => e.State)
            .HasConversion(
                g => StateIdTransformer.GetStateId(g),
                e => StateIdTransformer.GetState(e)
            );
        modelBuilder
            .Entity<LocalTask>()
            .Property(e => e.CreationDateTime)
            .HasConversion(
                g => g.ToString(CultureInfo.InvariantCulture),
                e => DateTime.Parse(e)
            );
        modelBuilder
            .Entity<LocalTask>()
            .Property(e => e.Parent)
            .HasConversion(
                g => g,
                e => e
            );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Configuration.ConnectionString();

        optionsBuilder.UseSqlServer(connectionString);
    }
}