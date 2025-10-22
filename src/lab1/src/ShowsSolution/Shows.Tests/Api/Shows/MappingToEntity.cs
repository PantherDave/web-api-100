using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using Shows.Api.Shows;
using Shows.Api.Shows.Models;

namespace Shows.Tests.Api.Shows;

[Trait("Category", "Unit")]
public class MappingToEntity
{
    [Fact]
    public void CanMapModelToEntity() 
    {
        var model = new ShowCreateRequestModel
        {
            Name = "Name",
            Description = "Description",
            StreamingService = "StreamingService",
        };

        var fakeDate = new DateTimeOffset(1969, 4, 20, 23, 59, 59, TimeSpan.FromHours(-4));
        var fakeClock = new FakeTimeProvider(fakeDate);
        var fakeTestIdGenerator = Substitute.For<IProvideUniqueIds>();
        var fakeId = Guid.Parse("e9c0c2d5-aeea-4df4-a3e5-38f3025e1099");
        fakeTestIdGenerator.GetGuid().Returns(fakeId);

        var result = model.MapToEntity(fakeClock, fakeTestIdGenerator);

        Assert.Equal(model.Name, result.Name);
        Assert.Equal(model.Description, result.Description);
        Assert.Equal(model.StreamingService, result.StreamingService);
        Assert.Equal(fakeDate, result.CreatedAt);
        Assert.Equal(fakeId, result.Id);
    }
}
