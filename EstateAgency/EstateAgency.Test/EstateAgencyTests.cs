using EstateAgency.Domain.Data;
using EstateAgency.Domain.Enums;

namespace EstateAgency.Test;

/// <summary>
/// Contains unit tests that validate business logic queries for the EstateAgency project.
/// Uses FixtureDataClass as an in-memory data source.
/// </summary>
public class EstateAgencyTests(DataSeeder testData) : IClassFixture<DataSeeder>
{
    /// <summary>
    /// Test: Get all sellers who submitted applications within a date range.
    /// </summary>
    [Fact]
    public void GetSellersByPeriod()
    {
        var start = new DateTime(2024, 06, 01);
        var end = new DateTime(2024, 08, 31);
        var expected = new[] { "Anna Smirnova", "Dmitry Kozlov", "Olga Nikitina" };

        var sellers = testData.Applications
            .Where(a => a.Type == ApplicationType.Sale && a.Date >= start && a.Date <= end)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .Join(testData.Counterparties, id => id, c => c.Id, (id, c) => c.FullName)
            .Order()
            .ToList();
        
        Assert.Equal(expected, sellers);
    }

    /// <summary>
    /// Test: Top 5 clients by number of applications (Purchase or Sale separately)
    /// </summary>
    [Fact]
    public void Top5Clients()
    {
        var expectedBuyers = new[] 
        {
            new { Name = "Sergey Ivanov", Count = 1 },
            new { Name = "Maria Volkova", Count = 1 },
            new { Name = "Pavel Sidorov", Count = 1 },
            new { Name = "Elena Popova", Count = 1 },
            new { Name = "Mikhail Lebedev", Count = 1 }
        };

        var expectedSellers = new[] 
        {
            new { Name = "Ivan Petrov", Count = 1 },
            new { Name = "Anna Smirnova", Count = 1 },
            new { Name = "Dmitry Kozlov", Count = 1 },
            new { Name = "Olga Nikitina", Count = 1 },
            new { Name = "Natalia Orlova", Count = 1 }
        };

        var topBuyers = testData.Applications
            .Where(a => a.Type == ApplicationType.Purchase)
            .GroupBy(a => a.CounterpartyId)
            .Select(g => new { CounterpartyId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.CounterpartyId)
            .Take(5)
            .Join(testData.Counterparties, x => x.CounterpartyId, c => c.Id, (x, c) => new { c.FullName, x.Count })
            .ToList();

        var topSellers = testData.Applications
            .Where(a => a.Type == ApplicationType.Sale)
            .GroupBy(a => a.CounterpartyId)
            .Select(g => new { CounterpartyId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.CounterpartyId)
            .Take(5)
            .Join(testData.Counterparties, x => x.CounterpartyId, c => c.Id, (x, c) => new { c.FullName, x.Count })
            .ToList();

        Assert.Equal(expectedBuyers, topBuyers.Select(b => new { Name = b.FullName, b.Count }));
        Assert.Equal(expectedSellers, topSellers.Select(s => new { Name = s.FullName, s.Count }));
    }

    /// <summary>
    /// Test: Number of applications per real estate type
    /// </summary>
    [Fact]
    public void RequestsByEstateType()
    {
        var expectedCounts = new Dictionary<RealEstateType, int>
        {
            { RealEstateType.Apartment, 2 },
            { RealEstateType.House, 1 },
            { RealEstateType.Cottage, 1 },
            { RealEstateType.Office, 2 },
            { RealEstateType.Townhouse, 1 },
            { RealEstateType.Shop, 1 },
            { RealEstateType.Warehouse, 1 },
            { RealEstateType.Garage, 1 }
        };

        var result = testData.Applications
            .Join(testData.RealEstates, a => a.RealEstateId, r => r.Id, (a, r) => r.Type)
            .GroupBy(t => t)
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .OrderBy(x => x.Type)
            .ToList();

        Assert.Equal(expectedCounts, result.ToDictionary(r => r.Type, r => r.Count));
    }

    /// <summary>
    /// Test: Clients with the minimum transaction amount
    /// </summary>
    [Fact]
    public void ClientsWithMinTransaction()
    {
        const decimal expectedMinAmount = 1200000m;
        const string expectedClient = "Elena Popova";
        
        var minAmount = testData.Applications.Min(a => a.TransactionAmount);
        var clients = testData.Applications
            .Where(a => a.TransactionAmount == minAmount)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .Join(testData.Counterparties, id => id, c => c.Id, (id, c) => c.FullName)
            .Order()
            .ToList();

        Assert.Equal(expectedMinAmount, minAmount);
        Assert.Equal([expectedClient], clients);
    }

    /// <summary>
    /// Test: Clients searching for real estate of a specific type, sorted by FullName
    /// </summary>
    [Fact]
    public void ClientsByEstateType()
    {
        var type = RealEstateType.Apartment;
        var expectedClients = new[] { "Ivan Petrov", "Mikhail Lebedev" };

        var clients = testData.Applications
            .Join(testData.RealEstates, a => a.RealEstateId, r => r.Id, (a, r) => new { a, r })
            .Where(x => x.r.Type == type)
            .Select(x => x.a.CounterpartyId)
            .Distinct()
            .Join(testData.Counterparties, id => id, c => c.Id, (id, c) => c.FullName)
            .Order()
            .ToList();

        Assert.Equal(expectedClients, clients);
    }
}
