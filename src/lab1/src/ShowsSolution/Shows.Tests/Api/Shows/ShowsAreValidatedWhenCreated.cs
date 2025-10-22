using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba;
using Shows.Api.Shows.Models;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Collection("SystemTestFixture")]
[Trait("Category", "SystemTest")]
public class ShowsAreValidatedWhenCreated(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Theory]
    [MemberData(nameof(InvalidExamples))]
    public async Task BadRequestsReturnA400(ShowCreateRequestModel model, string _)
    {
        await _host.Scenario(api => {
            api.Post.Json(model).ToUrl("/api/shows");
            api.StatusCodeShouldBe(400);
        });
    }

    public static int showNameMinLength = 3;
    public static int showNameMaxLength = 100;
    public static int showDescriptionMinLength = 10;
    public static int showDescriptionMaxLength = 500;
    public static IEnumerable<object[]> ValidExamples() =>
   [
       [
            new ShowCreateRequestModel {Name = new string('X', showNameMaxLength), Description = new string('X', showDescriptionMaxLength), StreamingService="X" },
            $"The minimum length of show name should be {showNameMinLength}"
        ],
        [
            new ShowCreateRequestModel { Name = new string('X', showNameMaxLength), Description = new string('X', showDescriptionMinLength), StreamingService ="Netflix"},
            $"This should be valid, blah blah blah"
            ],


       ];

    public static IEnumerable<object[]> InvalidExamples() =>
        [
        [
            new ShowCreateRequestModel {Name = new string('X', showNameMinLength -1), Description=new string('X', showDescriptionMaxLength), StreamingService = "X"},
            "The Name Doesn't Match The Min Length"
            ],
        [
                new ShowCreateRequestModel { Name = new string('X', showNameMinLength), Description = new string('X', showDescriptionMinLength)},
                "Streaming Service Should Be Required"
                ]
        ];
}
