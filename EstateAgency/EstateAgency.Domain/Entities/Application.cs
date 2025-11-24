using EstateAgency.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstateAgency.Domain.Entities;

/// <summary>
/// Represents a real estate transaction application in the agency system.
/// Contains information about the property, counterparty, transaction details, and application metadata.
/// </summary>
[Table("applications")]
public class Application
{
    /// <summary>
    /// Unique identifier for the application.
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Foreign key reference to the counterparty associated with this application.
    /// </summary>
    [Column("counterparty_id")]
    public required int CounterpartyId { get; set; }

    /// <summary>
    /// Navigation property to the counterparty (client) details.
    /// </summary>
    public Counterparty? Counterparty { get; set; }

    /// <summary>
    /// Foreign key reference to the real estate object involved in the transaction.
    /// </summary>
    [Column("real_estate_id")]
    public required int RealEstateId { get; set; }

    /// <summary>
    /// Navigation property to the real estate object details.
    /// </summary>
    public RealEstate? RealEstate { get; set; }

    /// <summary>
    /// Monetary amount of the transaction in the application.
    /// </summary>
    [Column("transaction_amount")]
    public required decimal TransactionAmount { get; set; }

    /// <summary>
    /// Type of the application (Purchase or Sale).
    /// </summary>
    [Column("type")]
    public required ApplicationType Type { get; set; }

    /// <summary>
    /// Date when the application was created or submitted.
    /// </summary>
    [Column("date")]
    public required DateTime Date { get; set; }
}
