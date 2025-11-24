namespace EstateAgency.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for returning application information.
/// </summary>
/// <param name="Id">Application ID.</param>
/// <param name="CounterpartyId">ID of the associated counterparty.</param>
/// <param name="RealEstateId">ID of the associated real estate.</param>
/// <param name="TransactionAmount">Transaction amount.</param>
/// <param name="Type">Application type.</param>
/// <param name="Date">Application date.</param>
public record ApplicationGetDto(
    int Id,
    int CounterpartyId,
    int RealEstateId,
    decimal TransactionAmount,
    string Type,
    DateTime Date
);
