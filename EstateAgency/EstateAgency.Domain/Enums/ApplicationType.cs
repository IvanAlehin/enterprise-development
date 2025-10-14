namespace EstateAgency.Domain.Enums;

/// <summary>
/// Type of transaction application (request) in the agency system.
/// </summary>
public enum ApplicationType
{
    /// <summary>
    /// Client wants to buy the property (purchase request).
    /// </summary>
    Purchase,

    /// <summary>
    /// Client wants to sell the property (sale request).
    /// </summary>
    Sale
}
