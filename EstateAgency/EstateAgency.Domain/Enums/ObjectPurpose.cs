namespace EstateAgency.Domain.Enums;

/// <summary>
/// Intended usage of a real estate object.
/// </summary>
public enum ObjectPurpose
{
    /// <summary>
    /// Property intended for living (apartments, houses).
    /// </summary>
    Residential,

    /// <summary>
    /// Property intended for business and trade.
    /// </summary>
    Commercial,

    /// <summary>
    /// Property intended for industrial or production use.
    /// </summary>
    Industrial,

    /// <summary>
    /// Property used for farming and agricultural purposes.
    /// </summary>
    Agricultural
}
