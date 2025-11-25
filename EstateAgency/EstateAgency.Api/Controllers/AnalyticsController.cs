using AutoMapper;
using EstateAgency.Application.Contracts.Dto;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Enums;
using EstateAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstateAgency.Api.Controllers;

/// <summary>
/// Provides analytical endpoints for the real estate agency system
/// </summary>
/// <param name="applicationsRepo">Repository for accessing application data.</param>
/// <param name="counterpartiesRepo">Repository for accessing counterparty data.</param>
/// <param name="realEstateRepo">Repository for accessing real estate data.</param>
/// <param name="mapper">AutoMapper instance for mapping entities to DTOs.</param>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(
    IRepository<Domain.Entities.Application> applicationsRepo,
    IRepository<Counterparty> counterpartiesRepo,
    IRepository<RealEstate> realEstateRepo,
    IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Gets sellers who made sales within a specified period.
    /// </summary>
    /// <param name="start">Start date of the period.</param>
    /// <param name="end">End date of the period.</param>
    /// <returns>A list of sellers as <see cref="CounterpartyGetDto"/>.</returns>
    [HttpGet("sellers-by-period")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CounterpartyGetDto>>> GetSellersByPeriod(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        var apps = await applicationsRepo.GetAllAsync();

        var sellersIds = apps
            .Where(a => a.Type == ApplicationType.Sale && a.Date >= start && a.Date <= end)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .ToList();

        var counterparties = await counterpartiesRepo.GetAllAsync();

        var sellersDtos = counterparties
            .Where(c => sellersIds.Contains(c.Id))
            .OrderBy(c => c.FullName)
            .Select(c => mapper.Map<CounterpartyGetDto>(c));

        return Ok(sellersDtos);
    }

    /// <summary>
    /// Gets the top 5 buyers and top 5 sellers by number of transactions.
    /// </summary>
    /// <returns>An object containing top buyers and top sellers lists of <see cref="TopClientDto"/>.</returns>
    [HttpGet("top-clients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<object>> GetTopClients()
    {
        var apps = await applicationsRepo.GetAllAsync();
        var counterparties = await counterpartiesRepo.GetAllAsync();

        var topBuyers = apps
            .Where(a => a.Type == ApplicationType.Purchase)
            .GroupBy(a => a.CounterpartyId)
            .Select(g => new { CounterpartyId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.CounterpartyId)
            .Take(5)
            .Join(counterparties, x => x.CounterpartyId, c => c.Id, (x, c) => new TopClientDto(
                mapper.Map<CounterpartyGetDto>(c),
                x.Count))
            .ToList();

        var topSellers = apps
            .Where(a => a.Type == ApplicationType.Sale)
            .GroupBy(a => a.CounterpartyId)
            .Select(g => new { CounterpartyId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.CounterpartyId)
            .Take(5)
            .Join(counterparties, x => x.CounterpartyId, c => c.Id, (x, c) => new TopClientDto(
                mapper.Map<CounterpartyGetDto>(c),
                x.Count))
            .ToList();

        return Ok(new { TopBuyers = topBuyers, TopSellers = topSellers });
    }

    /// <summary>
    /// Gets the number of requests grouped by estate type.
    /// </summary>
    /// <returns>A list of <see cref="EstateTypeRequestsDto"/>.</returns>
    [HttpGet("requests-by-estate-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EstateTypeRequestsDto>>> GetRequestsByEstateType()
    {
        var apps = await applicationsRepo.GetAllAsync();
        var estates = await realEstateRepo.GetAllAsync();

        var result = apps
            .Join(estates, a => a.RealEstateId, r => r.Id, (a, r) => r.Type)
            .GroupBy(t => t)
            .Select(g => new EstateTypeRequestsDto(g.Key.ToString(), g.Count()))
            .OrderBy(x => x.EstateType)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Gets clients with the minimum transaction amount.
    /// </summary>
    /// <returns>A list of <see cref="ClientTransactionDto"/>.</returns>
    [HttpGet("clients-with-min-transaction")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ClientTransactionDto>>> GetClientsWithMinTransaction()
    {
        var apps = await applicationsRepo.GetAllAsync();
        var counterparties = await counterpartiesRepo.GetAllAsync();
        var minAmount = apps.Min(a => a.TransactionAmount);

        var clients = apps
            .Where(a => a.TransactionAmount == minAmount)
            .Select(a => a.CounterpartyId)
            .Distinct()
            .Join(counterparties, id => id, c => c.Id,
                (id, c) => new ClientTransactionDto(
                    mapper.Map<CounterpartyGetDto>(c),
                    minAmount))
            .OrderBy(c => c.Client.FullName)
            .ToList();

        return Ok(clients);
    }

    /// <summary>
    /// Gets clients who purchased or sold real estate of a specific type.
    /// </summary>
    /// <param name="type">The type of real estate.</param>
    /// <returns>A list of clients as <see cref="CounterpartyGetDto"/>.</returns>
    [HttpGet("clients-by-estate-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CounterpartyGetDto>>> GetClientsByEstateType([FromQuery] RealEstateType type)
    {
        var apps = await applicationsRepo.GetAllAsync();
        var estates = await realEstateRepo.GetAllAsync();
        var counterparties = await counterpartiesRepo.GetAllAsync();

        var clientIds = apps
            .Join(estates, a => a.RealEstateId, r => r.Id, (a, r) => new { a, r })
            .Where(x => x.r.Type == type)
            .Select(x => x.a.CounterpartyId)
            .Distinct()
            .ToList();

        var clientsDtos = counterparties
            .Where(c => clientIds.Contains(c.Id))
            .OrderBy(c => c.FullName)
            .Select(c => mapper.Map<CounterpartyGetDto>(c))
            .ToList();

        return Ok(clientsDtos);
    }
}
