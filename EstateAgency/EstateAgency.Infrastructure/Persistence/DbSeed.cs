using EstateAgency.Domain.Data;

namespace EstateAgency.Infrastructure.Persistence;

/// <summary>
/// Provides extension methods for seeding the database with initial data.
/// </summary>
public static class DbSeed
{
    /// <summary>
    /// Seeds the database with default data if it is empty.
    /// </summary>
    /// <param name="context">The database context to seed.</param>
    public static void Seed(this AppDbContext context)
    {
        if (context.Counterparties.Any() || context.RealEstates.Any() || context.Applications.Any()) return;
        var fixture = new FixtureDataClass();
        context.Counterparties.AddRange(fixture.Counterparties);
        context.RealEstates.AddRange(fixture.RealEstates);
        context.Applications.AddRange(fixture.Applications);
        context.SaveChanges();
    }
}
