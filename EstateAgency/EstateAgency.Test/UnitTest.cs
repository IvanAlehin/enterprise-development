using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Enums;
using EstateAgency.Test.Data;
using System.Linq;
using Xunit;

namespace EstateAgency.Test;

/// <summary>
/// Unit tests for the EstateAgency project.
/// Performs queries on in-memory collections provided by FixtureDataClass.
/// </summary>
public class UnitTest
{
    private readonly List<Counterparty> _counterparties;
    private readonly List<RealEstate> _realEstates;
    private readonly List<Application> _applications;

    public UnitTest()
    {
        _counterparties = FixtureDataClass.GetCounterparties();
        _realEstates = FixtureDataClass.GetRealEstates();
        _applications = FixtureDataClass.GetApplications();
    }

    /// <summary>
    /// Test: Get all sellers who submitted applications within a date range.
    /// </summary>
    [Fact]
    public void Sellers_ByDateRange_ShouldReturnCorrect()
    {
        var start = new DateTime(2024, 06, 01);
        var end = new DateTime(2024, 08, 31);

        var sellers = _applications
            .Where(a => a.Type == ApplicationType.Sale && a.Date >= start && a.Date <= end)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .Join(_counterparties, id => id, c => c.Id, (id, c) => c.FullName)
            .ToList();

        Assert.NotEmpty(sellers);
        Assert.Contains("Anna Smirnova", sellers);
    }

    /// <summary>
    /// Test: Top 5 clients by number of applications (Purchase or Sale separately)
    /// </summary>
    [Fact]
    public void Top5Clients_ByApplications_ShouldReturnCorrect()
    {
        var topBuyers = _applications
            .Where(a => a.Type == ApplicationType.Purchase)
            .GroupBy(a => a.CounterpartyId)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Join(_counterparties, g => g.Key, c => c.Id, (g, c) => new { c.FullName, Count = g.Count() })
            .ToList();

        Assert.NotEmpty(topBuyers);
        Assert.True(topBuyers.All(x => x.Count > 0));

        var topSellers = _applications
            .Where(a => a.Type == ApplicationType.Sale)
            .GroupBy(a => a.CounterpartyId)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Join(_counterparties, g => g.Key, c => c.Id, (g, c) => new { c.FullName, Count = g.Count() })
            .ToList();

        Assert.NotEmpty(topSellers);
        Assert.True(topSellers.All(x => x.Count > 0));
    }

    /// <summary>
    /// Test: Number of applications per real estate type
    /// </summary>
    [Fact]
    public void Applications_ByRealEstateType_ShouldReturnCorrect()
    {
        var result = _applications
            .Join(_realEstates, a => a.RealEstateId, r => r.Id, (a, r) => r.Type)
            .GroupBy(t => t)
            .Select(g => new { Type = g.Key, Count = g.Count() })
            .ToList();

        Assert.NotEmpty(result);
    }

    /// <summary>
    /// Test: Clients with the minimum transaction amount
    /// </summary>
    [Fact]
    public void Clients_WithMinimumTransaction_ShouldReturnCorrect()
    {
        var minAmount = _applications.Min(a => a.TransactionAmount);

        var clients = _applications
            .Where(a => a.TransactionAmount == minAmount)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .Join(_counterparties, id => id, c => c.Id, (id, c) => c.FullName)
            .ToList();

        Assert.NotEmpty(clients);
    }

    /// <summary>
    /// Test: Clients searching for real estate of a specific type, sorted by FullName
    /// </summary>
    [Fact]
    public void Clients_ByRealEstateType_ShouldReturnCorrect()
    {
        var type = RealEstateType.Apartment;

        var clients = _applications
            .Join(_realEstates, a => a.RealEstateId, r => r.Id, (a, r) => new { a, r })
            .Where(x => x.r.Type == type)
            .Select(x => x.a.CounterpartyId)
            .Distinct()
            .Join(_counterparties, id => id, c => c.Id, (id, c) => c.FullName)
            .OrderBy(n => n)
            .ToList();

        Assert.NotEmpty(clients);
    }
}
