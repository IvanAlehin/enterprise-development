namespace EstateAgency.Application.Contracts.Dto;

/// <summary>
/// Data transfer object representing a top client and their number of transactions.
/// </summary>
/// <param name="Client">The client information.</param>
/// <param name="Count">The number of transactions made by the client.</param>
public record TopClientDto(
    CounterpartyGetDto Client,
    int Count
);
