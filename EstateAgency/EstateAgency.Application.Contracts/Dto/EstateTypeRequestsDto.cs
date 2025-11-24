namespace EstateAgency.Application.Contracts.Dto;

/// <summary>
/// Data transfer object representing the number of requests for a specific estate type.
/// </summary>
/// <param name="EstateType">The type of the real estate.</param>
/// <param name="Count">The number of requests for this estate type.</param>
public record EstateTypeRequestsDto(
    string EstateType, 
    int Count
);
