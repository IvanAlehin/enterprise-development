using EstateAgency.Domain.Enums;

namespace EstateAgency.Domain.Entities;

/// <summary>
/// Represents a real estate object (property) in the agency system.
/// Contains physical, legal and address information.
/// </summary>
public class RealEstate
{
    /// <summary>
    /// Primary key — integer identifier for the object.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Type of the real estate (apartment, house, land, etc.).
    /// </summary>
    public required ObjectType Type { get; set; }

    /// <summary>
    /// Purpose/usage category of the real estate (residential, commercial, etc.).
    /// </summary>
    public required ObjectPurpose Purpose { get; set; }

    /// <summary>
    /// Cadastral number (string format, may include separators).
    /// </summary>
    public required string CadastralNumber { get; set; }

    /// <summary>
    /// Full address of the property.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Number of floors in the building (total floors).
    /// </summary>
    public required int FloorsTotal { get; set; }

    /// <summary>
    /// Total area in square meters.
    /// </summary>
    public required double TotalArea { get; set; }

    /// <summary>
    /// Number of rooms in the property (if applicable).
    /// </summary>
    public required int Rooms { get; set; }

    /// <summary>
    /// Height of ceilings in meters.
    /// </summary>
    public required double CeilingHeight { get; set; }

    /// <summary>
    /// Floor number where the unit is located (0 for ground/land).
    /// </summary>
    public required int FloorNumber { get; set; }

    /// <summary>
    /// Indicates whether the property has encumbrances (mortgage, arrest, etc.).
    /// </summary>
    public bool HasEncumbrances { get; set; }
}
