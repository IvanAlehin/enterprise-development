using Bogus;
using EstateAgency.Application.Contracts.Dto;

namespace EstateAgency.Kafka;

/// <summary>
/// Generates randomized application DTOs for Kafka producing.
/// </summary>
/// <param name="maxCounterparties">Max counterparty ID value.</param>
/// <param name="maxRealEstates">Max real estate ID value.</param>
public class ApplicationGenerator(int maxCounterparties = 10, int maxRealEstates = 10)
{
    /// <summary>
    /// Faker instance for generating application DTOs.
    /// </summary>
    private readonly Faker<ApplicationEditDto> _faker = new Faker<ApplicationEditDto>()
        .CustomInstantiator(f => new ApplicationEditDto(
            CounterpartyId: f.Random.Int(1, maxCounterparties),
            RealEstateId: f.Random.Int(1, maxRealEstates),
            TransactionAmount: f.Finance.Amount(10000, 1000000),
            Type: f.PickRandom(_items),
            Date: f.Date.Recent(90)
        ));

    /// <summary>
    /// Possible application types.
    /// </summary>
    private static readonly string[] _items = ["Purchase", "Sale"];

    /// <summary>
    /// Generates a new random application DTO.
    /// </summary>
    public ApplicationEditDto Generate()
    {
        return _faker.Generate();
    }
}
