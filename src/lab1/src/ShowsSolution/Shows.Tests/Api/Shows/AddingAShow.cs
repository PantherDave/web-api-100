using Alba;
using Shows.Api.Shows.Models;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Collection("SystemTestFixture")]
[Trait("Category", "SystemTest")]
public class AddingAShow(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Fact]
    public async Task AddShow()
    {
        var showToCreate = new ShowCreateRequestModel
        {
            Name = "Test Show",
            Description = "This is a test show",
            StreamingService = "HBO Max",
        };

        var response = await _host.Scenario(api =>
        {
            api.Post.Json(showToCreate).ToUrl("/api/shows");
            api.StatusCodeShouldBeOk();
        });

        var postBody = response.ReadAsJson<ShowDetailsResponseModel>();

        Assert.NotNull(postBody);
        Assert.Equal(showToCreate.Name, postBody.Name);
        Assert.Equal(showToCreate.StreamingService, postBody.StreamingService);
        Assert.Equal(showToCreate.Description, postBody.Description);

        var getResponse = await _host.Scenario(api => 
        {
            // GET /shows/{id}
            api.Get.Url("/api/shows");
            api.StatusCodeShouldBeOk();
        });

        var getBody = getResponse.ReadAsJson<List<ShowDetailsResponseModel>>();

        Assert.NotNull(getBody);
        Assert.True(getBody.Any());

        var showAtTheTop = getBody.First();
        Assert.Equal(postBody, showAtTheTop);
    }
    
}