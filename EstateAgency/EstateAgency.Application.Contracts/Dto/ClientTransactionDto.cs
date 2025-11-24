namespace EstateAgency.Application.Contracts.Dto;

/// <summary>
/// Data transfer object representing a client and their total transaction amount.
/// </summary>
/// <param name="Client">The client information.</param>
/// <param name="TransactionAmount">The total transaction amount for the client.</param>
public record ClientTransactionDto(
    CounterpartyGetDto Client,
    decimal TransactionAmount
);
