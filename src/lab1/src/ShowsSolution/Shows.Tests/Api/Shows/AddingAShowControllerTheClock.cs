using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using Shows.Api.Shows;
using Shows.Api.Shows.Models;

namespace Shows.Tests.Api.Shows;

public class AddingAShowControllerTheClock
{

    [Fact]
    public async Task ShowCreatedOnMyBirthday()
    {
        var myBirthday = new DateTimeOffset(1969, 4, 20, 23, 59, 59, TimeSpan.Zero);
        FakeTimeProvider provider;
        var fakeId = Guid.Parse("e9c0c2d5-aeea-4df4-a3e5-38f3025e1099");
        var host = await AlbaHost.For<Program>(config => 
        {
            config.ConfigureTestServices(sc =>
            {
                // before the build this is called
                provider = new FakeTimeProvider(myBirthday);
                sc.AddSingleton<TimeProvider>();

                var fakeTestIdGenerator = Substitute.For<IProvideUniqueIds>();

                fakeTestIdGenerator.GetGuid().Returns(fakeId);
                sc.AddSingleton<IProvideUniqueIds>(_ => fakeTestIdGenerator);
            });
        });
        var showToCreate = new ShowCreateRequestModel
        {
            Name = "Test Show",
            Description = "This is a test show",
            StreamingService = "HBO Max",
        };

        var response = await host.Scenario(api =>
        {
            api.Post.Json(showToCreate).ToUrl("/api/shows");
            api.StatusCodeShouldBeOk();
        });

        var postBody = response.ReadAsJson<ShowDetailsResponseModel>();

        Assert.NotNull(postBody);
        Assert.Equal(fakeId, postBody.Id);

        Assert.Equal(myBirthday, postBody.CreatedAt);
    }
}
