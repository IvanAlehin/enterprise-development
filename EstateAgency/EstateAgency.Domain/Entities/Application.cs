using EstateAgency.Domain.Enums;

namespace EstateAgency.Domain.Entities;

/// <summary>
/// Represents a real estate transaction application in the agency system.
/// Contains information about the property, counterparty, transaction details, and application metadata.
/// </summary>
public class Application
{
    /// <summary>
    /// Unique identifier for the application.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key reference to the counterparty associated with this application.
    /// </summary>
    public required int CounterpartyId { get; set; }

    /// <summary>
    /// Navigation property to the counterparty (client) details.
    /// </summary>
    public Counterparty? Counterparty { get; set; }

    /// <summary>
    /// Foreign key reference to the real estate object involved in the transaction.
    /// </summary>
    public required int RealEstateId { get; set; }

    /// <summary>
    /// Navigation property to the real estate object details.
    /// </summary>
    public RealEstate? RealEstate { get; set; }

    /// <summary>
    /// Monetary amount of the transaction in the application.
    /// </summary>
    public required decimal TransactionAmount { get; set; }

    /// <summary>
    /// Type of the application (Purchase or Sale).
    /// </summary>
    public required ApplicationType Type { get; set; }

    /// <summary>
    /// Date when the application was created or submitted.
    /// </summary>
    public required DateTime Date { get; set; }
}
