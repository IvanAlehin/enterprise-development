using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstateAgency.Domain.Entities;

/// <summary>
/// Represents a client or agent (counterparty) in the real estate agency system.
/// Stores personal identification and contact information for transaction participants.
/// </summary>
[Table("counterparties")]
public class Counterparty
{
    /// <summary>
    /// Unique identifier for the counterparty.
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Full name (Last, First, Middle) of the counterparty.
    /// </summary>
    [Column("full_name")]
    [MaxLength(50)]
    public required string FullName { get; set; }

    /// <summary>
    /// Passport number or other legal identification document.
    /// </summary>
    [Column("passport_number")]
    [MaxLength(20)]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Contact phone number of the counterparty.
    /// </summary>
    [Column("phone_number")]
    [MaxLength(20)]
    public required string PhoneNumber { get; set; }
}
