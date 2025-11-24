namespace EstateAgency.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for creating or updating a counterparty.
/// </summary>
/// <param name="FullName">Full name of the counterparty.</param>
/// <param name="PassportNumber">Passport number of the counterparty.</param>
/// <param name="PhoneNumber">Phone number of the counterparty.</param>
public record CounterpartyEditDto(
    string FullName,
    string PassportNumber,
    string PhoneNumber
);
