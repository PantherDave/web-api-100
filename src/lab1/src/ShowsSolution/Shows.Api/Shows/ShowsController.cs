using System.Linq.Expressions;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Shows.Api.Shows.Entities;
using Shows.Api.Shows.Models;

namespace Shows.Api.Shows;

[ApiController]
public class ShowsController(IDocumentSession session, TimeProvider clock): ControllerBase
{
    [HttpPost("/api/shows")]
    public async Task<ActionResult> AddAShowAsync(
        [FromBody] ShowCreateRequestModel request,
        [FromServices] ShowCreateRequestValidator validator,
        [FromServices] IProvideUniqueIds idGenerator
        )
    {
        var validationResults = await validator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return BadRequest();
        }

        var entity = request.MapToEntity(clock, idGenerator);

        session.Store(entity);
        await session.SaveChangesAsync();

        var response = new ShowDetailsResponseModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            StreamingService = entity.StreamingService,
            CreatedAt = entity.CreatedAt,
        };

        return Ok(response);
    }

    [HttpGet("/api/shows")]
    public async Task<ActionResult> GetAllShowsAsync() 
    {
        var shows = await session.Query<ShowEntity>()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        return Ok(shows);
    }
}
