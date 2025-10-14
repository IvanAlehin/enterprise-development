using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Enums;

namespace EstateAgency.Test.Data;

/// <summary>
/// Provides test fixture data for unit tests.
/// Contains in-memory collections of counterparties, real estates, and applications.
/// </summary>
public static class FixtureDataClass
{
    /// <summary>
    /// Returns a list of sample counterparties (clients/agents).
    /// </summary>
    public static List<Counterparty> GetCounterparties() => new()
    {
        new() { Id = 1, FullName = "Ivan Petrov", PassportNumber = "1234 567890", PhoneNumber = "+7-900-111-22-33" },
        new() { Id = 2, FullName = "Anna Smirnova", PassportNumber = "2234 112233", PhoneNumber = "+7-900-222-33-44" },
        new() { Id = 3, FullName = "Sergey Ivanov", PassportNumber = "3344 998877", PhoneNumber = "+7-900-333-44-55" },
        new() { Id = 4, FullName = "Maria Volkova", PassportNumber = "4455 887766", PhoneNumber = "+7-900-444-55-66" },
        new() { Id = 5, FullName = "Dmitry Kozlov", PassportNumber = "5566 776655", PhoneNumber = "+7-900-555-66-77" },
        new() { Id = 6, FullName = "Olga Nikitina", PassportNumber = "6677 665544", PhoneNumber = "+7-900-666-77-88" },
        new() { Id = 7, FullName = "Pavel Sidorov", PassportNumber = "7788 554433", PhoneNumber = "+7-900-777-88-99" },
        new() { Id = 8, FullName = "Elena Popova", PassportNumber = "8899 443322", PhoneNumber = "+7-900-888-99-00" },
        new() { Id = 9, FullName = "Mikhail Lebedev", PassportNumber = "9900 332211", PhoneNumber = "+7-900-999-00-11" },
        new() { Id = 10, FullName = "Natalia Orlova", PassportNumber = "1111 222233", PhoneNumber = "+7-901-123-45-67" },
    };

    /// <summary>
    /// Returns a list of sample real estate objects.
    /// </summary>
    public static List<RealEstate> GetRealEstates() => new()
    {
        new() { Id = 1, Type = ObjectType.Apartment, Purpose = ObjectPurpose.Residential, CadastralNumber = "63:01:0000001", Address = "Moscow, Tverskaya 10", FloorsTotal = 12, TotalArea = 55.5, Rooms = 2, CeilingHeight = 2.7, FloorNumber = 7, HasEncumbrances = false },
        new() { Id = 2, Type = ObjectType.House, Purpose = ObjectPurpose.Residential, CadastralNumber = "63:02:0000002", Address = "Samara, Lenina 23", FloorsTotal = 2, TotalArea = 120.0, Rooms = 5, CeilingHeight = 3.0, FloorNumber = 0, HasEncumbrances = false },
        new() { Id = 3, Type = ObjectType.Cottage, Purpose = ObjectPurpose.Recreational, CadastralNumber = "63:03:0000003", Address = "Sochi, Green Hills 5", FloorsTotal = 1, TotalArea = 80.2, Rooms = 3, CeilingHeight = 2.8, FloorNumber = 0, HasEncumbrances = false },
        new() { Id = 4, Type = ObjectType.Office, Purpose = ObjectPurpose.Commercial, CadastralNumber = "63:04:0000004", Address = "Moscow, Business Center 15", FloorsTotal = 20, TotalArea = 300.0, Rooms = 10, CeilingHeight = 3.5, FloorNumber = 10, HasEncumbrances = true },
        new() { Id = 5, Type = ObjectType.Townhouse, Purpose = ObjectPurpose.Residential, CadastralNumber = "63:05:0000005", Address = "Kazan, Victory Ave 8", FloorsTotal = 3, TotalArea = 140.0, Rooms = 6, CeilingHeight = 3.0, FloorNumber = 0, HasEncumbrances = false },
        new() { Id = 6, Type = ObjectType.Shop, Purpose = ObjectPurpose.Commercial, CadastralNumber = "63:06:0000006", Address = "Samara, Market Street 12", FloorsTotal = 1, TotalArea = 90.0, Rooms = 2, CeilingHeight = 3.2, FloorNumber = 1, HasEncumbrances = false },
        new() { Id = 7, Type = ObjectType.Warehouse, Purpose = ObjectPurpose.Industrial, CadastralNumber = "63:07:0000007", Address = "Samara, Industrial Zone 9", FloorsTotal = 1, TotalArea = 500.0, Rooms = 1, CeilingHeight = 5.0, FloorNumber = 0, HasEncumbrances = true },
        new() { Id = 8, Type = ObjectType.Garage, Purpose = ObjectPurpose.Utility, CadastralNumber = "63:08:0000008", Address = "Samara, Garage Co-op 3", FloorsTotal = 1, TotalArea = 20.0, Rooms = 1, CeilingHeight = 2.5, FloorNumber = 0, HasEncumbrances = false },
        new() { Id = 9, Type = ObjectType.Apartment, Purpose = ObjectPurpose.Residential, CadastralNumber = "63:09:0000009", Address = "Moscow, Arbat 22", FloorsTotal = 16, TotalArea = 75.0, Rooms = 3, CeilingHeight = 2.9, FloorNumber = 9, HasEncumbrances = false },
        new() { Id = 10, Type = ObjectType.Office, Purpose = ObjectPurpose.Commercial, CadastralNumber = "63:10:0000010", Address = "Saint Petersburg, Nevsky 100", FloorsTotal = 8, TotalArea = 220.0, Rooms = 8, CeilingHeight = 3.4, FloorNumber = 5, HasEncumbrances = true },
    };

