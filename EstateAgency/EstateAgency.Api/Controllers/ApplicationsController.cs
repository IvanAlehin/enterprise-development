using AutoMapper;
using EstateAgency.Application.Contracts.Dto;
using EstateAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstateAgency.Api.Controllers;

/// <summary>
/// Controller for managing real estate applications.
/// Provides endpoints to create, read, update, and delete applications.
/// </summary>
/// <param name="repo">Repository for accessing application entities.</param>
/// <param name="mapper">AutoMapper instance for mapping entities to DTOs.</param>
[ApiController]
[Route("api/applications")]
public class ApplicationsController(
    IRepository<EstateAgency.Domain.Entities.Application> repo,
    IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Retrieves all applications.
    /// </summary>
    /// <returns>A list of <see cref="ApplicationGetDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ApplicationGetDto>>> GetAll()
    {
        var entities = await repo.GetAllAsync();
        var dtos = mapper.Map<IEnumerable<ApplicationGetDto>>(entities);
        return Ok(dtos);
    }

    /// <summary>
    /// Retrieves a specific application by its ID.
    /// </summary>
    /// <param name="id">The ID of the application.</param>
    /// <returns>The <see cref="ApplicationGetDto"/> if found; otherwise, 404 Not Found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApplicationGetDto>> Get(int id)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null) return NotFound();
        var dto = mapper.Map<ApplicationGetDto>(entity);
        return Ok(dto);
    }

    /// <summary>
    /// Creates a new application.
    /// </summary>
    /// <param name="dto">The application data to create.</param>
    /// <returns>The created <see cref="ApplicationGetDto"/> with status 201 Created.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ApplicationGetDto>> Create([FromBody] ApplicationEditDto dto)
    {
        var entity = mapper.Map<EstateAgency.Domain.Entities.Application>(dto);
        var created = await repo.AddAsync(entity);
        var resultDto = mapper.Map<ApplicationGetDto>(created);
        return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
    }

    /// <summary>
    /// Updates an existing application.
    /// </summary>
    /// <param name="id">The ID of the application to update.</param>
    /// <param name="dto">The updated application data.</param>
    /// <returns>The updated <see cref="ApplicationGetDto"/> if successful, 404 Not Found if not found, or 400 Bad Request if invalid.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApplicationGetDto>> Update(int id, [FromBody] ApplicationEditDto dto)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null) return NotFound();
        mapper.Map(dto, entity);
        await repo.UpdateAsync(entity);
        var resultDto = mapper.Map<ApplicationGetDto>(entity);
        return Ok(resultDto);
    }

    /// <summary>
    /// Deletes an application by its ID.
    /// </summary>
    /// <param name="id">The ID of the application to delete.</param>
    /// <returns>204 No Content if deleted; 404 Not Found if not found.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await repo.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}