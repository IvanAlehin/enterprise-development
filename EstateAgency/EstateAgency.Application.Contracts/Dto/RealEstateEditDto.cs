namespace EstateAgency.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for creating or updating a real estate entity.
/// </summary>
/// <param name="Type">Type of the real estate.</param>
/// <param name="Purpose">Purpose of the property (e.g., residential, commercial).</param>
/// <param name="CadastralNumber">Cadastral number of the property.</param>
/// <param name="Address">Address of the property.</param>
/// <param name="FloorsTotal">Total number of floors in the building.</param>
/// <param name="TotalArea">Total area of the property in square meters.</param>
/// <param name="Rooms">Number of rooms in the property.</param>
/// <param name="CeilingHeight">Ceiling height in meters.</param>
/// <param name="FloorNumber">Floor number where the property is located.</param>
/// <param name="HasEncumbrances">Indicates whether the property has encumbrances or restrictions.</param>
public record RealEstateEditDto(
    string Type,
    string Purpose,
    string CadastralNumber,
    string Address,
    int FloorsTotal,
    double TotalArea,
    int Rooms,
    double CeilingHeight,
    int FloorNumber,
    bool HasEncumbrances
);
