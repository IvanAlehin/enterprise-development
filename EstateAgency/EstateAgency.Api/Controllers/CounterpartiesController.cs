using AutoMapper;
using EstateAgency.Application.Contracts.Dto;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstateAgency.Api.Controllers;

/// <summary>
/// Controller for managing counterparties.
/// Provides endpoints to create, read, update, and delete counterparties.
/// </summary>
/// <param name="repo">Repository for accessing counterparty entities.</param>
/// <param name="mapper">AutoMapper instance for mapping entities to DTOs.</param>
[ApiController]
[Route("api/counterparties")]
public class CounterpartiesController(
    IRepository<Counterparty> repo,
    IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Retrieves all counterparties.
    /// </summary>
    /// <returns>A list of <see cref="CounterpartyGetDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CounterpartyGetDto>>> GetAll()
    {
        var entities = await repo.GetAllAsync();
        var dtos = mapper.Map<IEnumerable<CounterpartyGetDto>>(entities);
        return Ok(dtos);
    }

    /// <summary>
    /// Retrieves a specific counterparty by its ID.
    /// </summary>
    /// <param name="id">The ID of the counterparty.</param>
    /// <returns>The <see cref="CounterpartyGetDto"/> if found; otherwise, 404 Not Found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CounterpartyGetDto>> Get(int id)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null) return NotFound();
        var dto = mapper.Map<CounterpartyGetDto>(entity);
        return Ok(dto);
    }

    /// <summary>
    /// Creates a new counterparty.
    /// </summary>
    /// <param name="dto">The counterparty data to create.</param>
    /// <returns>The created <see cref="CounterpartyGetDto"/> with status 201 Created.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CounterpartyGetDto>> Create([FromBody] CounterpartyEditDto dto)
    {
        var entity = mapper.Map<Counterparty>(dto);
        var created = await repo.AddAsync(entity);
        var resultDto = mapper.Map<CounterpartyGetDto>(created);
        return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
    }

    /// <summary>
    /// Updates an existing counterparty.
    /// </summary>
    /// <param name="id">The ID of the counterparty to update.</param>
    /// <param name="dto">The updated counterparty data.</param>
    /// <returns>The updated <see cref="CounterpartyGetDto"/> if successful, 404 Not Found if not found, or 400 Bad Request if invalid.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CounterpartyGetDto>> Update(int id, [FromBody] CounterpartyEditDto dto)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null) return NotFound();
        mapper.Map(dto, entity);
        await repo.UpdateAsync(entity);
        var resultDto = mapper.Map<CounterpartyGetDto>(entity);
        return Ok(resultDto);
    }

    /// <summary>
    /// Deletes a counterparty by its ID.
    /// </summary>
    /// <param name="id">The ID of the counterparty to delete.</param>
    /// <returns>204 No Content if deleted.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        await repo.DeleteAsync(id);
        return NoContent();
    }
}
