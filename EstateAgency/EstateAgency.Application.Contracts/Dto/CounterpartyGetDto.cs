namespace EstateAgency.Application.Contracts.Dto;

/// <summary>
/// Data transfer object for returning counterparty information.
/// </summary>
/// <param name="Id">Unique identifier of the counterparty.</param>
/// <param name="FullName">Full name of the counterparty.</param>
/// <param name="PassportNumber">Passport number of the counterparty.</param>
/// <param name="PhoneNumber">Phone number of the counterparty.</param>
public record CounterpartyGetDto(
    int Id,
    string FullName,
    string PassportNumber,
    string PhoneNumber
);
