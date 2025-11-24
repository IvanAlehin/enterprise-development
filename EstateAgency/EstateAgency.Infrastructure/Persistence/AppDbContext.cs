using EstateAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstateAgency.Infrastructure.Persistence;

/// <summary>
/// EF Core database context for applications, counterparties, and real estate entities.
/// </summary>
/// <param name="options">Database context options.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Applications table.
    /// </summary>
    public DbSet<Application> Applications { get; set; }

    /// <summary>
    /// Counterparties table.
    /// </summary>
    public DbSet<Counterparty> Counterparties { get; set; }

    /// <summary>
    /// Real estate table.
    /// </summary>
    public DbSet<RealEstate> RealEstates { get; set; }

    /// <summary>
    /// Configures entity mappings and relationships.
    /// </summary>
    /// <param name="modelBuilder">Model builder instance.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.HasOne(a => a.Counterparty)
                  .WithMany()
                  .HasForeignKey(a => a.CounterpartyId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(a => a.RealEstate)
                  .WithMany()
                  .HasForeignKey(a => a.RealEstateId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.Property(a => a.Type)
                  .HasConversion<string>()
                  .HasMaxLength(50)
                  .IsRequired();
        });

        modelBuilder.Entity<Counterparty>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<RealEstate>()
            .HasKey(r => r.Id);
    }
}