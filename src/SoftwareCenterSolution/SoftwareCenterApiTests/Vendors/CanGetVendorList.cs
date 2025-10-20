using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Alba;

namespace SoftwareCenterApiTests.Vendors;

[Trait("Category", "System")]
public class CanGetVendorList
{
    //[Fact]
    //public async Task GovesASuccessStatusCodeAsync() 
    //{ 
    //    var client = new HttpClient();
    //    client.BaseAddress = new Uri("http://localhost:1337");

    //    var response = await client.GetAsync("/vendors");
    //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    //}

    [Fact]
    public async Task GettingAllVendorsAsync() 
    {
        // start up the api using my program.cs in memory
        var host = await AlbaHost.For<Program>();
        // here's the scenario for this test
        await host.Scenario(api => 
        {
            // get the vendors (no host or anything, it's internal)
            api.Get.Url("/vendors");
            // if it isn't this, fails
            api.StatusCodeShouldBeOk();
        });
    }
}
