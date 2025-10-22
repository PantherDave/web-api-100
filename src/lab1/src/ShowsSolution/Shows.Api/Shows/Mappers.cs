using Shows.Api.Shows.Entities;
using Shows.Api.Shows.Models;

namespace Shows.Api.Shows;

public static class Mappers
{
    public static ShowEntity MapToEntity(this ShowCreateRequestModel model, TimeProvider clock, IProvideUniqueIds idGenerator)
    {
        return new ShowEntity
        {
            Id = idGenerator.GetGuid(),
            Name = model.Name,
            Description = model.Description,
            StreamingService = model.StreamingService,
            CreatedAt = clock.GetUtcNow(),
        };
    }
}
