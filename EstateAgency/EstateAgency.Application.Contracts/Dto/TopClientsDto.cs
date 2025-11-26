namespace EstateAgency.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for returning top buyers and sellers counterparties.
/// </summary>
/// <param name="TopBuyers">List of top sellers counterparties.</param>
/// <param name="TopSellers">List of top sellers counterparties.</param>
public record TopClientsDto(
    List<CounterpartyWithCountDto> TopBuyers,
    List<CounterpartyWithCountDto> TopSellers
);
