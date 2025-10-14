namespace EstateAgency.Domain.Entities;

/// <summary>
/// Represents a client or agent (counterparty) in the real estate agency system.
/// Stores personal identification and contact information for transaction participants.
/// </summary>
public class Counterparty
{
    /// <summary>
    /// Unique identifier for the counterparty.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name (Last, First, Middle) of the counterparty.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Passport number or other legal identification document.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Contact phone number of the counterparty.
    /// </summary>
    public required string PhoneNumber { get; set; }
}