    /// <summary>
    /// Returns a list of sample applications (requests for purchase/sale).
    /// </summary>
    public static List<Application> GetApplications() => new()
    {
        new() { Id = 1, CounterpartyId = 1, RealEstateId = 1, TransactionAmount = 5500000m, Type = ApplicationType.Sale, Date = new DateTime(2024, 05, 10) },
        new() { Id = 2, CounterpartyId = 2, RealEstateId = 2, TransactionAmount = 12500000m, Type = ApplicationType.Sale, Date = new DateTime(2024, 06, 01) },
        new() { Id = 3, CounterpartyId = 3, RealEstateId = 3, TransactionAmount = 7000000m, Type = ApplicationType.Purchase, Date = new DateTime(2024, 06, 15) },
        new() { Id = 4, CounterpartyId = 4, RealEstateId = 4, TransactionAmount = 30500000m, Type = ApplicationType.Purchase, Date = new DateTime(2024, 07, 02) },
        new() { Id = 5, CounterpartyId = 5, RealEstateId = 5, TransactionAmount = 9500000m, Type = ApplicationType.Sale, Date = new DateTime(2024, 07, 12) },
        new() { Id = 6, CounterpartyId = 6, RealEstateId = 6, TransactionAmount = 8200000m, Type = ApplicationType.Sale, Date = new DateTime(2024, 08, 05) },
        new() { Id = 7, CounterpartyId = 7, RealEstateId = 7, TransactionAmount = 15500000m, Type = ApplicationType.Purchase, Date = new DateTime(2024, 08, 20) },
        new() { Id = 8, CounterpartyId = 8, RealEstateId = 8, TransactionAmount = 1200000m, Type = ApplicationType.Sale, Date = new DateTime(2024, 09, 10) },
        new() { Id = 9, CounterpartyId = 9, RealEstateId = 9, TransactionAmount = 7800000m, Type = ApplicationType.Purchase, Date = new DateTime(2024, 09, 25) },
        new() { Id = 10, CounterpartyId = 10, RealEstateId = 10, TransactionAmount = 21000000m, Type = ApplicationType.Sale, Date = new DateTime(2024, 10, 05) },
    };
}
