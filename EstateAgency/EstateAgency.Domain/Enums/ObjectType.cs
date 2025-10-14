namespace EstateAgency.Domain.Enums;

/// <summary>
/// Defines the detailed classification of real estate objects 
/// according to their structural type and functional characteristics.
/// </summary>
public enum ObjectType
{
    /// <summary>
    /// Apartment within a residential building or complex.
    /// </summary>
    Apartment,

    /// <summary>
    /// Detached single-family residential house.
    /// </summary>
    House,

    /// <summary>
    /// Small rural or seasonal property designed for rest or vacation.
    /// </summary>
    Cottage,

    /// <summary>
    /// Multi-level residential unit sharing walls with adjacent homes.
    /// </summary>
    Townhouse,

    /// <summary>
    /// Commercial office space used for administrative or business purposes.
    /// </summary>
    Office,

    /// <summary>
    /// Retail space designed for trading or providing consumer services.
    /// </summary>
    Shop,

    /// <summary>
    /// Warehouse or industrial storage building for goods and materials.
    /// </summary>
    Warehouse,

    /// <summary>
    /// Parking or private garage for vehicles.
    /// </summary>
    Garage
}
