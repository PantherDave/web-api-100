using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alba;
using SoftwareCenterApi.Vendors;

namespace SoftwareCenterApiTests.Vendors;
public class CanAddAVendor
{

    [Fact]
    public async Task AddingAVendor() 
    {
        var host = await AlbaHost.For<Program>();

        var vendorPointOfContact = new VendorPointOfContact
        {
            Name = "Satya Nadella",
            Email = "satya@microsoft.com",
            Phone = "555-555-MSFT"
        };

        var vendorToAdd = new VendorDetailsModel
        {
            Name = "Microsoft",
            PointOfContact = vendorPointOfContact,
        };

        var postResponse = await host.Scenario(api => 
        {
            api.Post.Json(vendorToAdd).ToUrl("/vendors");
            api.StatusCodeShouldBe(201);
        });

        var postEntityReturned = postResponse.ReadAsJson<VendorDetailsModel>();

        Assert.NotNull(postEntityReturned);

        Assert.True(postEntityReturned.Id != Guid.Empty);
        Assert.Equal(postEntityReturned.Name, vendorToAdd.Name);
        Assert.Equal(postEntityReturned.PointOfContact, vendorToAdd.PointOfContact);

        var getResponse = await host.Scenario(api => 
        {
            api.Get.Url($"/vendors/{postEntityReturned.Id}");
            api.StatusCodeShouldBeOk();
        });

        var getEntityReturned = getResponse.ReadAsJson<VendorDetailsModel>();
        Assert.NotNull(getEntityReturned);
        Assert.Equal(postEntityReturned, getEntityReturned);
    }
}
