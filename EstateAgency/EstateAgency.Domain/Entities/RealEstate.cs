using EstateAgency.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstateAgency.Domain.Entities;

/// <summary>
/// Represents a real estate object (property) in the agency system.
/// Contains physical, legal and address information.
/// </summary>
[Table("real_estates")]
public class RealEstate
{
    /// <summary>
    /// Primary key — integer identifier for the object.
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Type of the real estate (apartment, house, land, etc.).
    /// </summary>
    [Column("type")]
    public required RealEstateType Type { get; set; }

    /// <summary>
    /// Purpose/usage category of the real estate (residential, commercial, etc.).
    /// </summary>
    [Column("purpose")]
    public required RealEstatePurpose Purpose { get; set; }

    /// <summary>
    /// Cadastral number (string format, may include separators).
    /// </summary>
    [Column("cadastral_number")]
    [MaxLength(50)]
    public required string CadastralNumber { get; set; }

    /// <summary>
    /// Full address of the property.
    /// </summary>
    [Column("address")]
    [MaxLength(50)]
    public required string Address { get; set; }

    /// <summary>
    /// Number of floors in the building (total floors).
    /// </summary>
    [Column("floors_total")]
    public required int FloorsTotal { get; set; }

    /// <summary>
    /// Total area in square meters.
    /// </summary>
    [Column("total_area")]
    public required double TotalArea { get; set; }

    /// <summary>
    /// Number of rooms in the property (if applicable).
    /// </summary>
    [Column("rooms")]
    public required int Rooms { get; set; }

    /// <summary>
    /// Height of ceilings in meters.
    /// </summary>
    [Column("ceiling_height")]
    public required double CeilingHeight { get; set; }

    /// <summary>
    /// Floor number where the unit is located (0 for ground/land).
    /// </summary>
    [Column("floor_number")]
    public required int FloorNumber { get; set; }

    /// <summary>
    /// Indicates whether the property has encumbrances (mortgage, arrest, etc.).
    /// </summary>
    [Column("has_encumbrances")]
    public bool HasEncumbrances { get; set; }
}
