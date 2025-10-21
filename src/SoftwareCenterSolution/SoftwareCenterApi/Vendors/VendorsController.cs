using System.ComponentModel.DataAnnotations;
using Marten;
using SoftwareCenterApi.Vendors.Entities;
using SoftwareCenterApi.Vendors.Models;

namespace SoftwareCenterApi.Vendors;

// When we get a GET request to "/vendors", we want this controller to be created, and
// a specific method on this controller to handle providing the response for the request
[ApiController]
public class VendorsController(IDocumentSession session): ControllerBase
{
    [HttpGet("/vendors")]
    public async Task<ActionResult> GetAllVendorsAsync() 
    {
        return Ok();
    }

    [HttpPost("/vendors")]
    public async Task<ActionResult> AddVendorAsync(
        [FromBody] VendorCreateModel model,
        [FromServices] VendorCreateModelValidator validator
        ) 
    {
        // TODO: Validate the inputs, check auth all that stuff
        var validations = await validator.ValidateAsync(model);

        if (!validations.IsValid)
        {
            return BadRequest();
        }

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

// POST /user/vaction-requests
