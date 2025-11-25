using AutoMapper;
using EstateAgency.Application.Contracts.Dto;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstateAgency.Api.Controllers;

/// <summary>
/// Controller for managing real estate entities.
/// Provides endpoints to create, read, update, and delete real estates.
/// </summary>
/// <param name="repo">Repository for accessing real estate entities.</param>
/// <param name="mapper">AutoMapper instance for mapping entities to DTOs.</param>
[ApiController]
[Route("api/real-estates")]
public class RealEstatesController(
    IRepository<RealEstate> repo, 
    IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Retrieves all real estate entities.
    /// </summary>
    /// <returns>A list of <see cref="RealEstateGetDto"/>.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RealEstateGetDto>>> GetAll()
    {
        var entities = await repo.GetAllAsync();
        var dtos = mapper.Map<IEnumerable<RealEstateGetDto>>(entities);
        return Ok(dtos);
    }

    /// <summary>
    /// Retrieves a specific real estate by its ID.
    /// </summary>
    /// <param name="id">The ID of the real estate.</param>
    /// <returns>The <see cref="RealEstateGetDto"/> if found; otherwise, 404 Not Found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RealEstateGetDto>> Get(int id)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null) return NotFound();
        var dto = mapper.Map<RealEstateGetDto>(entity);
        return Ok(dto);
    }

    /// <summary>
    /// Creates a new real estate entity.
    /// </summary>
    /// <param name="dto">The real estate data to create.</param>
    /// <returns>The created <see cref="RealEstateGetDto"/> with status 201 Created.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<RealEstateGetDto>> Create([FromBody] RealEstateEditDto dto)
    {
        var entity = mapper.Map<RealEstate>(dto);
        var created = await repo.AddAsync(entity);
        var resultDto = mapper.Map<RealEstateGetDto>(created);
        return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
    }

    /// <summary>
    /// Updates an existing real estate entity.
    /// </summary>
    /// <param name="id">The ID of the real estate to update.</param>
    /// <param name="dto">The updated real estate data.</param>
    /// <returns>The updated <see cref="RealEstateGetDto"/> if successful, 404 Not Found if not found, or 400 Bad Request if invalid.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RealEstateGetDto>> Update(int id, [FromBody] RealEstateEditDto dto)
    {
        var entity = await repo.GetByIdAsync(id);
        if (entity == null) return NotFound();
        mapper.Map(dto, entity);
        await repo.UpdateAsync(entity);
        var resultDto = mapper.Map<RealEstateGetDto>(entity);
        return Ok(resultDto);
    }

    /// <summary>
    /// Deletes a real estate entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the real estate to delete.</param>
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
