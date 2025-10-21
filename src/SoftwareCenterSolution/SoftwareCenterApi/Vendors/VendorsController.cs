namespace SoftwareCenterApi.Vendors;

// When we get a GET request to "/vendors", we want this controller to be created, and
// a specific method on this controller to handle providing the response for the request
public class VendorsController: ControllerBase
{
    [HttpGet("/vendors")]
    public async Task<ActionResult> GetAllVendorsAsync() 
    {
        return Ok();
    }

    [HttpPost("/vendors")]
    public async Task<ActionResult> AddVendorAsync(
        [FromBody] VendorCreateModel model
        ) 
    {
        var response = new VendorDetailsModel
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            PointOfContact = model.PointOfContact,
        };
        return StatusCode(201, response);
    }
}

public record VendorPointOfContact
{ 
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public record VendorCreateModel 
{
    public string Name { get; set; } = string.Empty;
    public VendorPointOfContact PointOfContact { get; set; } = new();
}

public record VendorDetailsModel 
{
    public Guid Id;
    public string Name { get; set; } = string.Empty;
    public VendorPointOfContact PointOfContact { get; set; } = new();
}