using Marten;

namespace SoftwareCenterApi.Vendors;

// When we get a GET request to "/vendors", we want this controller to be created, and
// a specific method on this controller to handle providing the response for the request
public class VendorsController(IDocumentSession session): ControllerBase
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
        // TODO: Validate the inputs, check auth all that stuff

        //store the data somewhere

        var entity = new VendorEntity
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            PointOfContact = model.PointOfContact,
        };
        session.Store(entity);
        await session.SaveChangesAsync();

        var response = new VendorDetailsModel
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            PointOfContact = model.PointOfContact,
        };
        return StatusCode(201, response);
    }

    [HttpGet("/vendors/{id:Guid}")]
    public async Task<ActionResult> GetVendorByIdAsync(Guid id)
    {
        var savedVendor = await session.Query<VendorEntity>()
            .Where(v => v.Id == id)
            .SingleOrDefaultAsync();

        if (savedVendor == null)
        {
            return NotFound();
        }
        else 
        {
            var response = new VendorDetailsModel
            {
                Id = savedVendor.Id,
                Name = savedVendor.Name,
                PointOfContact = savedVendor.PointOfContact,
            };

            return Ok(response);
        }
    }
}

public record VendorPointOfContact
{ 
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

// this represents what we are expecting from the client on the POST /vendors
public record VendorCreateModel 
{
    public string Name { get; set; } = string.Empty;
    public VendorPointOfContact PointOfContact { get; set; } = new();
}

// what I am returning to the caller on the POST and the GET /vendors/{id}
public record VendorDetailsModel 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public VendorPointOfContact PointOfContact { get; set; } = new();
}

// what I'm actually storing in the db
public class VendorEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public VendorPointOfContact PointOfContact { get; set; } = new();
    // who created this?
}